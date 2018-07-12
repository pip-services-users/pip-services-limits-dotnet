using System;
using System.IO;
using System.Reflection;

using System.Threading.Tasks;
using PipServices.Commons.Config;
using Xunit;

namespace PipServicesLimitsDotnet.Persistence
{
    public class LimitsFilePersistenceTest
    {
        public LimitsFilePersistence Persistence { get; set; }
        public LimitsPersistenceFixture Fixture { get; set; }

        public LimitsFilePersistenceTest()
        {
            Persistence = new LimitsFilePersistence();
            var config = new ConfigParams();
            config.Add("path", "./Data/beacons.test.json");
            Persistence.Configure(config);
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