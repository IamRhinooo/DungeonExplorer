using System;
using System.Collections.Generic;
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
        // Filters items in inventory by type - weapons
        public IEnumerable<Weapon> GetWeapons()
        {
            return inventory.OfType<Weapon>();
        }
        // Filters items in inventory by type - potions
        public IEnumerable<Potion> GetPotions()
        {
            return inventory.OfType<Potion>();
        }
    }
}
