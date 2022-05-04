
using Moq;

using NUnit.Framework;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal;
using ScheduledMeets.Business.UseCases.DecodeJsonWebBearerToken;
using ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;
using ScheduledMeets.Business.UseCases.GetUserByClaimsPrincipal;
using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.Tests.UseCases.GetOrCreateUserByBearerToken
{
    public class UserConstitutorTests
    {
        [Test]
        public async Task ValidateToken()
        {
            string token = "token";
            ClaimsIdentity userIdentity = new();
            ClaimsPrincipal userPrincipal = new(userIdentity);
            User user = new("username");

            Mock<IProcessor<DecodeJsonWebBearerTokenRequest, ClaimsPrincipal>> tokenValidator = new();
            tokenValidator
                .Setup(_ => _.ProcessAsync(
                    It.Is<DecodeJsonWebBearerTokenRequest>(req => req.Token == token),
                    It.IsAny<CancellationToken>()))
                .Returns<DecodeJsonWebBearerTokenRequest, CancellationToken>((_, _) =>
                    Task.FromResult(userPrincipal))
                .Verifiable();
            Mock<IProcessor<GetUserByClaimsPrincipalRequest, User?>> userProvider = new();
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            userProvider
                .Setup(_ => _.ProcessAsync(
                    It.Is<GetUserByClaimsPrincipalRequest>(req => req.ClaimsPrincipal == userPrincipal),
                    It.IsAny<CancellationToken>()))
                .Returns<GetUserByClaimsPrincipalRequest, CancellationToken>((_, _) =>
                    Task.FromResult(user))
                .Verifiable();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

            UserConstitutor sut = new(
                tokenValidator.Object, 
                userProvider.Object,
                Mock.Of<IProcessor<CreateUserByClaimsPrincipalRequest, User>>());
            User result = await sut.Handle(new GetOrCreateUserByBearerTokenRequest(token));

            Assert.That(result, Is.SameAs(user));
            tokenValidator.Verify();
            userProvider.Verify();
        }
    }
}
