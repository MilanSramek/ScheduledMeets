using ScheduledMeets.View;

internal class UserExtender : IUserExtender
{
    private readonly IReader<MeetsView> _meets;

    public UserExtender(IReader<MeetsView> meets)
    {
        _meets = meets ?? throw new ArgumentNullException(nameof(meets));
    }

    public IQueryable<MeetsView> GetMeets(UserView user) => _meets
        .Where(_ => _.Attendees.Any(attendee => attendee.UserId == user.Id));
}
