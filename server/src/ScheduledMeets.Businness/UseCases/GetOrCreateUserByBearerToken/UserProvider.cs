using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Core;
using ScheduledMeets.Internals.Extensions;

using System.Security.Claims;
using System.Security.Principal;

namespace ScheduledMeets.Business.UseCases.GetOrCreateUserByBearerToken
{
    internal class UserProvider : IUserProvider
    {
        private readonly IReadRepository<User> _users;

        public UserProvider(IReadRepository<User> users)
        {
            ArgumentNullException.ThrowIfNull(users);
            _users = users;
        }

        public async Task<User?> GetUserBy(ClaimsPrincipal claimsPrincipal,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

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
