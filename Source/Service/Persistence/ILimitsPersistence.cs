using System.Threading.Tasks;
using PipServices.Commons.Data;
using PipServicesLimitsDotnet.Data.Version1;

namespace PipServicesLimitsDotnet.Persistence
{
    public interface ILimitsPersistence
    {
        Task<DataPage<LimitV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging);
        Task<LimitV1> GetOneByIdAsync(string correlationId, string id);
        Task<LimitV1> GetOneByUserIdAsync(string correlationId, string userId);
        Task<LimitV1> CreateAsync(string correlationId, LimitV1 limit);
        Task<LimitV1> UpdateAsync(string correlationId, LimitV1 limit);
        Task<LimitV1> DeleteByIdAsync(string correlationId, string userId);
    }
}

