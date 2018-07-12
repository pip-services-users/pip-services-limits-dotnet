using System.Threading.Tasks;

using PipServices.Commons.Refer;
using PipServices.Commons.Config;

using PipServicesLimitsDotnet.Persistence;
using PipServicesLimitsDotnet.Logic;
using PipServicesLimitsDotnet.Clients.Version1;
using PipServicesLimitsDotnet.Services.Version1;

using Xunit;

namespace PipServicesLimitsDotnet.Clients.Version1
{
    public class LimitsHttpClientV1Test : System.IDisposable
    {
        private LimitsMemoryPersistence _persistence;
        private LimitsController _controller;
        private LimitsHttpClientV1 _client;
        private LimitsHttpServiceV1 _service;
        private LimitsClientV1Fixture _fixture;

        public LimitsHttpClientV1Test()
        {
            _persistence = new LimitsMemoryPersistence();
            _persistence.Configure(new ConfigParams());

            _controller = new LimitsController();
            _controller.Configure(new ConfigParams());

            _client = new LimitsHttpClientV1();
            _service = new LimitsHttpServiceV1();


            ConfigParams HttpConfig = ConfigParams.FromTuples(
                "connection.protocol", "http",
                "connection.host", "localhost",
                "connection.port", 3000
            );

            _service.Configure(HttpConfig);
            _client.Configure(HttpConfig);

            _fixture = new LimitsClientV1Fixture(_client);

            IReferences references = References.FromTuples(
                new Descriptor("pip-services-limits-dotnet", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("pip-services-limits-dotnet", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("pip-services-limits-dotnet", "client", "http", "default", "1.0"), _client,
                new Descriptor("pip-services-limits-dotnet", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);
            _client.SetReferences(references);
            _service.SetReferences(references);

            _persistence.OpenAsync(null).Wait();
            _service.OpenAsync(null).Wait();
            _client.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            _client.CloseAsync(null).Wait();
            _service.CloseAsync(null).Wait();
            _persistence.CloseAsync(null).Wait();
        }

        [Fact]
        public async Task It_Should_Do_All_Operations()
        {
            await _fixture.TestAllOperations();
        }
    }
}
