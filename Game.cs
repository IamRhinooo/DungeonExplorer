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
            // Initialize the game with one room and one player
            currentRoom = new Room("");
            player = new Player("Gerrard", 50);
        }
        public void Start()
        {
            // Change the playing logic into true and populate the while loop
            bool playing = true;

            while (playing)
            {
                // Code your playing logic here

                PlayerStats();
                
                player.PickUpItem("Harrisons dedeorant");
                string Inventory = player.InventoryContents();

                playing = false;
            }
        }
        private void PlayerStats()
        {   // Add the player stats to the console
            Console.WriteLine("Name: " + player.Name);
            Console.WriteLine("Health: " + player.Health);
            Console.WriteLine("Inventory: " + player.InventoryContents());
        }
    }
}