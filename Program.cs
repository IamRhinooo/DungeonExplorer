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
            GameMap map = new GameMap();
            Game game = new Game(map);
            game.Start();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
