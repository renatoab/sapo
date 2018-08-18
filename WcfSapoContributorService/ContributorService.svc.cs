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

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        IncludeExceptionDetailInFaults = true)]
    public class ContributorService : IContributorService
    {
        private static Queue<Politician> politiciansQueue = new Queue<Politician>();
        private static Queue<Proposal> proposalsQueue = new Queue<Proposal>();
        private static Queue<PoliticianProposal> politicianProposalsQueue = new Queue<PoliticianProposal>();

        public bool AddPolitician(string name, string party)
        {
            Politician politician;

            using (var cli = new PoliticianServiceClient())
            {
                cli.Open();
                politician = cli.GeneratePolitician(name, party);
                cli.Close();
            }
                        
            if (politician != null)
            {
                politiciansQueue.Enqueue(politician);
                return true;
            }                

            return false;
        }
        
        public bool AddPoliticianVote(Guid idPolitician, Guid idProposal, bool? vote)
        {
            if (vote == null)
                return false;
            
            PoliticianProposal politicianProposal;

            using (var cli = new PoliticianProposalServiceClient())
            {
                cli.Open();
                bool auxVote = vote ?? false;
                politicianProposal = cli.GeneratePoliticianProposal(idPolitician, idProposal, auxVote);
                cli.Close();
            }

            if (politicianProposal != null)
            {
                politicianProposalsQueue.Enqueue(politicianProposal);
                return true;
            }

            return false;
        }

        public bool AddPoliticianVoteByName(string politicianName, string proposalTitle, bool vote)
        {
            Guid idPolitician = politiciansQueue.Where(x => x.Name == politicianName).FirstOrDefault().Id;

            //if (idPolitician = Guid(0))
            //    throw new Exception();

            Guid idProposal = proposalsQueue.Where(x => x.Title == proposalTitle).FirstOrDefault().Id;

            AddPoliticianVote(idPolitician, idProposal, vote);

            return true;
        }


        public bool AddProposal(string title, string text)
        {
            //throw new NotImplementedException();
            Proposal proposal;

            using (var cli = new ProposalServiceClient())
            {
                cli.Open();
                proposal = cli.GenerateProposal(title, text);
                cli.Close();
            }

            if (proposal != null)
            {
                proposalsQueue.Enqueue(proposal);
                return true;
            }

            return false;
        }

        public Queue<Politician> ListPoliticians()
        {
            return politiciansQueue;
        }

        public Queue<Proposal> ListProposals()
        {
            return proposalsQueue;
            //throw new NotImplementedException();
        }

        public bool VerifyAndPersist(string userName, string password)
        {
            using (var cli = new PersistServiceClient())
            {
                cli.Open();
                bool saveOk;
                try
                {
                    saveOk = cli.SaveProposals(proposalsQueue, userName, password);
                    saveOk = cli.SavePoliticians(politiciansQueue, userName, password);
                    saveOk = cli.SavePoliticianProposals(politicianProposalsQueue, userName, password);
                }
                catch(FaultException<AutenticationException> ex)
                {
                    cli.Close();
                    return false;
                }

                cli.Close();
                return true;
            }
        }
    }
}
