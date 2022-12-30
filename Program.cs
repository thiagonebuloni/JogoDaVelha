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


        private static void Jogar(List<string> jogadores, List<int> pontuacao, List<int> empates, List<int> derrotas, List<string> jogadoresRankeados, List<int> pontuacaoRankeados, List<int> empatesRankeados, List<int> derrotasRankeados)
        {
            Console.Clear();

            int indexJogadorAtivo1 = 0;
            int indexJogadorAtivo2 = 1;

            // se não existirem jogadores registrados, cria 2 novos
            if (jogadores.Count() == 0) {
                
                jogadores.Add("Jogador 1");
                pontuacao.Add(0);
                empates.Add(0);
                derrotas.Add(0);
                jogadores.Add("Jogador 2");
                pontuacao.Add(0);
                empates.Add(0);
                derrotas.Add(0);
            }
            else if ( jogadores.Count() == 1) { // se existir apenas 1 jogador criado, cria o segundo
                jogadores.Add($"Jogador {jogadores.Count() + 1}");
                pontuacao.Add(0);
                empates.Add(0);
                derrotas.Add(0);
            }
            else {      // seleciona jogadores já cadastrados
                for (int i = 0; i < jogadores.Count(); i++) {
                    if (i % 2 == 0) Console.ForegroundColor = ConsoleColor.DarkGreen;
                    else Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{i + 1} - {jogadores[i]}");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Selecione o jogador 1: ");
                indexJogadorAtivo1 = (int.Parse(Console.ReadLine()) - 1);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Selecione o jogador 2: ");
                indexJogadorAtivo2 = (int.Parse(Console.ReadLine()) - 1);
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

            while (true) {
                // laço para impedir selecionar mesma casa
                do {
                    MostraTabuleiroAtual(posicoes);
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (jogadorTurno == "X") Console.Write($"\n{jogadores[indexJogadorAtivo1]} [{jogadorTurno}], escolha um número: ");
                    else Console.Write($"\n{jogadores[indexJogadorAtivo2]} [{jogadorTurno}], escolha um número: ");
                    try {
                        jogadaAtual = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e) {
                        Cores("Opção inválida. Aperte Enter para tentar novamente.", ConsoleColor.Red);
                        Console.ReadKey();
                        continue;
                    }
                } while (!ValidaJogada(posicoes, jogadaAtual, jogadorTurno));

                // se empatado incrementa quantidade para cada jogador.
                if (ValidaJogo(posicoes) == -1) {
                    empates[indexJogadorAtivo1]++;
                    empates[indexJogadorAtivo2]++;
                    MostraTabuleiroAtual(posicoes);
                    Cores("\nIh deu velha!", ConsoleColor.Red);
                    Console.ReadKey();
                    break;
                }
                else if (ValidaJogo(posicoes) == 1) {       // se jogador 1 ganhar
                    MostraTabuleiroAtual(posicoes);
                    if (jogadorTurno == "X") {
                        pontuacao[indexJogadorAtivo1]++;
                        derrotas[indexJogadorAtivo2]++;
                        Console.WriteLine($"\n{jogadores[indexJogadorAtivo1]} venceu!!");
                        Console.ReadKey();
                    }
                    else {                                  // se jogador 2 ganhar
                        pontuacao[indexJogadorAtivo2]++;
                        derrotas[indexJogadorAtivo1]++;
                        Console.WriteLine($"\n{jogadores[indexJogadorAtivo2]} venceu!!");
                        Console.ReadKey();
                    }
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
                for (int j = 0; j < 3; j++) {
                    if (posicoes[i,j] != "_X_" && posicoes[i,j] != "_O_") {
                        return false;
                    }
                }
            }
            Console.WriteLine("EMPATADO");
            return true;
        }

        private static bool ValidaJogada(string[,] posicoes, int jogadaAtual, string jogadorTurno)
        {
            if (jogadaAtual > 9 || jogadaAtual < 1) {
                Cores("Escolha entre 1 e 9 apenas. Aperte Enter para tentar novamente.", ConsoleColor.Red);
                Console.ReadKey();
                return false;
            }

            int contador = 1;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (contador == jogadaAtual) {      // valida casa já preenchida
                        if (posicoes[i,j] == "_X_" || posicoes[i,j] == "_O_") {
                            Cores("Não é possível escolher essa casa, ela já está selecionada. \nAperte Enter para tentar novamente", ConsoleColor.Red);
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

        private static void RegistrarUsuario(List<string> jogadores, List<int> pontuacao, List<int> empates, List<int> derrotas, List<string> jogadoresRankeados, List<int> pontuacaoRankeados, List<int> empatesRankeados, List<int> derrotasRankeados)
        {
            Console.Clear();
            Console.Write("Quer dar um nome para o seu usuário? [S/N] ");
            string resposta = Console.ReadLine();

            if (resposta != "N" && resposta != "n")
            {
                Console.Write("Qual é o nome? ");
                string nomeJogador = Console.ReadLine();
                if (string.IsNullOrEmpty(nomeJogador)) {
                    jogadores.Add($"Jogador {jogadores.Count() + 1}");
                    pontuacao.Add(0);
                    empates.Add(0);
                }
                else {
                    jogadores.Add(nomeJogador);
                    pontuacao.Add(0);
                    empates.Add(0);
                }
            }
            else {
                jogadores.Add($"Jogador {jogadores.Count() + 1}");
                pontuacao.Add(0);
                empates.Add(0);

            }
        }


        private static void VerRanking(List<string> jogadores, List<int> pontuacao, List<int> empates, List<int> derrotas, List<string> jogadoresRankeados, List<int> pontuacaoRankeados, List<int> empatesRankeados, List<int> derrotasRankeados)
        {
            // limpa lista rankeados e copia dados de jogadores
            jogadoresRankeados.Clear();
            pontuacaoRankeados.Clear();
            empatesRankeados.Clear();
            derrotasRankeados.Clear();
            jogadoresRankeados.AddRange(jogadores);
            pontuacaoRankeados.AddRange(pontuacao);
            empatesRankeados.AddRange(empates);
            derrotasRankeados.AddRange(derrotas);

            // ordena jogadores
            for (int i = 0; i < jogadores.Count() - 1; i++) {
                for (int j = 1; j < jogadores.Count(); j++) {
                    if (pontuacaoRankeados[i] < pontuacaoRankeados[j]) {
                        string tempJogador = jogadoresRankeados[j];
                        jogadoresRankeados[j] = jogadoresRankeados[i];
                        jogadoresRankeados[i] = tempJogador;

                        int tempPontuacao = pontuacaoRankeados[j];
                        pontuacaoRankeados[j] = pontuacaoRankeados[i];
                        pontuacaoRankeados[i] = tempPontuacao;

                        int tempEmpates = empatesRankeados[j];
                        empatesRankeados[j] = empatesRankeados[i];
                        empatesRankeados[i] = tempEmpates;

                        int tempDerrotas = derrotasRankeados[j];
                        derrotasRankeados[j] = derrotasRankeados[i];
                        derrotasRankeados[i] = tempDerrotas;
                    }
                }
            }

            // tenta definir a distância entre campos da tabela com base no comprimento dos nomes dos jogadores
            string tab = "\t";
            Console.Clear();
            for (int i = 0; i < jogadores.Count(); i++) {
                if (jogadores[i].Length > 7) {
                    tab += "\t";
                    break;
                }
            }
            Console.WriteLine("\nJogadores" + tab + "| Vitórias\t| Derrotas\t| Empates");
            Console.WriteLine("=================================================================");

            for (int i = 0; i < jogadoresRankeados.Count(); i++) {
                // alterna cores na tabela
                if (i % 2 == 0) Console.ForegroundColor = ConsoleColor.DarkGreen;
                else Console.ForegroundColor = ConsoleColor.Green;

                if (jogadoresRankeados[i].Length > 14) {
                    Console.WriteLine($"{jogadoresRankeados[i]}{tab}| {pontuacaoRankeados[i]}\t| {derrotasRankeados[i]}\t| {empatesRankeados[i]}");
                }
                else if (jogadoresRankeados[i].Length < 8){
                    Console.WriteLine($"{jogadoresRankeados[i]}{tab}\t| {pontuacaoRankeados[i]}\t| {derrotasRankeados[i]}\t| {empatesRankeados[i]}");
                }
                else {
                    Console.WriteLine($"{jogadoresRankeados[i]}{tab}| {pontuacaoRankeados[i]}{tab}| {derrotasRankeados[i]}{tab}| {empatesRankeados[i]}");
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }        


        public static void Main(string[] args) {

            // Criando listas de usuários
            List<string> jogadores = new List<string>();
            List<int> pontuacao = new List<int>();
            List<int> empates = new List<int>();
            List<string> jogadoresRankeados = new List<string>();
            List<int> pontuacaoRankeados = new List<int>();
            List<int> empatesRankeados = new List<int>();
            List<int> derrotas = new List<int>();
            List<int> derrotasRankeados = new List<int>();

            // cria arquivo json 

            int option = 1;
            do {
                ShowMenu();
                Console.Write("O que você quer fazer? ");
                    try {
                        option = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e) {
                        Cores("Opção inválida. Aperte Enter para tentar novamente.", ConsoleColor.Red);
                        Console.ReadKey();
                        continue;
                    }

                switch (option){
                    case 0:
                        break;
                    case 1:
                        Jogar(jogadores, pontuacao, empates, derrotas, jogadoresRankeados, pontuacaoRankeados, empatesRankeados, derrotasRankeados);
                        break;
                    case 2:
                        RegistrarUsuario(jogadores, pontuacao, empates, derrotas, jogadoresRankeados, pontuacaoRankeados, empatesRankeados, derrotasRankeados);
                        break;
                    case 3:
                        VerRanking(jogadores, pontuacao, empates, derrotas, jogadoresRankeados, pontuacaoRankeados, empatesRankeados, derrotasRankeados);
                        break;
                    default:
                        Cores("Opção inválida. Aperte enter para tentar novamente.", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }
                    

            } while (option != 0);
            
        }

    }
}