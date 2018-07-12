using PipServicesLimitsDotnet.Data.Version1;
using PipServicesLimitsDotnet.Persistence.MongoDb;

using MongoDB.Driver;

using PipServices.Commons.Data;
using PipServices.Commons.Data.Mapper;
using PipServices.Oss.MongoDb;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PipServicesLimitsDotnet.Persistence
{
    public class LimitsMongoDbPersistence : IdentifiableMongoDbPersistence<LimitsMongoDbSchema, string>, ILimitsPersistence
    {
        public LimitsMongoDbPersistence() : base("limits")
        {
            this._maxPageSize = 1000;
        }


        private FilterDefinition<LimitsMongoDbSchema> ComposeFilter(FilterParams filterParams)
        {
            filterParams = filterParams ?? new FilterParams();

            var builder = Builders<LimitsMongoDbSchema>.Filter;
            var filter = builder.Empty;

            foreach (var filterKey in filterParams.Keys)
            {
                if (filterKey.Equals("id"))
                {
                    filter &= builder.In(s => s.Id, ToStringArray(filterParams.GetAsNullableString("id")));
                    continue;
                }

                if (filterKey.Equals("ids"))
                {
                    filter &= builder.In(s => s.Id, ToStringArray(filterParams.GetAsNullableString("ids")));
                    continue;
                }

                if (filterKey.Equals("user_id"))
                {
                    filter &= builder.In(s => s.UserId, ToStringArray(filterParams.GetAsNullableString("user_id")));
                    continue;
                }

                if (filterKey.Equals("user_ids"))
                {
                    filter &= builder.In(s => s.UserId, ToStringArray(filterParams.GetAsNullableString("user_ids")));
                    continue;
                }

                filter &= builder.Eq(filterKey, filterParams[filterKey]);
            }

            return filter;
        }

        public async Task<DataPage<LimitV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            var result = await base.GetPageByFilterAsync(correlationId, this.ComposeFilter(filter), paging);
            var data = result.Data.ConvertAll<LimitV1>(x => ToPublic(x));
            return new DataPage<LimitV1>()
            {
                Data = data,
                Total = data.Count
            };
        }

        public async Task<LimitV1> GetOneByUserIdAsync(string correlationId, string userId)
        {
            var filter = new FilterParams();
            var paging = new PagingParams();
            filter.Add("user_id", userId);
            var result = await GetPageByFilterAsync(correlationId, ComposeFilter(filter), paging);
            var data = result.Data.ConvertAll<LimitV1>(x => ToPublic(x));
            return data[0];
        }



        /*--------------------Interface's methods--------------------*/
        public async Task<LimitV1> CreateAsync(string correlationId, LimitV1 limit)
        {
            var result = await base.CreateAsync(correlationId, FromPublic(limit));

            return ToPublic(result);
        }

        public async Task<LimitV1> GetOneByIdAsync(string correlationId, string id)
        {
            var result = await base.GetOneByIdAsync(correlationId, id);

            return ToPublic(result);
        }

        public async Task<LimitV1> UpdateAsync(string correlationId, LimitV1 limit)
        {
            var result = await base.UpdateAsync(correlationId, FromPublic(limit));

            return ToPublic(result);
        }

        public async Task<LimitV1> DeleteByIdAsync(string correlationId, string id)
        {
            var result = await base.DeleteByIdAsync(correlationId, id);

            return ToPublic(result);
        }



        /*--------------------Helper methods--------------------*/
        private static LimitV1 ToPublic(LimitsMongoDbSchema value)
        {
            if (value == null)
            {
                return null;
            }

            return ObjectMapper.MapTo<LimitV1>(value);
        }

        private static LimitsMongoDbSchema FromPublic(LimitV1 value)
        {
            return ObjectMapper.MapTo<LimitsMongoDbSchema>(value);
        }

        private static string[] ToStringArray(string value)
        {
            if (value == null)
            {
                return null;
            }

            string[] items = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return items.Length > 0 ? items : null;
        }


    }
}
