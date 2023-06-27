using ScheduledMeets.Internals.Extensions;

namespace ScheduledMeets.View;

internal sealed class MeetExtender : IMeetExtender
{
    private readonly IByFilterReader<AttendanceView> _attendances;
    private readonly IByFilterReader<MemberView> _members;

    public MeetExtender(IByFilterReader<AttendanceView> attendances, 
        IByFilterReader<MemberView> members)
    {
        _attendances = attendances ?? throw new ArgumentNullException(nameof(attendances));
        _members = members ?? throw new ArgumentNullException(nameof(members));
    }

    public ValueTask<IReadOnlyList<MemberView>> GetAcceptingAttendeesAsync(MeetView meet,
        CancellationToken cancellationToken)
    {
        return GetAttendeesAsync(meet, AcceptanceStatusView.Accepted, cancellationToken);
    }

    public ValueTask<IReadOnlyList<MemberView>> GetDecliningAttendeesAsync(MeetView meet,
        CancellationToken cancellationToken)
    {
        return GetAttendeesAsync(meet, AcceptanceStatusView.Declined, cancellationToken);
    }

    public ValueTask<IReadOnlyList<MemberView>> GetPendingAttendeesAsync(MeetView meet,
        CancellationToken cancellationToken)
    {
        return GetAttendeesAsync(meet, AcceptanceStatusView.Maybe, cancellationToken);
    }

    public async ValueTask<IReadOnlyList<MemberView>> GetIdleAttendeesAsync(MeetView meet,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(meet);

        (IReadOnlyList<AttendanceView> attendances,
        IReadOnlyList<MemberView> members) = await
                _attendances.WithAsync(_ => _.MeetId, meet.Id, cancellationToken)
            .And(
                _members.WithAsync(_ => _.MeetsId, meet.MeetsId, cancellationToken));

        HashSet<long> memberAttendances = attendances
            .Select(_ => _.MemberId)
            .ToHashSet();

        return members
            .Where(_ => !memberAttendances.Contains(_.Id))
            .Evaluate();
    }

    private async ValueTask<IReadOnlyList<MemberView>> GetAttendeesAsync(
        MeetView meet,
        AcceptanceStatusView status,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(meet);

        (IReadOnlyList<AttendanceView> attendances, 
        IReadOnlyList<MemberView> members) = await
                _attendances.WithAsync(_ => _.MeetId, meet.Id, cancellationToken)
            .And(
                _members.WithAsync(_ => _.MeetsId, meet.MeetsId, cancellationToken));

        Dictionary<long, MemberView> keyMembers = members.ToDictionary(_ => _.Id);

        return attendances
            .Where(_ => _.Status == status)
            .Select(_ => keyMembers[_.MemberId])
            .Evaluate();
    }
}
