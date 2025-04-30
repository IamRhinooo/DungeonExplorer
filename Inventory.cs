using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<Items> inventory = new List<Items>();

        // Picks up items and adds them to inventory
        public void PickUpItem(Items item)
        {
            inventory.Add(item);
            // Debugging line to check if the item was added to inventory
            Debug.Assert(inventory.Contains(item), "The item slipped out of your hands, it was not added to inventory.");
        }
        // Removes items from inventory when used
        public void RemoveItem(Items item)
        {
            inventory.Remove(item);
        }
        // Contains all items in inventory
        public string InventoryContents()
        {
            return string.Join(", ", inventory.Select(i => i.Name));
        }
        // Questions, Gets and returns items in inventory by type - weapons
        // Questions are done via LINQ
        public IEnumerable<Weapon> GetWeapons()
        {
            return inventory.OfType<Weapon>();
        }
        // Questions, Gets and returns items in inventory by type - potions
        // Questions are done via LINQ
        public IEnumerable<Potion> GetPotions()
        {
            return inventory.OfType<Potion>();
        }
    }
}
