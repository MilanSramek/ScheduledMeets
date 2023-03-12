using ScheduledMeets.View;

namespace ScheduledMeets.Api;

internal class UserType : ObjectType<UserView>
{
    protected override void Configure(IObjectTypeDescriptor<UserView> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name("User");

        descriptor.BindFieldsExplicitly();

        descriptor.Field(_ => _.Id);
        descriptor.Field(_ => _.Username).IsProjected();
        descriptor.Field(_ => _.FirstName);
        descriptor.Field(_ => _.LastName);
        descriptor.Field(_ => _.Nickname);
        descriptor.Field(_ => _.Email);
        //descriptor.Field(_ => _.Attendees);
    }
}