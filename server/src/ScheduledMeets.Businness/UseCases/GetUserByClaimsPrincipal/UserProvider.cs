using Dawn;

using MediatR;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;
using ScheduledMeets.Internals.Extensions;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.GetUserByClaimsPrincipal
{
    class UserProvider : IRequestHandler<GetUserByClaimsPrincipalRequest, User?>
    {
        private readonly IReadRepository<User> _users;

        public UserProvider(IReadRepository<User> users)
            => _users = Guard.Argument(users, nameof(users)).NotNull().Value;

        public async Task<User?> Handle(GetUserByClaimsPrincipalRequest request,
            CancellationToken cancellationToken)
        {
            ClaimsPrincipal claimsPrincipal = Guard.Argument(request).NotNull().Value
                .ClaimsPrincipal;

            if (!claimsPrincipal.Identities.Any())
                throw new ApplicationException($"Claims principal carries no user identity.");

            IEnumerable<ClaimsIdentity> identities = claimsPrincipal.Identities
                .Where(identity => identity.Name is not null);

            foreach (ClaimsIdentity identity in identities)
            {
                User? user = await _users
                    .SingleOrDefaultAsync(user => user.Username == identity.Name, cancellationToken);
                if (user is not null)
                    return user;
            }

            return null;
        }
    }
}
