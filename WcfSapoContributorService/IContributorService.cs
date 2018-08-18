using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfSapoContributorService.SapoManagerService;

namespace WcfSapoContributorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IContributorService
    {
        [OperationContract]
        bool AddPolitician(string name, string party);

        [OperationContract]
        bool AddProposal(string title, string text);

        [OperationContract]
        bool AddPoliticianVote(Guid idPolitician, Guid idProposal, bool? vote);

        [OperationContract]
        bool AddPoliticianVoteByName(string politicianName, string proposalName, bool vote);

        [OperationContract]
        bool VerifyAndPersist(string userName, string password);

        [OperationContract]
        Queue<Politician> ListPoliticians();

        [OperationContract]
        Queue<Proposal> ListProposals();

        // TODO: Add your service operations here
    }

}
