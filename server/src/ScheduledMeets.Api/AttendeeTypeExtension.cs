using ScheduledMeets.View;

namespace ScheduledMeets.Api;

[ExtendObjectType<AttendeeView>(IncludeStaticMembers = true)]
internal class AttendeeTypeExtension
{
    public  static ValueTask<string?> GetNicknameAsync(
        [Parent]AttendeeView attendee,
        [Service]IAttendeeExtender attendeeExtender,
        CancellationToken cancellationToken)
    {
        return attendeeExtender.GetNicknameAsync(attendee, cancellationToken);
    }
}