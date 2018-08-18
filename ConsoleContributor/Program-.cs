using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleContributor.SapoContributorService;
using System.Collections.Concurrent;

namespace ConsoleContributor
{
    class Program
    {
        static void Main(string[] args)
        {

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
            
            using (var cli = new ContributorServiceClient())
            {
                cli.Open();


                politicians = cli.ListPoliticians().ToList();



                //proposals = cli.ListProposals().ToList();
                cli.Close();
            }


            

        }

        static void provideProposals()
        {
            using (var cli = new ContributorServiceClient())
            {
                cli.Open();

                string title1 = "Reforma trabalhista"; string texto1 = "Detalhes da Reforma trabalhista";
                cli.AddProposal(title1, texto1);

                string title2 = "Impedir Denuncia Temer 1"; string texto2 = "Detalhes da Denuncia 1";
                cli.AddProposal(title2, texto2);

                string title3 = "Impedir Denuncia Temer 2"; string texto3 = "Detalhes da Denuncia 2";
                cli.AddProposal(title3, texto3);

                cli.Close();
            }                           

        }

        static void providePoliticianData1()
        {
            string name;
            string party;
            bool voto1;
            bool voto2;
            bool voto3;

            using (var cli = new ContributorServiceClient())
            {
                cli.Open();

                name = "Tiririca"; party = "PR"; voto1 = false; voto2 = false; voto3 = false;
                cli.AddPoliticianVote(name, "Reforma trabalhista", voto1);
                cli.AddPoliticianVote(name, "Impedir Denuncia Temer 1", voto2);
                cli.AddPoliticianVote(name, "Impedir Denuncia Temer 2", voto3);

                name = "Romario"; party = "PODE"; voto1 = false;
                cli.AddPoliticianVote(name, "Reforma trabalhista", voto1);

                name = "Sergio Reis"; party = "PRB"; voto2 = false; voto3 = false;
                cli.AddPoliticianVote(name, "Impedir Denuncia Temer 1", voto2);
                cli.AddPoliticianVote(name, "Impedir Denuncia Temer 2", voto3);

                cli.Close();
            }


        }

    }
}
