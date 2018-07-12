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
            ConfigParams config = ConfigParams.FromTuples(
                "collection", "limits",
                "connection.uri", "mongodb://localhost:27017/test"
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
