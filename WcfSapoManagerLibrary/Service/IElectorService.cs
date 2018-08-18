using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace WcfSapoManagerLibrary
{
    [ServiceContract]
    interface IElectorService
    {
        [OperationContract]
        Elector GenerateElector(string userName);

        [OperationContract]
        int GetElectorsNumber();
    }
}
