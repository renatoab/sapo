using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfSapoManagerLibrary.Exception;

namespace WcfSapoManagerLibrary
{
    [ServiceContract]
    interface IPersistService
    {
        [OperationContract]
        [FaultContract(typeof(AutenticationException))]
        bool SavePoliticians(Queue<Politician> politiciansQueue, string userName, string password);
        [OperationContract]
        [FaultContract(typeof(AutenticationException))]
        bool SaveProposals(Queue<Proposal> proposalsQueue, string userName, string password);
        [OperationContract]
        bool SaveElectors(Queue<Elector> electorsQueue);
        [OperationContract]
        bool SaveElectorProposals(Queue<ElectorProposal> electorProposalsQueue);
        [OperationContract]
        [FaultContract(typeof(AutenticationException))]
        bool SavePoliticianProposals(Queue<PoliticianProposal> politicianProposalsQueue, string userName, string password);

        [OperationContract]
        IList<Proposal> LoadProposals();
        [OperationContract]
        IList<Politician> LoadPoliticians();
    }
}
