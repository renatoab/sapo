


Sistema de Avaliação de Políticos (SAPO)
========================================

Avalia os políticos (deputados e senadores) quanto à concordancia de sua atuação com a preferencia dos eleitores 

Atores envolvidos:
Administrador: cadastra usuário e senha de Colaboradores do sistema
Colaborador: cadastra informações sobre políticos, propostas votadas no congresso nacional e como cada político votou
Eleitor: dá sua opinião sobre as propostas votadas no congresso

A partir das informações adquiridas, o sistema dá um percentual de concordância dos politicos com os eleitores 

----------------------------------------------------------------------

Rodando o programa

Ao debugar pelo Visual Studio, serão disparados os 3 executáveis.
- ConsoleAdmin vai abrir, cadastrar os dados necessários de login do Colaboradores e fechar
- ConsoleContributor vai fazer a função dos Colaboradores: adicionar as informações e solicitar que se clique "S" 
  para persistir as informações
- ConsoleElector vai abrir e fazer tentativas de verificar se há informacoes no banco (inseridas por ConsoleContributor).
  Quando houver informacoes de propostas, vai cadastrar o voto dos eleitores naquelas propostas
  Ao final da execução de ConsoleElector, aparecerá o ranking (calculado como o percentual de concordancia dos votos
  dos politicos com os votos dos eleitores)

----------------------------------------------------------------------

Projetos:
---------

Serviço WcfSapoContributorService

-> Servico acessado por colaboradores, que vão: 
   - cadastrar propostas (projetos que foram votados no congresso)
   - cadastrar políticos e o voto desses políticos nesses projetos

Serviço WcfSapoVoterService

-> Serviço acessado por eleitores interessados que vão:
   - votar nas propostas
   - ver o ranking dos políticos

Serviço WcfSapoManager + dll WcfSapoManagerLibrary

-> Serviço que reúne todas as informações: "persiste" os dados de WcfSapoContributorService e WcfSapoVoterService e calcula a nota dos políticos de acordo com a concordância dos votos deles (cadastrados pelos colaboradores) com os votos dos eleitores (cadastrados pelos eleitores que acessam o sistema).

Executável ConsoleSapoContributor
-> Colaborador cadastra propostas, e cadastra políticos

Executável ConsoleSapoVoter
-> Eleitor vota nas propostas e acessa ranking dos políticos
(o console está fazendo o cadastro de vários eleitores através de threads)

Executável ConsoleAdmin
-> cadastra dados de autenticacao, para permitir ao Colaborador acessar o sistema

-------------------------------------------------------------





