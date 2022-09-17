using HotChocolate.Types;

namespace ScheduledMeets.GraphQL;

public class MutationsType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor
            .Field<SignInResolver>(_ => _.SignInAsync(null!, null!, null!, default));
    }
}
