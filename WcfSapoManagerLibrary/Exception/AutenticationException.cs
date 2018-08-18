using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfSapoManagerLibrary.Exception
{
    [DataContract]
    public class AutenticationException
    {
        [DataMember]
        public string Message { get; set; }
        public AutenticationException(string message)
        {
            Message = message;
        }
    }
}
