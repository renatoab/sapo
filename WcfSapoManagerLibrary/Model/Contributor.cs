using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfSapoManagerLibrary
{

    [DataContract]
    public class Contributor
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string UserName { get; set; }

        public Contributor(string userName)
        {
            Id = Guid.NewGuid();
            UserName = userName;
        }
        
        public override bool Equals(Object obj)
        {
            Contributor ep = null;

            try
            {
                ep = (Contributor)obj;
            }
            catch
            {
                return base.Equals(obj);
            }

            if (this.Id == ep.Id)
                return true;

            return false;
        }
    }

}
