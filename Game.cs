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
            // Starts the game
            bool playing = true;

            while (playing)
            {
                // Displays description for the room and the initial player stats
                Console.WriteLine(currentRoom.GetDescription());
                PlayerStats();

                // Asks if they would like to pickup the health potion
                Console.WriteLine("Would you like to pickup the health potion at the door? yes or no\n");
                string response = Console.ReadLine();
                response.ToLower();

                // If the player picks up the potion, adds to inventory and displays the potion in inventory
                if (response == "yes")
                {
                    Console.WriteLine("\nYou picked up the health potion");
                    player.PickUpItem("Health potion (50HP)");
                    PlayerStats();
                }
                // If the players doesn't pickup the potion, it dissapears behind
                else if (response == "no")
                {
                    Console.WriteLine("\nYou left the health potion behind");
                }
                else
                {
                    Console.WriteLine("\nYou left the health potion behind");
                }

                // If they pick it up, they are then asked if they want to use it
                if (player.InventoryContents().Contains("Health potion (50HP)")) 

                // This asks the question to if they would like to use the potion
                { Console.WriteLine("You have picked up the health potion. Would you like to use it? yes or no\n");
                    string response2 = Console.ReadLine();
                    response2.ToLower();

                    // If they would like to use it, consumes the potion and adds 50 health to the player
                    if (response2 == "yes")
                    {
                        Console.WriteLine("\nYou have used the health potion");
                        player.Health = player.Health + 50;
                        player.RemoveItem("Health potion (50HP)");
                        PlayerStats();
                    }
                    // If they don't want to use it, adds it to their inventory
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
        {   // Adds the players stats 
            Console.WriteLine("\n" + "Name: " + player.Name);
            Console.WriteLine("Health: " + player.Health + " HP");
            Console.WriteLine("Inventory: " + player.InventoryContents() + "\n");
        }
    }
}