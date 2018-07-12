using PipServicesLimitsDotnet.Data.Version1;
using PipServicesLimitsDotnet.Persistence;

using PipServices.Commons.Commands;
using PipServices.Commons.Config;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServicesLimitsDotnet.Logic
{
    public class LimitsController : ILimitsController, IConfigurable, IReferenceable, ICommandable
    {
        private ILimitsPersistence _persistence;
        private LimitsCommandSet _commandSet;

        public void Configure (ConfigParams config){
            
        }

        public void SetReferences(IReferences references)
        {
            this._persistence = references.GetOneRequired<ILimitsPersistence>(
                new Descriptor("pip-services-limits-dotnet", "persistence", "*", "*", "1.0")
            );
        }

        public CommandSet GetCommandSet()
        {
            return this._commandSet ?? (this._commandSet = new LimitsCommandSet(this));
        }

        public async Task<DataPage<LimitV1>> GetLimitsAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await this._persistence.GetPageByFilterAsync(correlationId, filter, paging);
        }

        public async Task<LimitV1> GetLimitByIdAsync(string correlationId, string limitId)
        {
            return await this._persistence.GetOneByIdAsync(correlationId, limitId);
        }

        public async Task<LimitV1> GetLimitByUserIdAsync(string correlationId, string userId)
        {
            return await this._persistence.GetOneByUserIdAsync(correlationId, userId);
        }
  
        public async Task<LimitV1> CreateLimitAsync(string correlationId, LimitV1 limit)
        {
            if (string.IsNullOrEmpty(limit.UserId))
                return null;
            limit.Id = limit.Id ?? IdGenerator.NextLong();
            var result = await this._persistence.CreateAsync(correlationId, limit);
            return result;
        }

        public async Task<LimitV1> UpdateLimitAsync(string correlationId, LimitV1 limit)
        {
            if (string.IsNullOrEmpty(limit.UserId))
                return null;
            return await this._persistence.UpdateAsync(correlationId, limit);
        }

        public async Task<LimitV1> DeleteLimitByIdAsync(string correlationId, string id)
        {
            return await this._persistence.DeleteByIdAsync(correlationId, id);
        }



        public async Task<LimitV1> IncreaseLimitOfUserAsync(string correlationId, string userId, long increaseBy)
        {
            var result = await this._persistence.GetOneByUserIdAsync(correlationId, userId);
            if (result == null || increaseBy < 0)
                return null;
            
            result.Limit += increaseBy;

            return await this._persistence.UpdateAsync(correlationId, result);
        }

        public async Task<LimitV1> DecreaseLimitOfUserAsync(string correlationId, string userId, long decreaseBy)
        {
            var result = await this._persistence.GetOneByUserIdAsync(correlationId, userId);
            if (result == null)
                return null;
            if (decreaseBy < 0)
                decreaseBy = 0 - decreaseBy;

            result.Limit -= decreaseBy;
            if (result.Limit < 0)
                return null;

            return await this._persistence.UpdateAsync(correlationId, result);
        }

        public async Task<LimitV1> IncreaseAmountUsedByUserAsync(string correlationId, string userId, long increaseBy)
        {
            var result = await this._persistence.GetOneByUserIdAsync(correlationId, userId);
            if (result == null || increaseBy < 0)
                return null;

            result.AmountUsed += increaseBy;
            if (result.AmountUsed > result.Limit)
                return null;

            return await this._persistence.UpdateAsync(correlationId, result);
        }

        public async Task<LimitV1> DecreaseAmountUsedByUserAsync(string correlationId, string userId, long decreaseBy)
        {
            var result = await this._persistence.GetOneByUserIdAsync(correlationId, userId);
            if (result == null)
                return null;
            if (decreaseBy < 0)
                decreaseBy = 0 - decreaseBy;

            result.AmountUsed -= decreaseBy;
            if (result.AmountUsed < 0)
                return null;

            return await this._persistence.UpdateAsync(correlationId, result);
        }

        public async Task<long> GetAmountAvailableToUserAsync(string correlationId, string userId)
        {
            var result = await this._persistence.GetOneByUserIdAsync(correlationId, userId);
            if (result == null)
                return 0;

            return result.Limit - result.AmountUsed;
        }

        public async Task<bool> CanUserAddAmountAsync(string correlationId, string userId, long amount)
        {
            var result = await this._persistence.GetOneByUserIdAsync(correlationId, userId);
            if (result == null || amount < 0)
                return false;

            return (result.AmountUsed + amount) <= result.Limit;
        }
    }

   
}

/*





}
 */