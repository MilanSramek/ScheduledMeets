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
        private readonly IUnitOfWork _unitOfWork;

        public UserCreator(IRepository<User> users, IUnitOfWork unitOfWork)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<User> Handle(CreateUserByClaimsPrincipalRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            User user = CreateUserBy(request.ClaimsPrincipal);
            await _users.SaveAsync(user, cancellationToken);

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
