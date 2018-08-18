using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace WcfSapoManagerLibrary
{
    [DataContract]
    public class PoliticianProposal : IComparable
    {
        [DataMember]
        public Guid IdPolitician { get; set; }
        [DataMember]
        public Guid IdProposal { get; set; }
        [DataMember]
        public bool Vote { get; set; }

        public PoliticianProposal(Guid idPolitician, Guid idProposal, bool vote)
        {
            IdPolitician = idPolitician;
            IdProposal = idProposal;
            Vote = vote;
        }

        public bool CompareVote(PoliticianProposal p)
        {
            if (p.Vote == this.Vote)
                return true;
            return false;
        }

        public bool CompareVote(ElectorProposal e)
        {
            if (e.Vote == this.Vote)
                return true;
            return false;
        }

        public override bool Equals(Object obj)
        {
            PoliticianProposal ep = null;

            try
            {
                ep = (PoliticianProposal)obj;
            }
            catch
            {
                return base.Equals(obj);
            }

            if (this.IdPolitician == ep.IdPolitician
                && this.IdProposal == ep.IdProposal)
                return true;

            return false;
        }

        public int CompareTo(object obj)
        {
            if (this.Equals(obj))
                return 0;
            return -1;
        }
    }
}