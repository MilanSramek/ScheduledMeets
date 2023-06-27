namespace ScheduledMeets.View;

public interface IUserExtender
{
    IQueryable<MeetsView> GetMeets(UserView user);
}
