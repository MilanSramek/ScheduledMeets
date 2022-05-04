using IdentityModel;

namespace ScheduledMeets.Business.OAuth
{
    static class ClaimType
    {
        public const string Issuer = JwtClaimTypes.Issuer;
        public const string Subject = JwtClaimTypes.Subject;
        public const string Email = JwtClaimTypes.Email;
        public const string EmailVerified = JwtClaimTypes.EmailVerified;
        public const string NickName = JwtClaimTypes.NickName;
        public const string Name = JwtClaimTypes.Name;
        public const string GivenName = JwtClaimTypes.GivenName;
        public const string FamilyName = JwtClaimTypes.FamilyName;
    }
}
