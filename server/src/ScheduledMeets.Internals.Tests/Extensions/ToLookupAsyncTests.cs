using ScheduledMeets.Internals.Extensions;
using ScheduledMeets.TestTools.AsyncQueryables;

namespace ScheduledMeets.Internals.Tests.Extensions;

internal class ToLookupAsyncTests
{

    [Test]
    public async Task Basic()
    {
        var values = new[] 
        { 
            new
            {
                Group = "_1_",
                Value = 1
            },
            new
            {
                Group = "_2_",
                Value = 2
            },
            new
            {
                Group = "_1_",
                Value = 11
            }
        }.AsAsyncQueryable();

        var result = await values.ToLookupAsync(_ => _.Group);

        result.Should().HaveCount(2);
        result["_1_"].Should().BeEquivalentTo(new[]
        {
            new
            {
                Group = "_1_",
                Value = 1
            },
            new
            {
                Group = "_1_",
                Value = 11
            }
        });
        result["_2_"].Should().BeEquivalentTo(new[]
        {
            new
            {
                Group = "_2_",
                Value = 2
            }
        });
    }
}
