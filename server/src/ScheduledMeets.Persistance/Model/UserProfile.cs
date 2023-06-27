using AutoMapper;

using ScheduledMeets.Core;
using ScheduledMeets.View;

namespace ScheduledMeets.Persistence.Model;

internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserView>(MemberList.Destination);

        CreateMap<User, PersonName>(MemberList.Destination).ReverseMap();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        CreateMap<Core.User, User>()
                .IncludeMembers(_ => _.Name)
                .ForMember(_ => _.Id, opt => opt.MapFrom(_ => _.Id))
                .ForMember(_ => _.Username, opt => opt.MapFrom(_ => _.Username))
                .ForMember(_ => _.FirstName, opt => opt.MapFrom(_ => _.Name.FirstName))
                .ForMember(_ => _.LastName, opt => opt.MapFrom(_ => _.Name.LastName))
                .ForMember(_ => _.Attendees, opt => opt.Ignore())
            .ReverseMap();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
