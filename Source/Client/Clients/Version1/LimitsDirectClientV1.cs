using PipServicesLimitsDotnet.Data.Version1;
using PipServicesLimitsDotnet.Logic;

using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using PipServices.Net.Direct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServicesLimitsDotnet.Clients.Version1
{
    public class LimitsDirectClientV1 : DirectClient<ILimitsController>, ILimitsClientV1
    {

        public LimitsDirectClientV1()
            : base()
        {
            _dependencyResolver.Put("controller", new Descriptor("pip-services-limits-dotnet", "controller", "*", "*", "1.0"));
        }

        public async Task<DataPage<LimitV1>> GetLimitsAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            var timing = Instrument(correlationId, "limits.get_limits");
            var result = await _controller.GetLimitsAsync(correlationId, filter, paging);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> GetLimitByIdAsync(string correlationId, string id)
        {
            var timing = Instrument(correlationId, "limits.get_limit_by_id");
            var result = await _controller.GetLimitByIdAsync(correlationId, id);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> GetLimitByUserIdAsync(string correlationId, string userId)
        {
            var timing = Instrument(correlationId, "limits.get_limit_by_user_id");
            var result = await _controller.GetLimitByUserIdAsync(correlationId, userId);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> CreateLimitAsync(string correlationId, LimitV1 limit)
        {
            var timing = Instrument(correlationId, "limits.create_limit");
            var result = await _controller.CreateLimitAsync(correlationId, limit);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> UpdateLimitAsync(string correlationId, LimitV1 limit)
        {
            var timing = Instrument(correlationId, "limits.update_limit");
            var result = await _controller.UpdateLimitAsync(correlationId, limit);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> DeleteLimitByIdAsync(string correlationId, string id)
        {
            var timing = Instrument(correlationId, "limits.delete_limit_by_id");
            var result = await _controller.DeleteLimitByIdAsync(correlationId, id);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> IncreaseLimitOfUserAsync(string correlationId, string userId, long increaseBy)
        {
            var timing = Instrument(correlationId, "limits.increase_limit_of_user");
            var result = await _controller.IncreaseLimitOfUserAsync(correlationId, userId, increaseBy);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> DecreaseLimitOfUserAsync(string correlationId, string userId, long decreaseBy)
        {
            var timing = Instrument(correlationId, "limits.decrease_limit_of_user");
            var result = await _controller.DecreaseLimitOfUserAsync(correlationId, userId, decreaseBy);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> IncreaseAmountUsedByUserAsync(string correlationId, string userId, long increaseBy)
        {
            var timing = Instrument(correlationId, "limits.increase_amount_used_by_user");
            var result = await _controller.IncreaseAmountUsedByUserAsync(correlationId, userId, increaseBy);
            timing.EndTiming();
            return result;
        }

        public async Task<LimitV1> DecreaseAmountUsedByUserAsync(string correlationId, string userId, long decreaseBy)
        {
            var timing = Instrument(correlationId, "limits.decrease_amount_used_by_user");
            var result = await _controller.DecreaseAmountUsedByUserAsync(correlationId, userId, decreaseBy);
            timing.EndTiming();
            return result;
        }

        public async Task<ResultV1> CanUserAddAmountAsync(string correlationId, string userId, long amount)
        {
            var timing = Instrument(correlationId, "limits.can_user_add_amount");
            var result = await _controller.CanUserAddAmountAsync(correlationId, userId, amount);
            timing.EndTiming();
            return result;
        }

        public async Task<ResultV1> GetAmountAvailableToUserAsync(string correlationId, string userId)
        {
            var timing = Instrument(correlationId, "limits.get_amount_available_to_user");
            var result = await _controller.GetAmountAvailableToUserAsync(correlationId, userId);
            timing.EndTiming();
            return result;
        }
    }
}