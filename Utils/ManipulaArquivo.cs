using System.Text.Json;

namespace JogoDaVelha.Utils {

    
    public class ManipulaArquivo
    {
    
            public static string path = @"data/";
            public static string file = @"ranking.txt";
            // public static string fullPath = System.IO.Path.Combine(path, file);
        public static void LeArquivo(List<Jogador> jogadores, string fullPath)
        {

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
            try
            {
                string jsonString = File.ReadAllText(fullPath);

                if (!String.IsNullOrEmpty(jsonString))
                {
                    List<Jogador> listaJogadores = JsonSerializer.Deserialize<List<Jogador>>(jsonString)!;
                    listaJogadores.ForEach(jogador => jogadores.Add(jogador));    
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


        public static void AtualizaArquivo(List<Jogador> jogadores, string fullPath)
        {
            // string path = @"data/";
            // string file = @"ranking.txt";
            // string fullPath = System.IO.Path.Combine(path, file);
            
            string jsonString = JsonSerializer.Serialize(jogadores);
            File.WriteAllText(fullPath, jsonString);
        }


    }
}