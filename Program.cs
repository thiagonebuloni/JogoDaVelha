namespace JogoDaVelha {

    class Program {


        private static void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[1] - Jogar");
            Console.WriteLine("[2] - Registrar jogador");
            Console.WriteLine("[3] - Ver ranking");
            Console.WriteLine("[0] - Sair\n");
            
        }


        private static void Jogar(List<string> jogadores, List<int> pontuacao)
        {
            Console.Clear();
            if (jogadores.Count() == 0) {
                
                jogadores.Add("Jogador 1");
                pontuacao.Add(0);
                jogadores.Add("Jogador 2");
                pontuacao.Add(0);
            }
            
            
            string[,] posicoes = new string[3,3];
            
            int contador = 1;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    posicoes[i,j] = $"_{contador}_";
                    contador++;
                }
            }
            
            // bool valendoo = true;
            string jogadorTurno = "X";
            int jogadaAtual = 0;

            while (true) {
                // laço para impedir selecionar mesma casa
                do {
                    MostraTabuleiroAtual(posicoes);
                    Console.Write($"\n{jogadorTurno}, escolha um número: ");
                    jogadaAtual = int.Parse(Console.ReadLine());
                } while (!ValidaJogada(posicoes, jogadaAtual, jogadorTurno));

                
                // valida se existe um vencedor
                int validaJogo = ValidaJogo(posicoes);
                Console.WriteLine(validaJogo);
                Console.ReadKey();
                if (validaJogo == 1) {
                    MostraTabuleiroAtual(posicoes);
                    if (jogadorTurno == "X") {
                        pontuacao[0]++;
                        Console.WriteLine($"\n{jogadores[0]} venceu!!");
                        Console.ReadKey();
                    }
                    else {
                        pontuacao[1]++;
                        Console.WriteLine($"\n{jogadores[1]} venceu!!");
                        Console.ReadKey();
                    }
                    break;
                }
                else if (validaJogo == -1) {
                    MostraTabuleiroAtual(posicoes);
                    Console.WriteLine("Ih deu velha!");
                    Console.ReadKey();
                    break;
                }

                // alterna turnos
                if (jogadorTurno == "X") jogadorTurno = "O";
                else jogadorTurno = "X";
            }
        }

        private static bool Empate(string[,] posicoes)
        {
            for (int i = 0; i < 3; i++) {
                for (int j = 0; i < 3; j++) {
                    if (posicoes[i,j] != "_X_" || posicoes[i,j] != "_O_") return false;
                }
            }
            return true;
        }

        private static bool ValidaJogada(string[,] posicoes, int jogadaAtual, string jogadorTurno)
        {
            if (jogadaAtual > 9 || jogadaAtual < 1) {
                Console.WriteLine("Escolha entre 1 e 9 apenas");
                Console.ReadKey();
                return false;
            }

            int contador = 1;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (contador == jogadaAtual) {      // preenche casa escolhida
                        if (posicoes[i,j] == "_X_" || posicoes[i,j] == "_O_") {
                            Console.WriteLine("Não é possível escolher essa casa, ela já está selecionada. \nTente novamente");
                            Console.ReadKey();
                            return false;
                        }
                        else {
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

        private static void MostraTabuleiroAtual(string[,] posicoes)
        {   
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 3; i++) {
                Console.Write(" ");
                for (int j = 0; j < 3; j++) {
                    if (posicoes[i,j] == "_X_") Cores($"{posicoes[i,j]}", ConsoleColor.DarkBlue);
                    else if (posicoes[i,j] == "_O_") Cores($"{posicoes[i,j]}", ConsoleColor.DarkRed);
                    else Cores($"{posicoes[i,j]}", ConsoleColor.DarkGreen);
                    if (j != 2) Cores("|", ConsoleColor.DarkGreen);
                }
                Console.WriteLine("");
            }

        }

        private static void Cores(string txt, ConsoleColor color) {
            // printa texto na cor selecionada e reseta para cor padrão
            Console.ForegroundColor = color;
            Console.Write(txt);
            Console.ResetColor();
        }

        private static void RegistrarUsuario(List<string> jogadores, List<int> pontuacao)
        {
            Console.Clear();
            Console.Write("Quer dar um nome para o seu usuário? [S/N] ");
            string resposta = Console.ReadLine();

            if (resposta != "N" && resposta != "n")
            {
                Console.Write("Qual é o nome? ");
                string nomeJogador = Console.ReadLine();
                jogadores.Add(nomeJogador);
                pontuacao.Add(0);
            }
            else {
                jogadores.Add($"Jogador {jogadores.Count() + 1}");
                pontuacao.Add(0);

            }
        }


        private static void VerRanking(List<string> jogadores, List<int> pontuacao)
        {
            Console.Clear();
            Console.WriteLine("\nJogadores\t| Pontuação");
            Console.WriteLine("==========================="); // Green

            for (int i = 0; i < jogadores.Count(); i++) {
                Console.WriteLine($"{jogadores[i]}\t| {pontuacao[i]}");
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }        


        public static void Main(string[] args) {

            // Criando listas de usuários
            List<string> jogadores = new List<string>();
            List<int> pontuacao = new List<int>();

            int option = 1;
            do {
                ShowMenu();
                Console.Write("O que você quer fazer? ");
                option = int.Parse(Console.ReadLine());

                switch (option){
                    case 0:
                        break;
                    case 1:
                        Jogar(jogadores, pontuacao);
                        break;
                    case 2:
                        RegistrarUsuario(jogadores, pontuacao);
                        break;
                    case 3:
                        VerRanking(jogadores, pontuacao);
                        break;
                }
                    

            } while (option != 0);
            
        }

    }
}