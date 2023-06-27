using HotChocolate.Authorization;

using ScheduledMeets.Internals.Extensions;
using ScheduledMeets.View;

using System.Security.Principal;

using static ScheduledMeets.Api.Authentication.AuthenticationGlobalState;
using static ScheduledMeets.Api.Authorization.PolicyConsts;

namespace ScheduledMeets.Api;

[QueryType]
internal static class UserPoint
{
    [Authorize(Policy = BeTheUser, Apply = ApplyPolicy.AfterResolver)]
    [GraphQLName("currentUser")]
    [GraphQLType<NonNullType<UserType>>]
    public static Task<UserView> GetCurrentUserAsync(
        [GlobalState(User)]IIdentity user,
        [Service]IReader<UserView> userReader,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(userReader);

        return userReader.SingleAsync(_ => _.Username == user.Name, cancellationToken);
    }

    [Authorize(Policy = BeTheUser, Apply = ApplyPolicy.AfterResolver)]
    [NodeResolver]
    [GraphQLName("user")]
    [GraphQLType<NonNullType<UserType>>]
    public static Task<UserView> GetUserAsync(
        long id,
        [Service]IWithIdReader<UserView> userReader,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userReader);
        return userReader.ReadAsync(id, cancellationToken);
    }
}
