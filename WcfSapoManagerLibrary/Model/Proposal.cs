using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfSapoManagerLibrary
{
    [DataContract]
    public class Proposal
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Text { get; set; }

        public Proposal(string title, string text)
        {
            Id = Guid.NewGuid();
            Title = title;
            Text = text;
        }

        public override bool Equals(Object obj)
        {
            Proposal ep = null;

            try
            {
                ep = (Proposal)obj;
            }
            catch
            {
                return base.Equals(obj);
            }

            if (this.Id == ep.Id)
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
