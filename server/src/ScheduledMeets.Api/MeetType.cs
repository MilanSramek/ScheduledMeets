using ScheduledMeets.View;

namespace ScheduledMeets.Api;

internal sealed class MeetType : ObjectType<MeetView>
{
    protected override void Configure(IObjectTypeDescriptor<MeetView> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name("Meet");
        descriptor.BindFieldsExplicitly();

        descriptor.Field(_ => _.Id);
        descriptor.Field(_ => _.From);
        descriptor.Field(_ => _.To);
    }
}
