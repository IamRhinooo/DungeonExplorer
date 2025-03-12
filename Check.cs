using System;

namespace DungeonExplorer
{	
    public class Testing
    {
        public bool IsItemInInventory(Player player, string item)
        {
            return player.InventoryContents().Contains(item);
        }
    }
}
