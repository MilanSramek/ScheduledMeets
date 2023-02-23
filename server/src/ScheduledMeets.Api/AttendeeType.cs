using ScheduledMeets.View;

namespace ScheduledMeets.GraphQL;

internal class AttendeeType : ObjectType<AttendeeView>
{
    protected override void Configure(IObjectTypeDescriptor<AttendeeView> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name("Attendee");
        descriptor.BindFieldsExplicitly();

        descriptor.Field(_ => _.Id);
        descriptor.Field(_ => _.Meets);
        descriptor.Field(_ => _.IsOwner);
    }
}
