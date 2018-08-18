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
    interface IPoliticianProposalService
    {
        [OperationContract]
        PoliticianProposal GeneratePoliticianProposal(Guid idPolitician, Guid idProposal, bool vote);
    }
}
