using System;
using PipServices.Commons.Config;
using System.Threading.Tasks;
using Xunit;

namespace PipServicesLimitsDotnet.Persistence
{
    // Please run local MongoDB instance and then execute these tests
    public class LimitsMongoDbPersistenceTest
    {
        public LimitsMongoDbPersistence Persistence { get; set; }
        public LimitsPersistenceFixture Fixture { get; set; }

        public LimitsMongoDbPersistenceTest()
        {
            var mongoUri = Environment.GetEnvironmentVariable("MONGO_SERVICE_URI");
            var mongoHost = Environment.GetEnvironmentVariable("MONGO_SERVICE_HOST") ?? "localhost";
            var mongoPort = Environment.GetEnvironmentVariable("MONGO_SERVICE_PORT") ?? "27017";
            var mongoDatabase = Environment.GetEnvironmentVariable("MONGO_SERVICE_DB") ?? "test";

            // Exit if mongo connection is not set
            if (mongoUri == null && mongoHost == null)
                return;

            ConfigParams config = ConfigParams.FromTuples(
                "connection.uri", mongoUri,
                "connection.host", mongoHost,
                "connection.port", mongoPort,
                "connection.database", mongoDatabase
             );

            Persistence = new LimitsMongoDbPersistence();
            Persistence.Configure(config);
            Persistence.OpenAsync(null).Wait();
            Persistence.ClearAsync(null).Wait();
            Fixture = new LimitsPersistenceFixture(Persistence);
        }

        [Fact]
        public async Task It_Should_Create_Limit()
        {
            await Fixture.TestCreateLimit();
        }

        [Fact]
        public async Task It_Should_Update_Limit()
        {
            await Fixture.TestUpdateLimit();
        }

        [Fact]
        public async Task It_Should_Get_Limit_By_Id()
        {
            await Fixture.TestGetLimitById();
        }



        [Fact]
        public async Task It_Should_Delete_Limit()
        {
            await Fixture.TestDeleteLimit();
        }
    }
}
