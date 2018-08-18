using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleContributor.SapoContributorService;
using System.Collections.Concurrent;

namespace ConsoleContributor
{
    class ProgramContributor
    {
        static void Main(string[] args)
        {
            string userName = "user1";
            string passWord = "1234";


            List<Politician> politicians;
            List<Proposal> proposals;

            provideProposals(); // cadastra as propostas

            using (var cli = new ContributorServiceClient())
            {
                cli.Open();
                politicians = cli.ListPoliticians().ToList();
                proposals = cli.ListProposals().ToList();
                cli.Close();
            }

            providePoliticianData();

            using (var cli = new ContributorServiceClient())
            {
                cli.Open();
                politicians = cli.ListPoliticians().ToList();
                //proposals = cli.ListProposals().ToList();
                cli.Close();
            }

            Console.WriteLine("Persistir as informacoes ? (S)im / (N)ao");
            string voto = Console.ReadKey().Key.ToString().ToUpper();
            if (voto == "S")
            {
                using (var cli = new ContributorServiceClient())
                {
                    cli.Open();
                    bool saveOk = false;
                    int itry = 0;
                    while (!saveOk && itry < 10)
                    {
                        saveOk = cli.VerifyAndPersist(userName, passWord);
                        System.Threading.Thread.Sleep(2500);
                        if (itry >= 1) Console.WriteLine($"Falha na autenticacao. Aguardando... Tentativa {itry} de 10");
                        itry++;
                    }
                    
                    Console.WriteLine("Dados foram persistidos");
                    //proposals = cli.ListProposals().ToList();
                    cli.Close();
                }
            }
            else
            {
                //
            }
            
            Console.WriteLine("Finalizada inclusao de informacoes");
            Console.ReadKey();
        }

        static void provideProposals()
        {
            using (var cli = new ContributorServiceClient())
            {
                cli.Open();

                Console.WriteLine("\nCadastrando propostas que foram votadas no congresso:\n");

                string title1 = "Reforma trabalhista"; string texto1 = "Detalhes da Reforma trabalhista";
                bool ok = cli.AddProposal(title1, texto1);
                if (ok) Console.WriteLine($"Proposta cadastrada : {title1}");

                string title2 = "Impedir Denuncia Temer 1"; string texto2 = "Detalhes da Denuncia 1";
                ok = cli.AddProposal(title2, texto2);
                if (ok) Console.WriteLine($"Proposta cadastrada : {title2}");

                string title3 = "Impedir Denuncia Temer 2"; string texto3 = "Detalhes da Denuncia 2";
                ok = cli.AddProposal(title3, texto3);
                if (ok) Console.WriteLine($"Proposta cadastrada : {title3}");

                cli.Close();
            }                           

        }

        static void providePoliticianData()
        {
            string name;
            string party;
            bool voto1;
            bool voto2;
            bool voto3;

            using (var cli = new ContributorServiceClient())
            {
                cli.Open();

                Console.WriteLine("\nCadastrando políticos:\n");

                name = "Tiririca"; party = "PR"; voto1 = false; voto2 = false; voto3 = false;
                bool ok = cli.AddPolitician(name, party);
                int icount = 0;
                if (cli.AddPoliticianVoteByName(name, "Reforma trabalhista", voto1)) icount++;
                if (cli.AddPoliticianVoteByName(name, "Impedir Denuncia Temer 1", voto2)) icount++;
                if (cli.AddPoliticianVoteByName(name, "Impedir Denuncia Temer 2", voto3)) icount++;
                if (ok) Console.WriteLine($"Cadastrado politico {name}, votos cadastrados: {icount}");

                name = "Romario"; party = "PODE"; voto1 = false;
                ok = cli.AddPolitician(name, party);
                icount = 0;
                if (cli.AddPoliticianVoteByName(name, "Reforma trabalhista", voto1)) icount++;
                if (ok) Console.WriteLine($"Cadastrado politico {name}, votos cadastrados: {icount}");

                name = "Sergio Reis"; party = "PRB"; voto2 = false; voto3 = false;
                ok = cli.AddPolitician(name, party);
                icount = 0;
                if (cli.AddPoliticianVoteByName(name, "Impedir Denuncia Temer 1", voto2)) icount++;
                if (cli.AddPoliticianVoteByName(name, "Impedir Denuncia Temer 2", voto3)) icount++;
                if (ok) Console.WriteLine($"Cadastrado politico {name}, votos cadastrados: {icount}");

                name = "Bolsonaro"; party = "PSC"; voto1 = true; voto2 = false; voto3 = false;
                ok = cli.AddPolitician(name, party);
                icount = 0;
                if (cli.AddPoliticianVoteByName(name, "Reforma trabalhista", voto1)) icount++;
                if (cli.AddPoliticianVoteByName(name, "Impedir Denuncia Temer 1", voto2)) icount++;
                if (cli.AddPoliticianVoteByName(name, "Impedir Denuncia Temer 2", voto3)) icount++;
                if (ok) Console.WriteLine($"Cadastrado politico {name}, votos cadastrados: {icount}");

                cli.Close();
            }


        }

    }
}
