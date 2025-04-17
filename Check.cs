using System;

namespace DungeonExplorer
{	
    public class Testing
    {
        public bool IsItemInInventory(Inventory inventory, string item)
        {
            return inventory.InventoryContents().Contains(item);
        }
    }
}
