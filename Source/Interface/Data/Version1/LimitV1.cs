using System.Runtime.Serialization;
using PipServices.Commons.Data;

namespace PipServicesLimitsDotnet.Data.Version1 
{
    [DataContract]
    public class LimitV1 : IStringIdentifiable
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "amount_used")]
        public long AmountUsed { get; set; }

        [DataMember(Name = "limit")]
        public long Limit { get; set; }
    }
}