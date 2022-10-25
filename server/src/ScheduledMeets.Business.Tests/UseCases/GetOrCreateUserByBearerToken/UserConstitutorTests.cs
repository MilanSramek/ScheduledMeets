using FluentAssertions;

using Moq;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.OAuth;
using ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;
using ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;
using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.Tests.UseCases.GetOrCreateUserByBearerToken;

public class UserConstitutorTests
{
    [Test]
    public async Task GetExistingUserTest()
    {
        string token = "token";
        ClaimsIdentity userIdentity = new();
        ClaimsPrincipal userPrincipal = new(userIdentity);
        User user = new("username");

        Mock<ITokenValidator> tokenValidator = new();
        tokenValidator
            .Setup(_ => _.ValidateAsync(
                It.Is<string>(value => value == token),
                It.IsAny<CancellationToken>()))
            .Returns<string, CancellationToken>((_, _) =>
                Task.FromResult(userPrincipal))
            .Verifiable("There was not attempt to verify the token.");

        Mock<IUserProvider> userProvider = new();
        userProvider
            .Setup(_ => _.GetUserBy(
                It.Is<ClaimsPrincipal>(value => value == userPrincipal),
                It.IsAny<CancellationToken>()))
            .Returns<ClaimsPrincipal, CancellationToken>((_, _) =>
                Task.FromResult<User?>(user))
            .Verifiable("There was no attempt to retrieve a user by the token.");

        UserConstitutor sut = new(
            Mock.Of<IProcessor<CreateUserByClaimsPrincipalRequest, User>>(),
            tokenValidator.Object, 
            userProvider.Object);
        User result = await sut.Handle(new GetOrCreateUserByBearerTokenRequest(token));

        result.Should().BeSameAs(user);
        tokenValidator.Verify();
        userProvider.Verify();
    }

    [Test]
    public async Task GetNewlyCreatedUserTest()
    {
        string token = "token";
        ClaimsIdentity userIdentity = new();
        ClaimsPrincipal userPrincipal = new(userIdentity);
        User user = new("username");

        Mock<ITokenValidator> tokenValidator = new();
        tokenValidator
            .Setup(_ => _.ValidateAsync(
                It.Is<string>(value => value == token),
                It.IsAny<CancellationToken>()))
            .Returns<string, CancellationToken>((_, _) =>
                Task.FromResult(userPrincipal))
            .Verifiable("There was not attempt to verify the token.");

        Mock<IUserProvider> userProvider = new();
        userProvider
            .Setup(_ => _.GetUserBy(
                It.Is<ClaimsPrincipal>(value => value == userPrincipal),
                It.IsAny<CancellationToken>()))
            .Returns<ClaimsPrincipal, CancellationToken>((_, _) =>
                Task.FromResult<User?>(null))
            .Verifiable("There was no attempt to retrieve a user by the token.");

        Mock<IProcessor<CreateUserByClaimsPrincipalRequest, User>> userCreator = new();
        userCreator
            .Setup(_ => _.ProcessAsync(
                It.Is<CreateUserByClaimsPrincipalRequest>(req => req.ClaimsPrincipal == userPrincipal),
                It.IsAny<CancellationToken>()))
            .Returns<CreateUserByClaimsPrincipalRequest, CancellationToken>((_, _) =>
                Task.FromResult(user))
            .Verifiable("There was no attempt to create a user.");

        UserConstitutor sut = new(
            userCreator.Object,
            tokenValidator.Object,
            userProvider.Object);
        User result = await sut.Handle(new GetOrCreateUserByBearerTokenRequest(token));

        result.Should().BeSameAs(user);
        tokenValidator.Verify();
        userProvider.Verify();
        userCreator.Verify();
    }
}
