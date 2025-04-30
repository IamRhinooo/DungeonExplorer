using System;
using System.Diagnostics;

namespace DungeonExplorer
{
    // This class is used for testing whether an item is in the inventory
    // It is used with the Debug.Assert method within Game.cs 
    public class Testing
    {
        public bool IsItemInInventory(Inventory inventory, string item)
        {
            return inventory.InventoryContents().Contains(item);
        }
    }
}