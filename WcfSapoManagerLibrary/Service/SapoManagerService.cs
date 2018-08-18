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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple,
    IncludeExceptionDetailInFaults = true)]
    public class SapoManagerService : IPoliticianService, IProposalService, IPoliticianProposalService, IElectorService, IElectorProposalService,
                                      IPersistService, IUserService
    {
        private static ConcurrentDictionary<string, Politician> politiciansDictionary = new ConcurrentDictionary<string, Politician>();
        private static ConcurrentDictionary<string, Proposal> proposalsDictionary = new ConcurrentDictionary<string, Proposal>();
        private static ConcurrentDictionary<string, Elector> electorsDictionary = new ConcurrentDictionary<string, Elector>();
        private static ConcurrentDictionary<PoliticianProposal, bool> politicianProposalsDictionary = new ConcurrentDictionary<PoliticianProposal, bool>();
        //private static ConcurrentDictionary<Tuple<Guid, Guid>, PoliticianProposal> politicianProposalsDictionary = new ConcurrentDictionary<Tuple<Guid, Guid>, PoliticianProposal>();
        private static ConcurrentDictionary<ElectorProposal, bool> electorProposalsDictionary = new ConcurrentDictionary<ElectorProposal, bool>();
        //private static ConcurrentDictionary<Tuple<Guid, Guid>, ElectorProposal> electorProposalsDictionary = new ConcurrentDictionary<Tuple<Guid, Guid>, ElectorProposal>();
        private static ConcurrentDictionary<string, Contributor> contributorDictionary = new ConcurrentDictionary<string, Contributor>();

        private static Dictionary<string, User> UsersDictionary  = new Dictionary<string, User>();

        public Politician GeneratePolitician(string name, string party)
        {
            if (politiciansDictionary != null)
                if (politiciansDictionary.Count > 0)
                    if (politiciansDictionary[name] != null)
                        return politiciansDictionary[name];
            return new Politician(name, party);
        }

        public Proposal GenerateProposal(string title, string text)
        {
            return new Proposal(title, text);
        }

        public PoliticianProposal GeneratePoliticianProposal(Guid idPolitician, Guid idProposal, bool vote)
        {
            return new PoliticianProposal(idPolitician, idProposal, vote);
        }

        public Elector GenerateElector(string userName)
        {
            return new Elector(userName);
        }

        public ElectorProposal GenerateElectorProposal(Guid idElector, Guid idProposal, bool vote)
        {
            return new ElectorProposal(idElector, idProposal, vote);
        }

        //public bool SaveEntity(ConcurrentQueue<TEntity> politiciansQueue) 
        //{

        //}

        // A ser realizado pelo colaborador, requer autenticacao
        public bool SavePoliticians(Queue<Politician> politiciansQueue, string userName, string password)
        {
            if (!AutenticateContributor(userName, password))
                throw new FaultException<AutenticationException>(new AutenticationException("Login invalido"));
            foreach (Politician politician in politiciansQueue)
            {
                politiciansDictionary.TryAdd(politician.Name, politician);
            }
            return true;
        }

        // A ser realizado pelo colaborador, requer autenticacao
        public bool SaveProposals(Queue<Proposal> proposalsQueue, string userName, string password)
        {
            if (!AutenticateContributor(userName, password))
                throw new FaultException<AutenticationException>(new AutenticationException("Login invalido"));
            foreach (Proposal proposal in proposalsQueue)
            {
                proposalsDictionary.TryAdd(proposal.Title, proposal);
            }
            return true;
        }

        // A ser realizado pelo eleitor, NÂO requer autenticacao
        public bool SaveElectors(Queue<Elector> electorsQueue)
        {
            foreach (Elector elector in electorsQueue)
            {
                electorsDictionary.TryAdd(elector.UserName, elector);
            }
            return true;
        }

        // A ser realizado pelo eleitor, NÂO requer autenticacao
        public bool SaveElectorProposals(Queue<ElectorProposal> electorProposalsQueue)
        {
            foreach (ElectorProposal electorProposal in electorProposalsQueue)
            {
                //Tuple<Guid, Guid> myTuple = Tuple.Create<Guid, Guid>(electorProposal.IdElector, electorProposal.IdProposal);
                //electorProposalsDictionary.TryAdd(myTuple, electorProposal);
                electorProposalsDictionary.TryAdd(electorProposal, true);
            }
            RefreshRanking();
            return true;
        }

        //public bool SaveEntity<T>(IList<T> entityList)
        //{
        //    foreach (T entity in entityList)
        //    {
        //        this.entityList.
        //        Dic.tryAdd(elector.UserName, elector);                
        //    }
        //    return true;
        //}

        // A ser realizado pelo colaborador, requer autenticacao
        public bool SavePoliticianProposals(Queue<PoliticianProposal> politicianProposalsQueue, string userName, string password)
        {
            if (!AutenticateContributor(userName, password))
                throw new FaultException<AutenticationException>(new AutenticationException("Login invalido"));
            foreach (PoliticianProposal politicianProposal in politicianProposalsQueue)
            {
                //Tuple<Guid, Guid> myTuple = Tuple.Create<Guid, Guid>(politicianProposal.IdPolitician, politicianProposal.IdProposal);
                //politicianProposalsDictionary.TryAdd(myTuple, politicianProposal);
                politicianProposalsDictionary.TryAdd(politicianProposal, true);
            }
            RefreshRanking();
            return true;
        }

        private void RefreshRanking()
        {
            bool politicianVotes = false;
            bool electorVotes = false;
            if (politicianProposalsDictionary != null)
                if (politicianProposalsDictionary.Count > 0)
                    politicianVotes = true;
            if (electorProposalsDictionary != null)
                if (electorProposalsDictionary.Count > 0)
                    electorVotes = true;


            if (politicianVotes && electorVotes)
            {
                int countFavorable = 0;
                int countNotFavorable = 0;
                foreach (var varPolitician in politiciansDictionary)
                {
                    var favorable =  from p in politicianProposalsDictionary.Keys
                                     join e in electorProposalsDictionary.Keys on p.IdProposal equals e.IdProposal
                                     where p.Vote == e.Vote && varPolitician.Value.Id == p.IdPolitician
                                     select p.Vote;
                    countFavorable = countFavorable + favorable.Count();
                    var notFavorable = from p in politicianProposalsDictionary.Keys
                                    join e in electorProposalsDictionary.Keys on p.IdProposal equals e.IdProposal
                                    where p.Vote != e.Vote && varPolitician.Value.Id == p.IdPolitician
                                    select p.Vote;
                    countNotFavorable = countNotFavorable + notFavorable.Count();

                    varPolitician.Value.Score = 100 * countFavorable / (countFavorable + countNotFavorable);

                    //politicianProposalsDictionary.Values.SequenceEqual(electorProposalsDictionary);

                }
            }
        }

        public IList<Proposal> LoadProposals()
        {
            if (proposalsDictionary != null)
                if (proposalsDictionary.Count > 0)
                    return proposalsDictionary.Select(x => x.Value).ToList();
            return null;
        }

        public IList<Politician> LoadPoliticians()
        {
            if (politiciansDictionary != null)
                if (politiciansDictionary.Count > 0)
                    return politiciansDictionary.Select(x => x.Value).ToList();
            return null;
        }

        public bool AutenticateContributor(string userName, string password)
        {
            // encontra o usuario na lista de usuarios
            User user = UsersDictionary[userName];
            if (user == null)
                return false;
            return User.AutenticateContributor(user, password);
        }
        
        public void AddUser(string userName, string password, bool isContributor, bool isAdmin)
        {
            User user = new User(userName, password, isContributor, isAdmin);
            UsersDictionary.Add(userName, user);
        }
        
        public int GetElectorsNumber()
        {
            return electorsDictionary.Count;
        }
    }
}