using System.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfSapoManagerLibrary
{
    [ServiceContract]
    interface IUserService
    {
        [OperationContract]
        bool AutenticateContributor(string userName, string password);
        [OperationContract]
        void AddUser(string userName, string password, bool isContributor, bool isAdmin);
    }
}
