using PipServices.Commons.Convert;
using PipServices.Commons.Validate;

namespace PipServicesLimitsDotnet.Data.Version1
{
    public class LimitV1Schema : ObjectSchema
    {
        public LimitV1Schema()
        {
            WithRequiredProperty("user_id", TypeCode.String);
            WithOptionalProperty("amount_used", TypeCode.Long);
            WithOptionalProperty("limit", TypeCode.Long);
        }
    }
}