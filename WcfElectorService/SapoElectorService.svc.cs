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

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        IncludeExceptionDetailInFaults = true)]
    public class SapoElectorService : ISapoElectorService
    {
        private static Queue<Elector> electorsQueue = new Queue<Elector>();
        private static List<Proposal> proposalsList = new List<Proposal>();
        private static List<Politician> politiciansList = new List<Politician>();
        private static Queue<ElectorProposal> electorProposalsQueue = new Queue<ElectorProposal>();
        
        public bool AddElector(string name)
        {
            Elector elector;

            using (var cli = new ElectorServiceClient())
            {
                cli.Open();
                elector = cli.GenerateElector(name);
                cli.Close();
            }

            if (elector != null)
            {
                electorsQueue.Enqueue(elector);
                return true;
            }

            return false;
        }

        public bool AddElectorVote(Guid idElector, Guid idProposal, bool vote)
        {
            //if (vote == null)
            //    return false;

            ElectorProposal electorProposal;

            using (var cli = new ElectorProposalServiceClient())
            {
                cli.Open();
                bool auxVote = vote; // ?? false;
                electorProposal = cli.GenerateElectorProposal(idElector, idProposal, auxVote);
                cli.Close();
            }

            if (electorProposal != null)
            {
                electorProposalsQueue.Enqueue(electorProposal);
                return true;
            }

            return false;
        }

        public bool AddElectorVoteByName(string electorName, string proposalTitle, bool vote)
        {
            Guid idElector = electorsQueue.Where(x => x.UserName == electorName).FirstOrDefault().Id;

            //if (idPolitician = Guid(0))
            //    throw new Exception();

            Guid idProposal = proposalsList.Where(x => x.Title == proposalTitle).FirstOrDefault().Id;

            AddElectorVote(idElector, idProposal, vote);

            return true;
        }

        public bool GetProposalsFromManager()
        {
            using (var cli = new PersistServiceClient())
            {
                cli.Open();
                bool saveOk;
                proposalsList = cli.LoadProposals();
                
                cli.Close();
            }

            return true;
        }

        public bool GetPoliticiansFromManager()
        {
            using (var cli = new PersistServiceClient())
            {
                cli.Open();
                bool saveOk;
                politiciansList = cli.LoadPoliticians();

                cli.Close();
            }

            return true;
        }

        public Queue<Elector> ListElectors()
        {
            return electorsQueue;
        }

        public Queue<ElectorProposal> ListElectorProposals()
        {
            return electorProposalsQueue;
            //throw new NotImplementedException();
        }

        public IList<Proposal> GetAllProposals()
        {
            if (GetProposalsFromManager())
                return proposalsList;
            else
                return null;
        }

        public IList<Politician> GetAllPoliticians()
        {
            if (GetPoliticiansFromManager())
            {
                politiciansList = politiciansList.OrderByDescending(p => p.Score).ToList();
                return politiciansList;
            }                
            else
                return null;
        }

        public int GetElectorsNumber()
        {
            using (var cli = new ElectorServiceClient())
            {
                return cli.GetElectorsNumber();
            }
        }

        public bool PersistData()
        {
            using (var cli = new PersistServiceClient())
            {
                cli.Open();
                bool saveOk;
                saveOk = cli.SaveElectors(electorsQueue);
                saveOk = cli.SaveElectorProposals(electorProposalsQueue);

                cli.Close();
            }

            return true;
        }
    }
}
