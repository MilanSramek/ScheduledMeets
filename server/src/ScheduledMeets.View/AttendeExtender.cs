namespace ScheduledMeets.View;

internal class AttendeExtender : IAttendeeExtender
{
    private readonly IWithIdReader<UserView> _userReader;

    public AttendeExtender(IWithIdReader<UserView> userReader)
    {
        _userReader = userReader ?? throw new ArgumentNullException(nameof(userReader));
    }

    public async ValueTask<string?> GetNicknameAsync(
        AttendeeView attendee,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(attendee);

        if (attendee is { Nickname: string nickname })
            return nickname;

        UserView user = await _userReader.ReadAsync(attendee.UserId, cancellationToken);
        return user.Nickname;
    }
}