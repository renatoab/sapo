using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfSapoManagerLibrary
{
    [DataContract]
    public class ElectorProposal : IComparable
    {
        [DataMember]
        public Guid IdElector { get; set; }
        [DataMember]
        public Guid IdProposal { get; set; }
        [DataMember]
        public bool Vote { get; set; }

        public ElectorProposal(Guid idElector, Guid idProposal, bool vote)
        {
            IdElector = idElector;
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
            ElectorProposal ep = null;

            try
            {
                ep = (ElectorProposal)obj;
            }
            catch
            {
                return base.Equals(obj);
            }

            if (this.IdElector == ep.IdElector
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
