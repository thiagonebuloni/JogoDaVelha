namespace JogoDaVelha.Utils {

    public class Testes {


        public static void TestesOrdenaJogadores(List<Jogador> jogadores) {

            string x = "s";

            while (x != "n") {
                for (int i = 0; i < jogadores.Count(); i++) {
                    Console.WriteLine("Jogadores:");
                    Console.WriteLine($"{jogadores[i].NomeJogador}");
                    Console.WriteLine($"{jogadores[i].Vitorias}");
                    Console.WriteLine($"{jogadores[i].Derrotas}");
                    Console.WriteLine($"{jogadores[i].Empates}");
                    Console.ReadKey();
                }
                Jogador.OrdenarJogadores(jogadores);
                
                for (int i = 0; i < jogadores.Count(); i++) {
                    Console.WriteLine("Jogadores:");
                    Console.WriteLine($"{jogadores[i].NomeJogador}");
                    Console.WriteLine($"{jogadores[i].Vitorias}");
                    Console.WriteLine($"{jogadores[i].Derrotas}");
                    Console.WriteLine($"{jogadores[i].Empates}");
                    Console.ReadKey();
                }
                Console.WriteLine("X: ");
                x = Console.ReadLine()!;
            }
        }
    }
}