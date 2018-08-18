


Sistema de Avalia��o de Pol�ticos (SAPO)
========================================

Avalia os pol�ticos (deputados e senadores) quanto � concordancia de sua atua��o com a preferencia dos eleitores 

Atores envolvidos:
Administrador: cadastra usu�rio e senha de Colaboradores do sistema
Colaborador: cadastra informa��es sobre pol�ticos, propostas votadas no congresso nacional e como cada pol�tico votou
Eleitor: d� sua opini�o sobre as propostas votadas no congresso

A partir das informa��es adquiridas, o sistema d� um percentual de concord�ncia dos politicos com os eleitores 

----------------------------------------------------------------------

Rodando o programa

Ao debugar pelo Visual Studio, ser�o disparados os 3 execut�veis.
- ConsoleAdmin vai abrir, cadastrar os dados necess�rios de login do Colaboradores e fechar
- ConsoleContributor vai fazer a fun��o dos Colaboradores: adicionar as informa��es e solicitar que se clique "S" 
  para persistir as informa��es
- ConsoleElector vai abrir e fazer tentativas de verificar se h� informacoes no banco (inseridas por ConsoleContributor).
  Quando houver informacoes de propostas, vai cadastrar o voto dos eleitores naquelas propostas
  Ao final da execu��o de ConsoleElector, aparecer� o ranking (calculado como o percentual de concordancia dos votos
  dos politicos com os votos dos eleitores)

----------------------------------------------------------------------

Projetos:
---------

Servi�o WcfSapoContributorService

-> Servico acessado por colaboradores, que v�o: 
   - cadastrar propostas (projetos que foram votados no congresso)
   - cadastrar pol�ticos e o voto desses pol�ticos nesses projetos

Servi�o WcfSapoVoterService

-> Servi�o acessado por eleitores interessados que v�o:
   - votar nas propostas
   - ver o ranking dos pol�ticos

Servi�o WcfSapoManager + dll WcfSapoManagerLibrary

-> Servi�o que re�ne todas as informa��es: "persiste" os dados de WcfSapoContributorService e WcfSapoVoterService e calcula a nota dos pol�ticos de acordo com a concord�ncia dos votos deles (cadastrados pelos colaboradores) com os votos dos eleitores (cadastrados pelos eleitores que acessam o sistema).

Execut�vel ConsoleSapoContributor
-> Colaborador cadastra propostas, e cadastra pol�ticos

Execut�vel ConsoleSapoVoter
-> Eleitor vota nas propostas e acessa ranking dos pol�ticos
(o console est� fazendo o cadastro de v�rios eleitores atrav�s de threads)

Execut�vel ConsoleAdmin
-> cadastra dados de autenticacao, para permitir ao Colaborador acessar o sistema

-------------------------------------------------------------





