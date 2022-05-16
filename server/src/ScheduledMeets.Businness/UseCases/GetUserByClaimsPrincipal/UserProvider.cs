using Dawn;

using IdentityModel;

using MediatR;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;
using ScheduledMeets.Internals.Extensions;

using System.Security.Claims;
using System.Security.Principal;

namespace ScheduledMeets.Business.UseCases.GetUserByClaimsPrincipal
{
    class UserProvider : IRequestHandler<GetUserByClaimsPrincipalRequest, User?>
    {
        private readonly IReadRepository<User> _users;

        public UserProvider(IReadRepository<User> users)
            => _users = Guard.Argument(users, nameof(users)).NotNull().Value;

        public async Task<User?> Handle(GetUserByClaimsPrincipalRequest request,
            CancellationToken cancellationToken = default)
        {
            ClaimsPrincipal claimsPrincipal = Guard.Argument(request).NotNull().Value
                .ClaimsPrincipal;

            if (!claimsPrincipal.Identities.Any())
                throw new ApplicationException($"Claims principal carries no user identity.");

            IEnumerable<IIdentity> identities = claimsPrincipal.Identities
                .Where(identity => identity.Name is not null);

            bool anyIdentifiableIdentity = false;
            foreach (IIdentity identity in identities)
            {
                User? user = await _users
                    .SingleOrDefaultAsync(user => user.Username == identity.Name, cancellationToken);
                if (user is not null)
                    return user;

                anyIdentifiableIdentity = true;
            }

            return anyIdentifiableIdentity 
                ? null
                : throw new ApplicationException($"Claims principal carries no user identifiable identity.");
        }
    }
}
