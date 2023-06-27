using ScheduledMeets.View;

namespace ScheduledMeets.Api;

[ExtendObjectType<MeetView>]
internal sealed class MeetTypeExtensions
{
    public Task<IEnumerable<MemberView>> GetAcceptingAttendeesAsync(
        [Parent]MeetView meet,
        [Service]IReader<MemberView> attendees)
    {
        throw new NotImplementedException();
    }
}
