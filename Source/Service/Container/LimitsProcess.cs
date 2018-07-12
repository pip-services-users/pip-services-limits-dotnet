using System;
using PipServicesLimitsDotnet.Build;
using PipServices.Container;
using PipServices.Net.Build;
using PipServices.Oss.Build;

namespace PipServicesLimitsDotnet.Container
{
    public class LimitsProcess : ProcessContainer
    {
        public LimitsProcess() : base ("beacons", "Limits Microservice")
        {
            this._factories.Add(new LimitsServiceFactory());
            this._factories.Add(new DefaultNetFactory());
            this._factories.Add(new DefaultOssFactory());
        }
    }
}