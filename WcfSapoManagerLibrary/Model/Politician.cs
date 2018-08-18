using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WcfSapoManagerLibrary
{
    [DataContract]
    public class Politician
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Party { get; set; }
        [DataMember]
        public int Score { get; set; }

        public Politician(string name, string party)
        {
            Id = Guid.NewGuid();
            Name = name;
            Party = party;
            Score = 0;
        }

        public override bool Equals(Object obj)
        {
            Politician ep = null;

            try
            {
                ep = (Politician)obj;
            }
            catch
            {
                return base.Equals(obj);
            }

            if (this.Id == ep.Id || this.Name == ep.Name)
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
