using IdentityModel.Client;

using NUnit.Framework;

using System.Net;

namespace ScheduledMeets.Internals.Tests
{
    public class Test
    {
        [Test]
        public async Task TestA()
        {
            HttpResponseMessage response = new(HttpStatusCode.NotFound);
            DiscoveryDocumentResponse res = await ProtocolResponse
                .FromHttpResponseAsync<DiscoveryDocumentResponse>(response, "Bla");

        }
    }
}
