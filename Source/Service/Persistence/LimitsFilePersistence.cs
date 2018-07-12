using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using PipServices.Commons.Config;
using PipServices.Data.Memory;
using PipServices.Data.File;

using PipServicesLimitsDotnet.Data.Version1;

namespace PipServicesLimitsDotnet.Persistence
{
    
    public class LimitsFilePersistence : LimitsMemoryPersistence
    {
        protected JsonFilePersister<LimitV1> _persistor;

        public LimitsFilePersistence() : base()
        {
            _persistor = new JsonFilePersister<LimitV1>();
            this._loader = this._persistor;
            this._saver = this._persistor;
        }

        override public void Configure(ConfigParams config)
        {
            base.Configure(config);
            this._persistor.Configure(config);
        }
       
    }
}
