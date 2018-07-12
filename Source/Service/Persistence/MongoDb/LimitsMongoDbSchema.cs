using MongoDB.Bson.Serialization.Attributes;

using PipServices.Commons.Data;

namespace PipServicesLimitsDotnet.Persistence.MongoDb
{
    [BsonIgnoreExtraElements]
    public class LimitsMongoDbSchema : IStringIdentifiable
    {

        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("user_id")]
        public string UserId { get; set; }

        [BsonElement("amount_used")]
        public long AmountUsed { get; set; }

        [BsonElement("limit")]
        public long Limit { get; set; }
    }


}