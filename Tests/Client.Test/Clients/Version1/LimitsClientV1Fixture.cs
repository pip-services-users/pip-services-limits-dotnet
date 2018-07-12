using PipServicesLimitsDotnet.Data.Version1;
using PipServicesLimitsDotnet.Data;

using Xunit;
using System.Threading.Tasks;

using PipServices.Commons.Data;

namespace PipServicesLimitsDotnet.Clients.Version1
{
    

    public class LimitsClientV1Fixture
    {
        private ILimitsClientV1 _client;

        public LimitsClientV1Fixture(ILimitsClientV1 client){
            Assert.NotNull(client);
            _client = client;
        }

        //This part is pretty much identical to LimitsPersistenceFixute
        private async Task TestCreateLimits()
        {
            // arrange 
            var limit1 = TestModel.CreateLimit1();
            var limit2 = TestModel.CreateLimit2();
            var limit3 = TestModel.CreateLimit3();

            // act
            var result1 = await _client.CreateLimitAsync(null, limit1);
            var result2 = await _client.CreateLimitAsync(null, limit2);
            var result3 = await _client.CreateLimitAsync(null, limit3);

            // assert
            TestModel.AssertEqual(limit1, result1);
            TestModel.AssertEqual(limit2, result2);
            TestModel.AssertEqual(limit3, result3);
        }

        public async Task TestAllOperations()
        {
            // arrange 
            await TestCreateLimits();
            var page = await _client.GetLimitsAsync(null, new FilterParams(), new PagingParams());
            var limit1 = page.Data[0];
            var limit2 = page.Data[1];

            // act
            limit1.Limit = (long)(limit1.Limit * 1.5);
            var result1 = await _client.UpdateLimitAsync(null, limit1);
            // assert
            TestModel.AssertEqual(limit1, result1);



            // act
            var result2 = await _client.GetLimitByUserIdAsync(null, limit1.UserId);
            // assert
            TestModel.AssertEqual(limit1, result2);



            // act
            var idFilter = await _client.GetLimitsAsync(null, FilterParams.FromTuples("id", "1"), new PagingParams());
            var userIdFilter = await _client.GetLimitsAsync(null, FilterParams.FromTuples("user_id", "00002"), new PagingParams());
            var idsFilter = await _client.GetLimitsAsync(null, FilterParams.FromTuples("ids", "1,2"), new PagingParams());
            var userIdsFilter = await _client.GetLimitsAsync(null, FilterParams.FromTuples("user_ids", "00002,00003"), new PagingParams());
            // assert
            Assert.Equal(1, idFilter.Data.Count);
            Assert.Equal(1, userIdFilter.Data.Count);
            Assert.Equal(2, idsFilter.Data.Count);
            Assert.Equal(2, userIdsFilter.Data.Count);


            // act
            var result = await _client.IncreaseLimitOfUserAsync(null, limit2.UserId, (long)(limit2.Limit * 0.5));
            limit2.Limit += (long)(limit2.Limit * 0.5);
            // assert
            Assert.Equal(limit2.Limit, result.Limit);

            // act
            result = await _client.DecreaseLimitOfUserAsync(null, limit2.UserId, (long)(limit2.Limit * 0.5));
            limit2.Limit -= (long)(limit2.Limit * 0.5);
            // assert
            TestModel.AssertEqual(limit2, result);

            // act
            result = await _client.IncreaseAmountUsedByUserAsync(null, limit2.UserId, limit2.AmountUsed);
            limit2.AmountUsed *= 2;
            // assert
            TestModel.AssertEqual(limit2, result);

            // act
            result = await _client.DecreaseAmountUsedByUserAsync(null, limit2.UserId, limit2.AmountUsed/2);
            limit2.AmountUsed /= 2;
            // assert
            TestModel.AssertEqual(limit2, result);

            // act
            var amountAvailable = (await _client.GetAmountAvailableToUserAsync(null, limit2.UserId));
            // assert
            Assert.Equal(amountAvailable.longResult, limit2.Limit - limit2.AmountUsed);

            // act
            var trueResult = await _client.CanUserAddAmountAsync(null, limit2.UserId, amountAvailable.longResult);
            var falseResult = await _client.CanUserAddAmountAsync(null, limit2.UserId, amountAvailable.longResult + 1);
            // assert
            Assert.True(trueResult.boolResult);
            Assert.False(falseResult.boolResult);

            // act
            var result3 = await _client.DeleteLimitByIdAsync(null, limit1.Id);
            var result4 = await _client.GetLimitByIdAsync(null, limit1.Id);
            // assert
            TestModel.AssertEqual(limit1, result3);
            Assert.Null(result4);
        }


        //This part is pretty much identical to LimitsControllerTest
        public async Task TestModifyAmountAndLimit()
        {
            await TestCreateLimits();
            var page = await _client.GetLimitsAsync(null, new FilterParams(), new PagingParams());

        }
    }
}