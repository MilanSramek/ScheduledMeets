using ScheduledMeets.View;

namespace ScheduledMeets.GraphQL;

internal class MemberType : ObjectType<MemberView>
{
    protected override void Configure(IObjectTypeDescriptor<MemberView> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name("Attendee");
        descriptor.BindFieldsExplicitly();

        descriptor.Field(_ => _.Id);
        //descriptor.Field(_ => _.Meets);
        descriptor.Field(_ => _.IsOwner);
    }
}
