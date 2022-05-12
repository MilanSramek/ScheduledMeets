using Dawn;

using MediatR;

using ScheduledMeets.Business.Interfaces;
using ScheduledMeets.Business.OAuth;
using ScheduledMeets.Core;

using System.Security.Claims;

namespace ScheduledMeets.Business.UseCases.CreateUserByClaimsPrincipal
{
    class UserCreator : IRequestHandler<CreateUserByClaimsPrincipalRequest, User>
    {
        private readonly IRepository<User> _users;

        public UserCreator(IRepository<User> users) =>
            _users = Guard.Argument(users, nameof(users)).NotNull().Value;

        public async Task<User> Handle(CreateUserByClaimsPrincipalRequest request,
            CancellationToken cancellationToken)
        {
            Guard.Argument(request, nameof(request)).NotNull();

            User user = CreateUserBy(request.Principal);
            await _users.SaveAsync(user);

            return user;
        }

        private static User CreateUserBy(ClaimsPrincipal principal)
        {
            ClaimsIdentity identity = principal switch
            {
                { Identity: ClaimsIdentity claimsIdentity } => claimsIdentity,
                { Identity: null } => throw new InvalidOperationException("No primary identity."),
                _ => throw new InvalidOperationException("Unsupported identity type.")
            };

            return CreateUserBy(identity);
        }

        private static User CreateUserBy(ClaimsIdentity identity)
        {
            string username = identity.Name
                ?? throw new ApplicationException("Identity has no name.");

            return new(username)
            {
                Name = new PersonName(
                    FirstName: identity.FindFirst(ClaimType.GivenName)?.Value,
                    LastName: identity.FindFirst(ClaimType.FamilyName)?.Value),
                Email = identity.FindFirst(ClaimType.Email)?.Value,
                Nickname = identity.FindFirst(ClaimType.NickName)?.Value,
            };
        }
    }
}
