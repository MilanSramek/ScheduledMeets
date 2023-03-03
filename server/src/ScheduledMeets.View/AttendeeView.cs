﻿namespace ScheduledMeets.View;

public class AttendeeView : IWithId
{
    public long Id { get; set; }
    internal string? Nickname { get; set; }

    public long UserId { get; set; }

    public long MeetsId { get; set; }
    public bool IsOwner { get; set; }
}
