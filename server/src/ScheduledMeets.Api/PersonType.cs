namespace ScheduledMeets.Api;

internal sealed class PersonType : InterfaceType
{
    protected override void Configure(IInterfaceTypeDescriptor descriptor)
    {
        base.Configure(descriptor);

        descriptor.Name("Person");

        descriptor
            .Field("Id")
            .ID();
        descriptor
            .Field("FirstName")
            .Type<StringType>();
        descriptor
            .Field("LastName")
            .Type<StringType>();
        descriptor
            .Field("VisibleName")
            .Type<NonNullType<StringType>>();
    }
}
