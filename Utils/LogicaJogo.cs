namespace JogoDaVelha.Utils {
    
    
    public class LogicaJogo {

        public static void Jogar(List<Jogador> jogadores) {
            
            
            Console.Clear();

            int indexJogadorAtivo1 = 0;
            int indexJogadorAtivo2 = 1;

            // se não existirem jogadores registrados, cria 2 novos
            if (jogadores.Count() == 0)
            {    
                Jogador.RegistrarJogador(jogadores);
                Jogador.RegistrarJogador(jogadores);
            }
            else if ( jogadores.Count() == 1)
            { // se existir apenas 1 jogador criado, cria o segundo
                Jogador novoJogador = new Jogador("Jogador(a) 2", 0, 0, 0);
            }
            else {      // seleciona jogadores já cadastrados
                for (int i = 0; i < jogadores.Count(); i++) {
                    if (i % 2 == 0) Console.ForegroundColor = ConsoleColor.DarkGreen;
                    else Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine($"{i + 1} - {jogadores[i].NomeJogador}");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Selecione o jogador(a) 1: ");
                indexJogadorAtivo1 = (int.Parse(Console.ReadLine()!) - 1);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Selecione o jogador(a) 2: ");
                indexJogadorAtivo2 = (int.Parse(Console.ReadLine()!) - 1);
            }


            // cria e popula posicoes de 1 a 9
            string[,] posicoes = new string[3,3];
            
            int contador = 1;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    posicoes[i,j] = $"_{contador}_";
                    contador++;
                }
            }
            
            string jogadorTurno = "X";
            int jogadaAtual = 0;

            // laço do jogo
            while (true) {
                // laço para impedir selecionar mesma casa
                do {
                    Interface.MostraTabuleiroAtual(posicoes);

                    if (jogadorTurno == "X") Interface.Cores($"\n{jogadores[indexJogadorAtivo1].NomeJogador} [{jogadorTurno}], escolha um número: ", ConsoleColor.Blue);
                    else Interface.Cores($"\n{jogadores[indexJogadorAtivo2].NomeJogador} [{jogadorTurno}], escolha um número: ", ConsoleColor.Red);
                    try {
                        jogadaAtual = int.Parse(Console.ReadLine()!);
                    }
                    catch (Exception) {
                        Interface.Cores("Opção inválida. Aperte Enter para tentar novamente.", ConsoleColor.Red);
                        Console.ReadKey();
                        continue;
                    }
                } while (!ValidaJogada(posicoes, jogadaAtual, jogadorTurno));

                // se empatado incrementa quantidade para cada jogador.
                int resultado = ValidaJogo(posicoes);
                if (resultado == -1) {
                    jogadores[indexJogadorAtivo1].IncrementaEmpates();
                    jogadores[indexJogadorAtivo2].IncrementaEmpates();
                    Interface.MostraTabuleiroAtual(posicoes);
                    Interface.Cores("\nIh deu velha!", ConsoleColor.Red);
                    Console.ReadKey();
                    break;
                }
                else if (resultado == 1) { 
                    Interface.MostraTabuleiroAtual(posicoes);
                    if (jogadorTurno == "X") {              // se jogador 1 ganhar
                        jogadores[indexJogadorAtivo1].IncrementaVitorias();
                        jogadores[indexJogadorAtivo2].IncrementaDerrotas();
                        Interface.Cores($"\n{jogadores[indexJogadorAtivo1].NomeJogador} venceu!!", ConsoleColor.Blue);
                        Console.ReadKey();
                    }
                    else {                                  // se jogador 2 ganhar
                        jogadores[indexJogadorAtivo2].IncrementaVitorias();
                        jogadores[indexJogadorAtivo1].IncrementaDerrotas();
                        Interface.Cores($"\n{jogadores[indexJogadorAtivo2].NomeJogador} venceu!!", ConsoleColor.Red);
                        Console.ReadKey();
                    }
                    break;
                }

                // atualiza arquivo quando terminar cada jogo.

                // alterna turnos
                if (jogadorTurno == "X") jogadorTurno = "O";
                else jogadorTurno = "X";
            }
            
            Jogador.OrdenarJogadores(jogadores);
            ManipulaArquivo.AtualizaArquivo(jogadores);
            
        }


        

        private static bool Empate(string[,] posicoes)
        {
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (posicoes[i,j] != "_X_" && posicoes[i,j] != "_O_") {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool ValidaJogada(string[,] posicoes, int jogadaAtual, string jogadorTurno)
        {
            if (jogadaAtual > 9 || jogadaAtual < 1) {
                Interface.Cores("Escolha entre 1 e 9 apenas. Aperte Enter para tentar novamente.", ConsoleColor.Red);
                Console.ReadKey();
                return false;
            }

            int contador = 1;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (contador == jogadaAtual) {      // valida casa já preenchida
                        if (posicoes[i,j] == "_X_" || posicoes[i,j] == "_O_") {
                            Interface.Cores("Não é possível escolher essa casa, ela já está selecionada. \nAperte Enter para tentar novamente", ConsoleColor.Red);
                            Console.ReadKey();
                            return false;
                        }
                        else {                          // preenche casa escolhida
                            posicoes[i,j] = $"_{jogadorTurno}_";
                            return true;
                        }                            
                    }
                    contador++;
                }
            }
            return false;
        }

        private static int ValidaJogo(string[,] posicoes)
        {
            bool horizontal1 = posicoes[0,0] == posicoes[0,1] && posicoes[0,0] == posicoes[0,2];
            bool horizontal2 = posicoes[1,0] == posicoes[1,1] && posicoes[1,0] == posicoes[1,2];
            bool horizontal3 = posicoes[2,0] == posicoes[2,1] && posicoes[2,0] == posicoes[2,2];

            if (horizontal1 || horizontal2 || horizontal3) return 1;

            bool vertical1 = posicoes[0,0] == posicoes[1,0] && posicoes[0,0] == posicoes[2,0];
            bool vertical2 = posicoes[0,1] == posicoes[1,1] && posicoes[0,1] == posicoes[2,1];
            bool vertical3 = posicoes[0,2] == posicoes[1,2] && posicoes[0,2] == posicoes[2,2];

            if (vertical1 || vertical2 || vertical3) return 1;

            bool diagonal1 = posicoes[0,0] == posicoes[1,1] && posicoes[0,0] == posicoes[2,2];
            bool diagonal2 = posicoes[0,2] == posicoes[1,1] && posicoes[0,2] == posicoes[2,0];

            if (diagonal1 || diagonal2) return 1;
            
            if (Empate(posicoes)) return -1;
            
            return 0;
        }

    }
}