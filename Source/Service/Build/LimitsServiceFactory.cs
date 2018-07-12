using System;
using PipServicesLimitsDotnet.Logic;
using PipServicesLimitsDotnet.Persistence;
using PipServicesLimitsDotnet.Services.Version1;
using PipServices.Commons;
using PipServices.Commons.Build;
using PipServices.Commons.Refer;

namespace PipServicesLimitsDotnet.Build
{
    public class LimitsServiceFactory : Factory
    {
        public static Descriptor MemoryPersistenceDescriptor = new Descriptor("pip-services-limits-dotnet", "persistence", "memory", "*", "1.0");
        public static Descriptor FilePersistenceDescriptor = new Descriptor("pip-services-limits-dotnet", "persistence", "file", "*", "1.0");
        public static Descriptor MongoDbPersistenceDescriptor = new Descriptor("pip-services-limits-dotnet", "persistence", "mongodb", "*", "1.0");
        public static Descriptor ControllerDescriptor = new Descriptor("pip-services-limits-dotnet", "controller", "default", "*", "1.0");
        public static Descriptor HttpServiceV1Descriptor = new Descriptor("pip-services-limits-dotnet", "service", "http", "*", "1.0");

        public LimitsServiceFactory() : base()
        {
            this.RegisterAsType(LimitsServiceFactory.MemoryPersistenceDescriptor, typeof (LimitsMemoryPersistence));
            this.RegisterAsType(LimitsServiceFactory.FilePersistenceDescriptor, typeof(LimitsFilePersistence));
            this.RegisterAsType(LimitsServiceFactory.MongoDbPersistenceDescriptor, typeof(LimitsMongoDbPersistence));
            this.RegisterAsType(LimitsServiceFactory.ControllerDescriptor, typeof(LimitsController));
            this.RegisterAsType(LimitsServiceFactory.HttpServiceV1Descriptor, typeof(LimitsHttpServiceV1));
        }
    }
}