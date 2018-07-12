using PipServices.Commons.Data;
using PipServicesLimitsDotnet.Data.Version1;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PipServicesLimitsDotnet.Clients.Version1 {
    
    public interface ILimitsClientV1 {

        Task<DataPage<LimitV1>> GetLimitsAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<LimitV1> GetLimitByIdAsync(string correlationId, string id);
        Task<LimitV1> GetLimitByUserIdAsync(string correlationId, string userId);

        Task<LimitV1> CreateLimitAsync(string correlationId, LimitV1 beacon);
        Task<LimitV1> UpdateLimitAsync(string correlationId, LimitV1 beacon);
        Task<LimitV1> DeleteLimitByIdAsync(string correlationId, string id);

        Task<LimitV1> IncreaseLimitOfUserAsync(string correlationId, string userId, long increaseBy);
        Task<LimitV1> DecreaseLimitOfUserAsync(string correlationId, string userId, long decreaseBy);
        Task<LimitV1> IncreaseAmountUsedByUserAsync(string correlationId, string userId, long increaseBy);
        Task<LimitV1> DecreaseAmountUsedByUserAsync(string correlationId, string userId, long decreaseBy);
        Task<ResultV1> CanUserAddAmountAsync(string correlationId, string userId, long amount);
        Task<ResultV1> GetAmountAvailableToUserAsync(string correlationId, string userId);
    }  
}