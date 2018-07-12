using PipServices.Commons.Refer;
using PipServices.Net.Rest;

namespace IqsServicesBeaconsCore.Services.Version1
{
    public class BeaconsHttpServiceV1 : CommandableHttpService
    {
        public BeaconsHttpServiceV1() : base("v1/beacons")
        {
            _dependencyResolver.Put("controller", new Descriptor("iqs-services-beacons", "controller", "*", "*", "1.0"));
        }
    }
}

/*
import { CommandableHttpService } from 'pip-services-net-node';
import { Descriptor } from 'pip-services-commons-node';

export class BeaconsHttpServiceV1 extends CommandableHttpService {
    public constructor () {
        super('v1/beacons');
        this._dependencyResolver.put('controller', new Descriptor('iqs-services-beacons', 'controller', '*', '*', '1.0'));
    }
}
*/
