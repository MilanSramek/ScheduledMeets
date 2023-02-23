using FluentAssertions;

using Moq;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken;
using ScheduledMeets.Core;
using ScheduledMeets.TestTools.Extensions;

using System.Security.Claims;

namespace ScheduledMeets.Business.Tests.UseCases.GetUserByClaimsPrincipal;

public class UserProviderTest
{
    [Test]
    public async Task FindUser()
    {
        const string username = "username";

        Mock<IReadRepository<User>> users = new();
        users.SetupAsAsyncQueryable(new User[] { new(username) });

        ClaimsIdentity claimsIdentity = new();
        claimsIdentity.AddClaim(new(ClaimTypes.Name, username));

        ClaimsPrincipal claimsPrincipal = new();
        claimsPrincipal.AddIdentity(claimsIdentity);

        UserProvider sut = new(users.Object);
        User? user = await sut.GetUserBy(claimsPrincipal);

        user.Should().NotBeNull();
        user?.Username.Should().Be(username);
    }

    [Test]
    public async Task FindUserByAlternativeIdentity([Values("userA", "userB")] string username)
    {
        User user = new(username);
        Mock<IReadRepository<User>> users = new();
        users.SetupAsAsyncQueryable(new [] { user });

        ClaimsIdentity firstIdentity = new();
        firstIdentity.AddClaim(new(ClaimTypes.Name, "userA"));

        ClaimsIdentity secondaryIdentity = new();
        secondaryIdentity.AddClaim(new(ClaimTypes.Name, "userB"));

        ClaimsPrincipal claimsPrincipal = new();
        claimsPrincipal.AddIdentity(firstIdentity);
        claimsPrincipal.AddIdentity(secondaryIdentity);

        UserProvider sut = new(users.Object);
        User? result = await sut.GetUserBy(claimsPrincipal);

        result.Should().NotBeNull();
        result.Should().BeSameAs(user);
    }
}
