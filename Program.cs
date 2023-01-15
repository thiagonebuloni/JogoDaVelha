using JogoDaVelha.Utils;
using System.Text.Json;

namespace JogoDaVelha {

    class Program {


        public static void Main(string[] args)
        {


            // Criando listas de usuários
            List<Jogador> jogadores = new List<Jogador>();            

            string path = @"data/";
            string file = @"ranking.txt";
            string fullPath = System.IO.Path.Combine(path, file);
            
            ManipulaArquivo.LeArquivo(jogadores, fullPath);


            int option = 1;
            do
            {
                Interface.IShowMenu();
                Console.Write("O que você quer fazer? ");
                    try
                    {
                        option = int.Parse(Console.ReadLine()!);
                    }
                    catch (Exception)
                    {
                        Interface.ICores("Opção inválida. Aperte Enter para tentar novamente.", ConsoleColor.Red);
                        Console.ReadKey();
                        continue;
                    }

                switch (option){
                    case 0:
                        break;
                    case 1:
                        LogicaJogo.Jogar(jogadores, fullPath);
                        break;
                    case 2:
                        Jogador.RegistrarJogador(jogadores, jogadores.Count());
                        break;
                    case 3:
                        Interface.IVerRanking(jogadores);
                        break;
                    default:
                        Interface.ICores("Opção inválida. Aperte enter para tentar novamente.", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }
                    

            } while (option != 0);
            
        }

    }
}