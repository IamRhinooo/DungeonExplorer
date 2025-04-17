using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<string> inventory = new List<string>();

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
