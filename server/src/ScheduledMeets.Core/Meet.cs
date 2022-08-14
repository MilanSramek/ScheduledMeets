using ScheduledMeets.Internals;

namespace ScheduledMeets.Core;

public class Meet : IEntity
{
    #region Error
    static class Error
    {
        public static Exception ForeignAttendee(long attendeeId, long meetsId) => new InvalidOperationException(
            $"Attedee with Id='{attendeeId}' does not participate on the meets Id='{meetsId}'.");
    }
    #endregion Error

    #region Private fields

    private readonly HashSet<long> _acceptingAttendeeIds = new();
    private readonly HashSet<long> _decliningAttendeeIds = new();

    #endregion Private fields

    public Meet(long meetsId)
    {
        MeetsId = meetsId;
    }

    public long Id { get; }
    public long MeetsId { get; }

    public Interval<DateTime> Period { get; set; }

    public IReadOnlySet<long> AcceptingAttendeeIds => _acceptingAttendeeIds;
    public IReadOnlySet<long> DecliningAttendeeIds => _decliningAttendeeIds;

    #region Logic

    public void AcceptAttendee(Attendee attendee)
    {
        ArgumentNullException.ThrowIfNull(attendee);

        if (attendee.MeetsId != MeetsId) 
            throw Error.ForeignAttendee(attendee.Id, MeetsId);

        _acceptingAttendeeIds.Add(attendee.Id);
        _decliningAttendeeIds.Remove(attendee.Id);
    }

    public void DeclineAttendee(Attendee attendee)
    {
        ArgumentNullException.ThrowIfNull(attendee);

        if (attendee.MeetsId != MeetsId)
            throw Error.ForeignAttendee(attendee.Id, MeetsId);

        _decliningAttendeeIds.Add(attendee.Id);
        _acceptingAttendeeIds.Remove(attendee.Id);
    }

    #endregion Logic
}
