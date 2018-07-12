using PipServices.Commons.Refer;
using PipServices.Net.Rest;

namespace PipServicesLimitsDotnet.Services.Version1
{
    public class LimitsHttpServiceV1 : CommandableHttpService
    {
        public LimitsHttpServiceV1() : base("v1/limits")
        {
            _dependencyResolver.Put("controller", new Descriptor("pip-services-limits-dotnet", "controller", "*", "*", "1.0"));
        }
    }
}