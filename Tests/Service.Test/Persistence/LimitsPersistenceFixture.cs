using PipServices.Commons.Data;

using System.Threading.Tasks;

using Xunit;

using PipServicesLimitsDotnet.Data.Version1;
using PipServicesLimitsDotnet.Data;

namespace PipServicesLimitsDotnet.Persistence
{
    public class LimitsPersistenceFixture
    {
        private ILimitsPersistence _persistence;

        public LimitsPersistenceFixture(ILimitsPersistence persistence)
        {
            _persistence = persistence;
            //TODO: needed?
            //_persistence.ClearAsync(); // required for database persistence to have clean environment
        }

        public async Task TestCreateLimit()
        {
            // arrange 
            var limit = TestModel.CreateLimit1();

            // act
            var result = await _persistence.CreateAsync(null, limit);

            // assert
            TestModel.AssertEqual(limit, result);
        }

        public async Task TestUpdateLimit()
        {
            // arrange 
            var limit = await _persistence.CreateAsync(null, TestModel.CreateLimit1());

            // act
            limit.Limit = limit.Limit * 10;

            var result = await _persistence.UpdateAsync(null, limit);

            // assert
            TestModel.AssertEqual(limit, result);
        }

        public async Task TestGetLimitById()
        {
            // arrange 
            var limit = await _persistence.CreateAsync(null, TestModel.CreateLimit1());

            // act
            var result = await _persistence.GetOneByIdAsync(null, limit.Id);

            // assert
            TestModel.AssertEqual(limit, result);
        }

        public async Task TestDeleteLimit()
        {
            // arrange 
            var limit = await _persistence.CreateAsync(null, TestModel.CreateLimit1());

            // act
            var deletedLimit = await _persistence.DeleteByIdAsync(null, limit.Id);
            var result = await _persistence.GetOneByIdAsync(null, limit.Id);

            // assert
            TestModel.AssertEqual(limit, deletedLimit);
            Assert.Null(result);
        }
    }
}