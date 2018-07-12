using System.Threading.Tasks;

using PipServices.Commons.Refer;
using PipServices.Commons.Config;

using PipServicesLimitsDotnet.Persistence;
using PipServicesLimitsDotnet.Logic;
using PipServicesLimitsDotnet.Clients.Version1;

using Xunit;

namespace PipServicesLimitsDotnet.Clients.Version1
{
    public class LimitsDirectClientV1Test : System.IDisposable
    {

        private LimitsMemoryPersistence _persistence;
        private LimitsController _controller;
        private LimitsDirectClientV1 _client;
        private LimitsClientV1Fixture _fixture;

        public LimitsDirectClientV1Test()
        {
            _persistence = new LimitsMemoryPersistence();
            _persistence.Configure(new ConfigParams());

            _controller = new LimitsController();
            _controller.Configure(new ConfigParams());

            _client = new LimitsDirectClientV1();

            IReferences references = References.FromTuples(
                new Descriptor("pip-services-limits-dotnet", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("pip-services-limits-dotnet", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("pip-services-limits-dotnet", "client", "direct", "default", "1.0"), _client
            );

            _controller.SetReferences(references);

            _client.SetReferences(references);

            _fixture = new LimitsClientV1Fixture(_client);

            _persistence.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            _persistence.CloseAsync(null).Wait();
        }

        [Fact]
        public async Task It_Should_Do_All_Operations()
        {
            await _fixture.TestAllOperations();
        }
    }
}