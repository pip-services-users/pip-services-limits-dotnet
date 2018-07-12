using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using PipServices.Commons.Data;
using PipServices.Data.Memory;

using PipServicesLimitsDotnet.Data.Version1;

namespace PipServicesLimitsDotnet.Persistence
{
    
    public class LimitsMemoryPersistence : IdentifiableMemoryPersistence<LimitV1, String>, ILimitsPersistence
    {
        public LimitsMemoryPersistence() : base ()
        {
            this._maxPageSize = 1000;
        }

        public async Task<LimitV1> GetOneByUserIdAsync(string correlationId, string userId) 
        {
            _lock.EnterReadLock();

            try
            {
                var item = _items.FirstOrDefault(x => x.UserId.Equals(userId));

                if (item != null)
                    _logger.Trace(correlationId, $"Found limit of user with id {userId}");
                else
                    _logger.Trace(correlationId, $"Cannot find limit of user with id {userId}");

                return await Task.FromResult(item);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private List<Func<LimitV1, bool>> ComposeFilter(FilterParams filter)
        {
            filter = filter ?? new FilterParams();

            var id = filter.GetAsNullableString("id");
            var userId = filter.GetAsNullableString("user_id");
            var idsString = filter.GetAsNullableString("ids");
            var userIdsString = filter.GetAsNullableString("user_ids");
            string[] ids = null;
            string[] userIds = null;
            if (idsString != null && idsString is string)
                ids = idsString.Split(',');
            if (userIdsString != null && userIdsString is string)
                userIds = userIdsString.Split(',');

            return new List<Func<LimitV1, bool>>
            {
                item => {
                    if (id != null && item.Id != id)
                        return false;
                    if (userId != null && item.UserId != userId)
                        return false;
                    if (ids != null && Array.IndexOf(ids, item.Id) < 0)
                        return false;
                    if (userIds != null && Array.IndexOf(userIds, item.UserId) < 0)
                        return false;
                    return true;
                }
            };
        }

        public Task<DataPage<LimitV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return base.GetPageByFilterAsync(correlationId, this.ComposeFilter(filter), paging);
        }
    }
}
