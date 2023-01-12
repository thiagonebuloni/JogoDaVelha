
namespace JogoDaVelha.Utils {

    
    public class ManipulaArquivo {
    
            public static string path = @"data/";
            public static string file = @"ranking.txt";
            public static string fullPath = System.IO.Path.Combine(path, file);
        public static void LeArquivo(List<string> jogadores, List<int> pontuacao, List<int> empates, List<int> derrotas) {

            // verifica existencia da pasta data
            try {
                if (!System.IO.Directory.Exists(path)) {
                    System.IO.Directory.CreateDirectory(path);
                }
            }
            catch (Exception e) {
                string log = @"data/log.txt";
                if (!System.IO.File.Exists(log)) {
                    using (StreamWriter sw = new StreamWriter(log)) {
                        sw.Write($"{System.DateTime.Now} ");
                        sw.WriteLine($"LeArquivo-PathExists-{e.Message}");
                    }
                }
                else {
                    using (StreamWriter sw = File.AppendText(log)) {
                        sw.Write($"{System.DateTime.Now} ");
                        sw.WriteLine($"LeArquivo-PathExists{e.Message}");
                    }
                }
            }

            // Lê arquivo e recupera ranking
            try {
                if (System.IO.File.Exists(fullPath)) {
                    using (StreamReader sr = new StreamReader(fullPath)) {
                        Console.WriteLine("Antes Reader");          // DEBUG
                        string[] lines = System.IO.File.ReadAllLines(fullPath);
                        
                        if (lines.Length > 0) {     // verifica se 
                        int i = 0;
                                              
                            while (lines != null) {
                                Console.WriteLine("Antes de line. Dentro WHILE");
                                string[] line = sr.ReadLine()!.Split();
                                Console.WriteLine($"line - {line}");
                                Console.ReadKey();

                                foreach (string x in line) {
                                    Console.WriteLine($"{x}");
                                }
                                
                                // atribui informações do arquivo nas listas
                                Console.WriteLine($"line[0] - {line[0]}");
                                jogadores[i] = line[0];
                                Console.WriteLine($"line[1] - {line[1]}");
                                pontuacao[i] = int.Parse(line[1]);
                                Console.WriteLine($"line[2] - {line[2]}");
                                derrotas[i] = int.Parse(line[2]);
                                Console.WriteLine($"line[3] - {line[3]}");
                                empates[i] = int.Parse(line[3]);
                                Console.WriteLine($"{jogadores[i]}"); // debug
                                Console.ReadKey();
                                i++;

                            }
                        }
                    }
                }
                
            }
            catch (Exception e) {
                string log = @"data/log.txt";
                if (!System.IO.File.Exists(log)) {
                    using (StreamWriter sw = new StreamWriter(log)) {
                        sw.Write($"{System.DateTime.Now} ");
                        sw.WriteLine(e.Message);
                        Console.WriteLine($"LeArquivo - {e.Message}"); // debug
                        Console.ReadKey(); // debug
                    }
                }
                else {
                    using (StreamWriter sw = File.AppendText(log)) {
                        sw.Write($"{System.DateTime.Now} ");
                        sw.WriteLine($"LeArquivo-File exists-{e.Message}");
                        Console.WriteLine($"{e.Message}"); // debug
                        Console.ReadKey(); // debug
                    }
                }
            }
        }


        public static void AtualizaArquivo(List<Jogador> jogadores)
        {
            string path = @"data/";
            string file = @"ranking.txt";
            string fullPath = System.IO.Path.Combine(path, file);
            
            for (int i = 0; i < jogadores.Count(); i++) {
                using (StreamWriter sw = new StreamWriter(fullPath)) {
                    sw.WriteLine($"{jogadores[i].NomeJogador}|{jogadores[i].Vitorias}|{jogadores[i].Derrotas}|{jogadores[i].Empates}");
                }
            }
        }


    }
}