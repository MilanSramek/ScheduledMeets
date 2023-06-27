namespace ScheduledMeets.View;

public interface IMeetExtender
{
    ValueTask<IReadOnlyList<MemberView>> GetAcceptingAttendeesAsync(MeetView meet,
        CancellationToken cancellationToken);
    ValueTask<IReadOnlyList<MemberView>> GetDecliningAttendeesAsync(MeetView meet,
        CancellationToken cancellationToken);
    ValueTask<IReadOnlyList<MemberView>> GetIdleAttendeesAsync(MeetView meet,
        CancellationToken cancellationToken);
}
