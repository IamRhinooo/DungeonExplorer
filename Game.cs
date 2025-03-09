using System;
using System.Media;

namespace DungeonExplorer
{
    internal class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            player = new Player("Gerrard", 100);
            // Initialize the game with one room and one player

        }
        public void Start()
        {
            // Change the playing logic into true and populate the while loop
            bool playing = false;



            while (playing)
            {
                // Code your playing logic here
                player.PickUpItem("Harrisons dedeorant");
                string Inventory = player.InventoryContents();
            }
        }
    }
}