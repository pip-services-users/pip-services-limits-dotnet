using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;

using PipServicesLimitsDotnet.Data.Version1;
using PipServicesLimitsDotnet.Persistence;
using PipServicesLimitsDotnet.Logic;
using PipServicesLimitsDotnet.Data;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using System.Collections.Generic;

namespace PipServicesLimitsDotnet.Services.Version1
{
    public class LimitsHttpServiceV1Test : System.IDisposable
    {
        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", "3000"
        );

        private LimitsMemoryPersistence _persistence;
        private LimitsController _controller;
        private LimitsHttpServiceV1 _service;

        public LimitsHttpServiceV1Test()
        {
            _persistence = new LimitsMemoryPersistence();
            _controller = new LimitsController();
            _service = new LimitsHttpServiceV1();

            IReferences references = References.FromTuples(
                new Descriptor("pip-services-limits-dotnet", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("pip-services-limits-dotnet", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("pip-services-limits-dotnet", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _persistence.OpenAsync(null).Wait();

            _service.Configure(HttpConfig);
            _service.SetReferences(references);
            _service.OpenAsync(null).Wait();
        }

        public void Dispose()
        {
            _service.CloseAsync(null).Wait();
            _persistence.CloseAsync(null).Wait();
        }


        // Can't break them up into separate test, because they do not run async.
        [Fact]
        public async Task It_Should_Do_All_Operations()
        {
            LimitV1 expectedLimit1 = TestModel.CreateLimit1();
            LimitV1 limit1 = await Invoke<LimitV1>("create_limit", new { limit = expectedLimit1 });
            TestModel.AssertEqual(expectedLimit1, limit1);

            var expectedLimit2 = TestModel.CreateLimit2();
            var limit2 = await Invoke<LimitV1>("create_limit", new { limit = expectedLimit2 });
            TestModel.AssertEqual(expectedLimit2, limit2);

            var expectedLimit3 = TestModel.CreateLimit3();
            var limit3 = await Invoke<LimitV1>("create_limit", new { limit = expectedLimit3 });
            TestModel.AssertEqual(expectedLimit3, limit3);

            var page = await Invoke<DataPage<LimitV1>>("get_limits", new { });
            Assert.NotNull(page);
            Assert.Equal(3, page.Data.Count);

            limit1.AmountUsed = limit1.Limit/2;

            var limit = await Invoke<LimitV1>("update_limit", new { limit = limit1 });
            TestModel.AssertEqual(limit1, limit);

            limit = await Invoke<LimitV1>("get_limit_by_id", new { id = limit1.Id });
            TestModel.AssertEqual(limit1, limit);

            limit = await Invoke<LimitV1>("get_limit_by_user_id", new { user_id = limit1.UserId });
            TestModel.AssertEqual(limit1, limit);

            limit = await Invoke<LimitV1>("decrease_limit_of_user", new { decrease_by = limit1.Limit / 2, user_id = limit1.UserId });
            limit1.Limit -= limit1.Limit / 2;
            TestModel.AssertEqual(limit1, limit);

            limit = await Invoke<LimitV1>("increase_limit_of_user", new { increase_by = limit1.Limit/2, user_id = limit1.UserId });
            limit1.Limit += limit1.Limit / 2;
            TestModel.AssertEqual(limit1, limit);

            limit = await Invoke<LimitV1>("decrease_amount_used_by_user", new { decrease_by = limit1.AmountUsed / 2, user_id = limit1.UserId });
            limit1.AmountUsed -= limit1.AmountUsed / 2;
            TestModel.AssertEqual(limit1, limit);

            limit = await Invoke<LimitV1>("increase_amount_used_by_user", new { increase_by = limit1.AmountUsed / 2, user_id = limit1.UserId });
            limit1.AmountUsed += limit1.AmountUsed / 2;
            TestModel.AssertEqual(limit1, limit);

            var amount = await Invoke<long>("get_amount_available_to_user", new { user_id = limit1.UserId });
            Assert.Equal(amount, limit1.Limit - limit1.AmountUsed);

            var isIncreasable = await Invoke<bool>("can_user_add_amount", new { user_id = limit1.UserId, amount = limit1.Limit });
            Assert.False(isIncreasable);
            isIncreasable = await Invoke<bool>("can_user_add_amount", new { user_id = limit1.UserId, amount = amount });
            Assert.True(isIncreasable);

            //Delete all
            limit = await Invoke<LimitV1>("delete_limit_by_id", new { id = limit1.Id });
            Assert.NotNull(limit);
            Assert.Equal(limit1.Id, limit.Id);

            limit = await Invoke<LimitV1>("get_limit_by_id", new { id = limit1.Id });
            Assert.Null(limit);

            limit = await Invoke<LimitV1>("delete_limit_by_id", new { id = limit2.Id });
            Assert.NotNull(limit);
            Assert.Equal(limit2.Id, limit.Id);

            limit = await Invoke<LimitV1>("get_limit_by_id", new { id = limit2.Id });
            Assert.Null(limit);

            limit = await Invoke<LimitV1>("delete_limit_by_id", new { id = limit3.Id });
            Assert.NotNull(limit);
            Assert.Equal(limit3.Id, limit.Id);

            limit = await Invoke<LimitV1>("get_limit_by_id", new { id = limit3.Id });
            Assert.Null(limit);
        }

        private static async Task<T> Invoke<T>(string route, dynamic request)
        {
            using (var httpClient = new HttpClient())
            {
                var requestValue = JsonConverter.ToJson(request);
                using (var content = new StringContent(requestValue, Encoding.UTF8, "application/json"))
                {
                    var response = await httpClient.PostAsync("http://localhost:3000/v1/limits/" + route, content);
                    var responseValue = response.Content.ReadAsStringAsync().Result;
                    return JsonConverter.FromJson<T>(responseValue);
                }
            }
        }
    }
}
