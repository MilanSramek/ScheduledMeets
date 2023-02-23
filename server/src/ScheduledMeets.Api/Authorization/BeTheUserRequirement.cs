using Microsoft.AspNetCore.Authorization;

namespace ScheduledMeets.Api.Authorization;

internal record BeTheUserRequirement : IAuthorizationRequirement;
