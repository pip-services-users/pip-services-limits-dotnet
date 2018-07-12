using PipServicesLimitsDotnet.Data.Version1;

using PipServices.Commons.Data;
using System.Threading.Tasks;

namespace PipServicesLimitsDotnet.Logic
{
    public interface ILimitsController
    {
        Task<DataPage<LimitV1>> GetLimitsAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<LimitV1> GetLimitByIdAsync(string correlationId, string id);
        Task<LimitV1> GetLimitByUserIdAsync(string correlationId, string userId);

        Task<LimitV1> CreateLimitAsync(string correlationId, LimitV1 limit);
        Task<LimitV1> UpdateLimitAsync(string correlationId, LimitV1 limit);
        Task<LimitV1> DeleteLimitByIdAsync(string correlationId, string id);

        Task<LimitV1> IncreaseLimitOfUserAsync(string correlationId, string userId, long increaseBy);
        Task<LimitV1> DecreaseLimitOfUserAsync(string correlationId, string userId, long decreaseBy);
        Task<LimitV1> IncreaseAmountUsedByUserAsync(string correlationId, string userId, long increaseBy);
        Task<LimitV1> DecreaseAmountUsedByUserAsync(string correlationId, string userId, long decreaseBy);
        Task<bool> CanUserAddAmountAsync(string correlationId, string userId, long amount);
        Task<long> GetAmountAvailableToUserAsync(string correlationId, string userId);
    }
}