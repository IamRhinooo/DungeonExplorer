using System;
using System.Media;
using System.Runtime.CompilerServices;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            // Initialisation of the game with one room and one player
            currentRoom = new Room("You open the doors to the dungeon of tales.\nThere is a potion led on the floor infront of you and a closed door directly infront of you.\nIt seems the doors to your left and to your right have been barricaded.");
            player = new Player("Gerrard", 50);
        }
        public void Start()
        {
            // start of the game
            bool playing = true;

            while (playing)
            {
                // Code your playing logic here

                Console.WriteLine(currentRoom.GetDescription());
                PlayerStats();

                Console.WriteLine("Would you like to pickup the health potion at the door? yes or no\n");
                string response = Console.ReadLine();

                if (response == "yes")
                {
                    Console.WriteLine("\nYou picked up the health potion");
                    player.PickUpItem("Health potion (50HP)");
                    PlayerStats();
                }
                else if (response == "no")
                {
                    Console.WriteLine("\nYou left the health potion behind");
                }
                else
                {
                    Console.WriteLine("\nYou left the health potion behind");
                }

                if (player.InventoryContents().Contains("Health potion (50HP)")) 

                { Console.WriteLine("You have picked up the health potion. Would you like to use it? yes or no\n");
                    string response2 = Console.ReadLine();
                    if (response2 == "yes")
                    {
                        Console.WriteLine("\nYou have used the health potion");
                        player.Health = player.Health + 50;
                        player.RemoveItem("Health potion (50HP)");
                        PlayerStats();
                    }
                    else
                    {
                        Console.WriteLine("You left the item in your inventory");
                    }
                }
                // End of the game
                playing = false;
            }
        }
        private void PlayerStats()
        {   // Add the player stats to the console
            Console.WriteLine("\n" + "Name: " + player.Name);
            Console.WriteLine("Health: " + player.Health + " HP");
            Console.WriteLine("Inventory: " + player.InventoryContents() + "\n");
        }
    }
}