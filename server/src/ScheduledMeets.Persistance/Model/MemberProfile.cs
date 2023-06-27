using AutoMapper;

using ScheduledMeets.View;

namespace ScheduledMeets.Persistence.Model;

internal sealed class MemberProfile : Profile
{
    public MemberProfile()
    {
        CreateMap<MemberView, Member>();
    }
}
