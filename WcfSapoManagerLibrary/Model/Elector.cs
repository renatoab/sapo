using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WcfSapoManagerLibrary
{
    [DataContract]
    public class Elector
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string UserName { get; set; }

        public Elector(string userName)
        {
            Id = Guid.NewGuid();
            UserName = userName;
        }
        
        public override bool Equals(Object obj)
        {
            Elector ep = null;

            try
            {
                ep = (Elector)obj;
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
