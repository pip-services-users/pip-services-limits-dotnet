using PipServicesLimitsDotnet.Data.Version1;

using PipServices.Commons.Convert;
using PipServices.Commons.Validate;
using PipServices.Commons.Data;
using PipServices.Net.Rest;

using System.Threading.Tasks;

namespace PipServicesLimitsDotnet.Clients.Version1
{
    public class LimitsHttpClientV1 : CommandableHttpClient, ILimitsClientV1
    {

        public LimitsHttpClientV1()
            : base("v1/limits")
        {
        }

        public async Task<DataPage<LimitV1>> GetLimitsAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await CallCommandAsync<DataPage<LimitV1>>(
                "get_limits",
                correlationId,
                new
                {
                    filter = filter,
                    paging = paging
                }
            );
        }

        public async Task<LimitV1> GetLimitByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<LimitV1>(
                "get_limit_by_id",
                correlationId,
                new
                {
                    id = id
                }
            );
        }

        public async Task<LimitV1> GetLimitByUserIdAsync(string correlationId, string userId)
        {
            return await CallCommandAsync<LimitV1>(
                "get_limit_by_user_id",
                correlationId,
                new
                {
                    user_id = userId
                }
            );
        }

        public async Task<LimitV1> CreateLimitAsync(string correlationId, LimitV1 limit)
        {
            return await CallCommandAsync<LimitV1>(
                "create_limit",
                correlationId,
                new
                {
                    limit = limit
                }
            );
        }

        public async Task<LimitV1> UpdateLimitAsync(string correlationId, LimitV1 limit)
        {
            return await CallCommandAsync<LimitV1>(
                "update_limit",
                correlationId,
                new
                {
                    limit = limit
                }
            );
        }

        public async Task<LimitV1> DeleteLimitByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<LimitV1>(
                "delete_limit_by_id",
                correlationId,
                new
                {
                    id = id
                }
            );
        }

        public async Task<LimitV1> IncreaseLimitOfUserAsync(string correlationId, string userId, long increaseBy)
        {
            return await CallCommandAsync<LimitV1>(
                "increase_limit_of_user",
                correlationId,
                new
                {
                    user_id = userId,
                    increase_by = increaseBy
                }
            );
        }

        public async Task<LimitV1> DecreaseLimitOfUserAsync(string correlationId, string userId, long decreaseBy)
        {
            return await CallCommandAsync<LimitV1>(
                "decrease_limit_of_user",
                 correlationId,
                 new
                 {
                     user_id = userId,
                     decrease_by = decreaseBy
                 }
             );
        }

        public async Task<LimitV1> IncreaseAmountUsedByUserAsync(string correlationId, string userId, long increaseBy)
        {
            return await CallCommandAsync<LimitV1>(
                "increase_amount_used_by_user",
                correlationId,
                new
                {
                    user_id = userId,
                    increase_by = increaseBy
                }
            );
        }

        public async Task<LimitV1> DecreaseAmountUsedByUserAsync(string correlationId, string userId, long decreaseBy)
        {
            return await CallCommandAsync<LimitV1>(
                "decrease_amount_used_by_user",
                 correlationId,
                 new
                 {
                     user_id = userId,
                     decrease_by = decreaseBy
                 }
             );
        }

        public async Task<ResultV1> CanUserAddAmountAsync(string correlationId, string userId, long amount)
        {
            return await CallCommandAsync<ResultV1>(
                "can_user_add_amount",
                correlationId,
                new
                {
                    user_id = userId,
                    amount = amount
                }
            );
        }

        public async Task<ResultV1> GetAmountAvailableToUserAsync(string correlationId, string userId)
        {
            return await CallCommandAsync<ResultV1>(
                "get_amount_available_to_user",
                correlationId,
                new
                {
                    user_id = userId
                }
            );
        }
    }
}