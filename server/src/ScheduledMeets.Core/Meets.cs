using ScheduledMeets.Internals.Extensions;

namespace ScheduledMeets.Core;

public class Meets : IEntity
{
    #region Error
    static class Error
    {
        public static Exception DuplicitAttendee(long userId, long meetsId) => new InvalidOperationException(
            $"User Id={userId} is already an attendee of the meets Id={meetsId}.");

        public static Exception UnknownAttendee(long userId, long meetsId) => new InvalidOperationException(
            $"User Id={userId} is not an attendee of the meets Id={meetsId}.");

        public static Exception UnknownOwner(long userId, long meetsId) => new InvalidOperationException(
            $"User Id={userId} is not an owner of the meets Id={meetsId}.");

        public static Exception ForeignAttendee(long attendeeId, long meetsId) => new InvalidOperationException(
            $"Attedee with Id='{attendeeId}' does not participate on the meets Id='{meetsId}'.");

        public static Exception DuplicitNickname(long attendeeId, string nickname) => new InvalidOperationException(
            $"Nickname of the attendee Id='{attendeeId}' cannot be change to '{nickname}' because it is already taken.");
    }
    #endregion Error

    #region Private fields

    private readonly Dictionary<long, Attendee> _attendees;
    private readonly Dictionary<long, Attendee> _owners;

    #endregion Private fields

    public long Id { get; }
    public string? Name { get; set; }

    public IReadOnlyDictionary<long, Attendee> Owners => _owners;
    public IReadOnlyDictionary<long, Attendee> Attendees => _attendees;

    internal Meets(IEnumerable<Attendee> attendees)
    {
        ArgumentNullException.ThrowIfNull(attendees);

        attendees = attendees.Evaluate();

        IReadOnlyList<Exception>? foreignAttendeeExceptions = attendees
            .Where(_ => _.MeetsId != Id)
            .Select(attendee => Error.ForeignAttendee(attendee.Id, Id))
            .Evaluate();
        if (foreignAttendeeExceptions.Count > 0)
            throw new AggregateException(foreignAttendeeExceptions);

        _attendees = attendees.ToDictionary(_ => _.Id);
        _owners = attendees
            .Where(_ => _.IsOwner)
            .ToDictionary(_ => _.Id);
    }

    #region Logic
    public void SetAttendeesNickname(Attendee attendee, string nickname)
    {
        ArgumentNullException.ThrowIfNull(attendee);
        ArgumentException.ThrowIfNullOrEmpty(nickname);

        if (attendee.MeetsId != Id)
            throw Error.ForeignAttendee(attendee.Id, Id);

        nickname = nickname.Trim();

        if (attendee.Nickname == nickname)
            return;

        if (_attendees.Values.Any(_ => _.Nickname == nickname))
            throw Error.DuplicitNickname(attendee.Id, nickname);
    }

    public Attendee AddAttendee(long userId)
    {
        Attendee attendee = new(userId, Id);

        return _attendees.TryAdd(userId, attendee)
            ? attendee
            : throw Error.DuplicitAttendee(userId, Id);
    }

    public void RemoveAttendee(long userId)
    {
        if (!_attendees.Remove(userId))
            throw Error.UnknownAttendee(userId, Id);

        _owners.Remove(userId);
    }

    public Attendee AddOwner(long userId)
    {
        Attendee attendee = _attendees.TryGetValue(userId, out Attendee? attendee_)
            ? attendee_
            : AddAttendee(userId);

        attendee.IsOwner = true;
        _owners.TryAdd(userId, attendee);

        return attendee;
    }

    public void RemoveOwner(long userId)
    {
        if (!_owners.Remove(userId, out Attendee? attendee))
            throw Error.UnknownOwner(userId, Id);

        attendee.IsOwner = false;
    }
    #endregion Logic
}