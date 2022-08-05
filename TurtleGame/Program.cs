namespace TurtleGame
{
    using TurtleGame.Engine;

    internal class Program
    {
        public static void Main()
        {
            var path = Directory.GetCurrentDirectory();

            var line = Console.ReadLine();

            if (line != null)
            {
                string[] files = line.Split(' ');

                if (files.Length >= 2)
                {
                    var settingsFile = $"{path}\\{files[0]}.txt";
                    var movesFile = $"{path}\\{files[1]}.txt";

                    if (File.Exists(settingsFile) && File.Exists(movesFile))
                    {
                        var game = new Game();

                        var messages = game.InnitializeBoard(settingsFile, movesFile);

                        foreach (var message in messages)
                        {
                            Console.WriteLine(message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Missing game-settings and moves files");
                    }
                }
            }
            
            Console.ReadLine();
        }
    }
}
