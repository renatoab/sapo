using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleElector.SapoElectorService;
using System.Threading;

namespace ConsoleElector
{
    class ProgramElector
    {
        static void Main(string[] args)
        {


            List<Proposal> listProposals = null;

            using (var cli = new SapoElectorServiceClient())
            {
                cli.Open();
                int itry = 0;
                while ((listProposals == null || listProposals.Count == 0) && itry < 20)
                {
                    listProposals = cli.GetAllProposals();
                    System.Threading.Thread.Sleep(2500);
                    if (itry >= 1) Console.WriteLine($"Nenhuma proposta a ser votada. Aguardando... Tentativa {itry} de 20");
                    itry++;
                }
                if (listProposals == null)
                {
                    Console.WriteLine("Limite de tentativas esgotado");
                }
                else
                {
                    Console.WriteLine("Ok. Propostas foram carregadas no sistema.");
                }
                //proposals = cli.ListProposals().ToList();

                // Trecho comentado: insere eleitores e seus votos manualmente

                //int nelec;
                //Console.WriteLine("numero de eleitores a criar: ");
                //nelec = Convert.ToInt16(Console.ReadLine());

                //string[] nomeEleitor = new string[nelec];
                //for (int i = 0; i < nelec; i++)
                //{
                //    nomeEleitor[i] = $"\neleitor {Convert.ToString(i + 1)}";
                //    cli.AddElector(nomeEleitor[i]);

                //    Console.WriteLine($"\n{nomeEleitor[i]}  votando : \n");
                //    foreach (Proposal proposal in listProposals)
                //    {
                //        Console.WriteLine($"\nProposta: {proposal.Title}");
                //        Console.WriteLine("Votar: (S)im / (N)ao / (ESPACO) saltar");
                //        string voto = Console.ReadKey().Key.ToString().ToUpper();
                //        if (voto == "S")
                //        {
                //            cli.AddElectorVoteByName(nomeEleitor[i],proposal.Title, true);
                //        }
                //        else if (voto == "N")
                //        {
                //            cli.AddElectorVoteByName(nomeEleitor[i], proposal.Title, false);
                //        }
                //    }                  

                //}
                //cli.PersistData();

                //RobotElector("eleitor", 50, listProposals);

                // Threads para cadastrar eleitores e votos dos eleitores
                // para simplificar, todos estao programados para votar NÃO em todas as propostas
                Thread client1 = new Thread(() => RobotElector("eleitor paulista", 10, listProposals)); 
                client1.Start();
                Thread client2 = new Thread(() => RobotElector("eleitor mineiro", 3, listProposals));
                client2.Start();
                Thread client3 = new Thread(() => RobotElector("eleitor carioca", 3, listProposals));
                client3.Start();
                Thread client4 = new Thread(() => RobotElector("eleitor baiano", 3, listProposals));
                client4.Start();
                Thread client5 = new Thread(() => RobotElector("eleitor paranaense", 3, listProposals));
                client5.Start();

                client1.Join();
                client2.Join();
                client3.Join();
                client4.Join();
                client5.Join();

                /*
                while (client1.IsAlive || client2.IsAlive || client3.IsAlive || client4.IsAlive
                       || client5.IsAlive)
                {
                    //Do something
                }*/

                int nTotalElect = cli.GetElectorsNumber();

                Console.WriteLine($"\nTotal de eleitores no banco: {nTotalElect}");

                Console.WriteLine("\n\nClassificacao - concordância dos políticos com os eleitores:\n\n");
                foreach(Politician politician in cli.GetAllPoliticians())
                {
                    Console.WriteLine($"{politician.Name}      {politician.Score}%");
                }
                                
                cli.Close();
            }

            Console.WriteLine("\n\nFim do Programa Console Eleitor");
            Console.ReadLine();

        }

        private static void RobotElector(string prefixName, int nelec, List<Proposal> listProposals)
        {
            Console.WriteLine($"Cadastrando {nelec} eleitores {prefixName}");

            using (var cli = new SapoElectorServiceClient())
            {
                string[] nomeEleitor = new string[nelec];
                for (int i = 0; i < nelec; i++)
                {
                    nomeEleitor[i] = $"{prefixName} {Convert.ToString(i + 1)}";

                    Console.WriteLine($"Criando: {nomeEleitor[i]}");
                    cli.AddElector(nomeEleitor[i]);

                    Thread.Sleep(50);

                    Console.WriteLine($"{nomeEleitor[i]}");
                    foreach (Proposal proposal in listProposals)
                    {
                        cli.AddElectorVoteByName(nomeEleitor[i], proposal.Title, false);
                    }
                }
                Console.WriteLine($"Persistindo dados dos eleitores");
                bool okPersist = cli.PersistData();

                Console.WriteLine($"Fim do cadastro de eleitores {prefixName}");
                
            }
            
        }

    }
}