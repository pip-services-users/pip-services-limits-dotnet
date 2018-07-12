using System.Threading.Tasks;

using Xunit;

namespace PipServicesLimitsDotnet.Persistence
{
    public class LimitsMemoryPersistenceTest
    {
        public LimitsMemoryPersistence Persistence { get; set; }
        public LimitsPersistenceFixture Fixture { get; set; }

        public LimitsMemoryPersistenceTest()
        {
            Persistence = new LimitsMemoryPersistence();
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




