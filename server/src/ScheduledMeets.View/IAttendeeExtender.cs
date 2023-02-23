namespace ScheduledMeets.View;

public interface IAttendeeExtender
{
    ValueTask<string?> GetNicknameAsync(
        AttendeeView attendee,
        CancellationToken cancellationToken);
}