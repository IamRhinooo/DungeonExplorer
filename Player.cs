using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }
        // Picks up items and adds them to inventory
        public void PickUpItem(string item)
        {
            inventory.Add(item);
        }
        // Removes items from inventory when used
        public void RemoveItem(string item)
        {
            inventory.Remove(item);
        }
        // Contains all items in inventory
        public string InventoryContents()
        {
            return string.Join(", ", inventory);
        }
    }
}