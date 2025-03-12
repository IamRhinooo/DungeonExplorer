using System;

namespace DungeonExplorer
{	
    public class Check
    {
        public bool IsItemInInventory(Player player, string item)
        {
            return player.InventoryContents().Contains(item);
        }
    }
}
