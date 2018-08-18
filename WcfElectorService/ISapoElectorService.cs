using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfSapoElectorService.SapoManagerService;

namespace WcfSapoElectorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISapoElectorService
    {
        [OperationContract]
        bool AddElector(string name);

        [OperationContract]
        bool AddElectorVote(Guid idElector, Guid idProposal, bool vote);

        [OperationContract]
        bool AddElectorVoteByName(string electorName, string proposalName, bool vote);

        [OperationContract]
        IList<Proposal> GetAllProposals();

        [OperationContract]
        IList<Politician> GetAllPoliticians();

        [OperationContract]
        int GetElectorsNumber();

        [OperationContract]
        bool PersistData();
    }

}
