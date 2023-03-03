using AutoMapper;

using ScheduledMeets.View;

namespace ScheduledMeets.Persistance.Model;

internal sealed class AttendeeProfile : Profile
{
    public AttendeeProfile()
    {
        CreateMap<AttendeeView, Attendee>();
    }
}
