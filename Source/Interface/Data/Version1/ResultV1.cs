using System.Runtime.Serialization;
using PipServices.Commons.Data;

namespace PipServicesLimitsDotnet.Data.Version1 
{
    [DataContract]
    public class ResultV1
    {
        [DataMember(Name = "boolResult")]
        public bool boolResult { get; set; }

        [DataMember(Name = "longResult")]
        public long longResult { get; set; }
    }
}