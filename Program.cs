using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        // Starts and ends the game
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
