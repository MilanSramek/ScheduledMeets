using HotChocolate.AspNetCore;
using HotChocolate.Execution;

using Microsoft.AspNetCore.Http;

using System.Security.Principal;

using static ScheduledMeets.Api.Authentication.AuthenticationGlobalState;

namespace ScheduledMeets.Api.Authentication;

internal class UserIdentityInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(
        HttpContext context,
        IRequestExecutor requestExecutor,
        IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        if (context.User is not { Identity: IIdentity identity } || !identity.IsAuthenticated)
            return ValueTask.CompletedTask;

        requestBuilder.SetGlobalState(User, identity);

        return base.OnCreateAsync(context, requestExecutor, requestBuilder,
            cancellationToken);
    }
}
