using ScheduledMeets.View;

namespace ScheduledMeets.Api;

internal class WithIdExtension : ObjectTypeExtension<IWithId>
{
    protected override void Configure(IObjectTypeDescriptor<IWithId> descriptor)
    {
        base.Configure(descriptor);

        descriptor.ImplementsNode()
            .IdField(_ => _.Id);
        descriptor.Field(_ => _.Id)
            .ID();
    }
}