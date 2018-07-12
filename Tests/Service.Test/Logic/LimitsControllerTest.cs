using PipServices.Commons.Refer;
using PipServices.Commons.Data;

using PipServicesLimitsDotnet.Persistence;
using PipServicesLimitsDotnet.Data.Version1;
using PipServicesLimitsDotnet.Data;

using Xunit;

using System.Collections.Generic;

namespace PipServicesLimitsDotnet.Logic
{
    public class LimitsControllerTest
    {
        private LimitsController _controller;
        private LimitsMemoryPersistence _persistence;

        public LimitsControllerTest()
        {
            var references = new References();
            _controller = new LimitsController();

            _persistence = new LimitsMemoryPersistence();

            references.Put(new Descriptor("pip-services-limits-dotnet", "persistence", "memory", "*", "1.0"), _persistence);
            references.Put(new Descriptor("pip-services-limits-dotnet", "controller", "default", "*", "1.0"), _controller);

            _controller.SetReferences(references);
        }

        [Fact]
        public async void It_Should_Create_Limits_Async()
        {
            //arrange
            var limit1 = TestModel.CreateLimit1();
            var limit2 = TestModel.CreateLimit2();
            var limit3 = TestModel.CreateLimit3();

            //act
            var result1 = await _controller.CreateLimitAsync(null, limit1);
            var result2 = await _controller.CreateLimitAsync(null, limit2);
            var result3 = await _controller.CreateLimitAsync(null, limit3);

            //assert
            TestModel.AssertEqual(limit1, result1);
            TestModel.AssertEqual(limit2, result2);
            TestModel.AssertEqual(limit3, result3);
        }

        [Fact]
        public async void It_Should_Not_Create_Limit_Without_User_Id_Async()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            limit.UserId = null;

            //act
            var result = await _controller.CreateLimitAsync(null, limit);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async void It_Should_Update_Limits_Async()
        {
            //arrange
            var limit = TestModel.CreateLimit1();

            //act
            await _controller.CreateLimitAsync(null, limit);
            var result = await _controller.UpdateLimitAsync(null, limit);

            //assert
            TestModel.AssertEqual(limit, result);
        }

        [Fact]
        public async void It_Should_Delete_Limits_Async()
        {
            //arrange
            var limit = TestModel.CreateLimit1();

            //act
            await _controller.CreateLimitAsync(null, limit);
            var result = await _controller.DeleteLimitByIdAsync(null, limit.Id);

            //assert
            TestModel.AssertEqual(limit, result);
        }

        [Fact]
        public async void It_Should_Get_Limit_By_Id_Async()
        {
            //arrange
            var limit = TestModel.CreateLimit1();

            //act
            await _controller.CreateLimitAsync(null, limit);
            var result = await _controller.GetLimitByIdAsync(null, limit.Id);

            //assert
            TestModel.AssertEqual(limit, result);
        }

        [Fact]
        public async void It_Should_Get_Limit_By_User_Id_Async()
        {
            //arrange
            var limit = TestModel.CreateLimit1();

            //act
            await _controller.CreateLimitAsync(null, limit);
            var result = await _controller.GetLimitByUserIdAsync(null, limit.UserId);

            //assert
            TestModel.AssertEqual(limit, result);
        }

        [Fact]
        public async void It_Should_Get_Limits_Async()
        {
            //arrange
            var limits = new DataPage<LimitV1>()
            {
                Data = new List<LimitV1>() { TestModel.CreateLimit1(), TestModel.CreateLimit2(), TestModel.CreateLimit3() },
                Total = 3
            };
            await _controller.CreateLimitAsync(null, limits.Data[0]);
            await _controller.CreateLimitAsync(null, limits.Data[1]);
            await _controller.CreateLimitAsync(null, limits.Data[2]);
            var filter = new FilterParams();
            var paging = new PagingParams();

            //act
            var result = await _controller.GetLimitsAsync(null, filter, paging);

            //assert
            Assert.Equal(limits.Total, limits.Total);
        }

        [Fact]
        public async void It_Should_Increase_Limit()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            await _controller.CreateLimitAsync(null, limit);

            //act
            var amount = (long)(limit.Limit * 0.1);
            var result = await _controller.IncreaseLimitOfUserAsync(null, limit.UserId, amount);
            limit.Limit += amount;

            //assert
            TestModel.AssertEqual(limit, result);
        }

        [Fact]
        public async void It_Should_Decrease_Limit()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            await _controller.CreateLimitAsync(null, limit);

            //act
            var amount = (long)(limit.Limit * 0.1);
            var result = await _controller.DecreaseLimitOfUserAsync(null, limit.UserId, amount);
            limit.Limit -= amount;

            //assert
            TestModel.AssertEqual(limit, result);
        }

        [Fact]
        public async void It_Should_Not_Decrease_Limit_Below_Zero()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            await _controller.CreateLimitAsync(null, limit);

            //act
            var result1 = await _controller.DecreaseLimitOfUserAsync(null, limit.UserId, limit.Limit);
            limit.Limit = 0;
            var result2 = await _controller.DecreaseLimitOfUserAsync(null, limit.UserId, 1);

            //assert
            TestModel.AssertEqual(limit, result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void It_Should_Increase_Amount_Used()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            //CurrentAmountUsed = 5000,
            //Limit = 100000
            await _controller.CreateLimitAsync(null, limit);

            //act
            var amount = (long)(limit.Limit * 0.1);
            var result = await _controller.IncreaseAmountUsedByUserAsync(null, limit.UserId, amount);
            limit.AmountUsed += amount;

            //assert
            TestModel.AssertEqual(limit, result);
        }

        [Fact]
        public async void It_Should_Not_Increase_Amount_Used_If_Over_Limit()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            //CurrentAmountUsed = 5000,
            //Limit = 100000
            await _controller.CreateLimitAsync(null, limit);

            //act
            var result = await _controller.IncreaseAmountUsedByUserAsync(null, limit.UserId, limit.Limit);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async void It_Should_Decrease_Amount_Used()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            await _controller.CreateLimitAsync(null, limit);

            //act
            var amount = (long)(limit.AmountUsed * 0.5);
            var result = await _controller.DecreaseAmountUsedByUserAsync(null, limit.UserId, amount);
            limit.AmountUsed -= amount;

            //assert
            TestModel.AssertEqual(limit, result);
        }

        [Fact]
        public async void It_Should_Not_Decrease_Amount_Used_Below_Zero()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            await _controller.CreateLimitAsync(null, limit);

            //act
            var result1 = await _controller.DecreaseAmountUsedByUserAsync(null, limit.UserId, limit.AmountUsed);
            limit.AmountUsed = 0;
            var result2 = await _controller.DecreaseAmountUsedByUserAsync(null, limit.UserId, 1);

            //assert
            TestModel.AssertEqual(limit, result1);
            Assert.Null(result2);
        }

        [Fact]
        public async void It_Should_Get_Amount_Available_To_User()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            await _controller.CreateLimitAsync(null, limit);

            //act
            var amountAvailable = await _controller.GetAmountAvailableToUserAsync(null, limit.UserId);

            //assert
            Assert.Equal(limit.Limit - limit.AmountUsed, amountAvailable);
        }

        [Fact]
        public async void It_Should_Check_If_Amount_Can_Be_Added()
        {
            //arrange
            var limit = TestModel.CreateLimit1();
            var result = await _controller.CreateLimitAsync(null, limit);
            var amountAvailable = await _controller.GetAmountAvailableToUserAsync(null, limit.UserId);

            //act
            var cannotBeAdded1 = await _controller.CanUserAddAmountAsync(null, limit.UserId, limit.Limit);
            var canBeAdded1 = await _controller.CanUserAddAmountAsync(null, limit.UserId, amountAvailable);
            var canBeAdded2 = await _controller.CanUserAddAmountAsync(null, limit.UserId, amountAvailable/2);
            var cannotBeAdded2 = await _controller.CanUserAddAmountAsync(null, limit.UserId, (-1) * amountAvailable);

            //assert
            Assert.False(cannotBeAdded1);
            Assert.True(canBeAdded1);
            Assert.True(canBeAdded2);
            Assert.False(cannotBeAdded2);
        }

    }
}
