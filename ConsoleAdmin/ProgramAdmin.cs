using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAdmin.SapoManagerService;

namespace ConsoleAdmin
{
    class ProgramAdmin
    {
        static void Main(string[] args)
        {

            using (var cli = new UserServiceClient())
            {
                cli.AddUser("user1","1234", true, false);
                cli.AddUser("user2", "1234", true, false);
            }
        }
    }
}
