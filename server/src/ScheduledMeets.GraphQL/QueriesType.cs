using HotChocolate.Types;

namespace ScheduledMeets.GraphQL;

public class QueriesType : ObjectType
{
    protected override void Configure(IObjectTypeDescriptor descriptor)
    {
        descriptor.Authorize();
        descriptor.Field("Sentence")
            .Resolve(_ =>
            {
                var user = _.GetUser();
                return "Ahoj světe!";
            })
            .Authorize();
    }
}
