using PipServicesLimitsDotnet.Data.Version1;

using PipServices.Commons.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServicesLimitsDotnet.Clients.Version1
{
    public class LimitsNullClientV1 : ILimitsClientV1
    {
       
        public async Task<DataPage<LimitV1>> GetLimitsAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return new DataPage<LimitV1>(new List<LimitV1>(), 0);
        }
                  
        public Task<LimitV1> GetLimitByIdAsync(string correlationId, string id)
        {
            return null;
        }

        public Task<LimitV1> GetLimitByUdiAsync(string correlationId, string udi)
        {
            return null;
        }

        public Task<LimitV1> CreateLimitAsync(string correlationId, LimitV1 limit)
        {
            return null;
        }

        public Task<LimitV1> UpdateLimitAsync(string correlationId, LimitV1 limit)
        {
            return null;
        }

        public Task<LimitV1> DeleteLimitByIdAsync(string correlationId, string id)
        {
            return null;
        }

        public Task<LimitV1> GetLimitByUserIdAsync(string correlationId, string userId)
        {
            return null;
        }

        public Task<LimitV1> IncreaseLimitOfUserAsync(string correlationId, string userId, long increaseBy)
        {
            return null;
        }

        public Task<LimitV1> DecreaseLimitOfUserAsync(string correlationId, string userId, long decreaseBy)
        {
            return null;
        }

        public Task<LimitV1> IncreaseAmountUsedByUserAsync(string correlationId, string userId, long increaseBy)
        {
            return null;
        }

        public Task<LimitV1> DecreaseAmountUsedByUserAsync(string correlationId, string userId, long decreaseBy)
        {
            return null;
        }

        public Task<ResultV1> CanUserAddAmountAsync(string correlationId, string userId, long amount)
        {
            return null;
        }

        public Task<ResultV1> GetAmountAvailableToUserAsync(string correlationId, string userId)
        {
            return null;
        }
    }
}