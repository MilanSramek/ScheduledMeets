using AutoMapper;

using ScheduledMeets.View;

namespace ScheduledMeets.Persistance.Model;

internal class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserView, User>().ReverseMap();
    }
}
