using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public class Room
    {
        private string description;

        public Room LeftRoom { get; set; }
        public Room RightRoom { get; set; }
        public List<Items> Items { get; set; }
        public Monsters Monster { get; set; }

        // Constructor for the Room class and allows for the creation of rooms in the GameMap.cs 
        // They can have optional monsters and numerous items in each room
        public Room(string description, Monsters monster = null, params Items[] items)
        {
            this.description = description;
            Monster = monster;
            Items = new List<Items>(items); 
        }
        // Used to get the description of the current room
        // Displays the items and monsters within the room 
        public string GetDescription()
        {
            var itemsDescription = Items != null && Items.Count > 0
                ? "The following items remain on the floor: " + string.Join(", ", Items.Select(i => i.Name))
                : "There are no items in this room.";

            var monsterDescription = Monster != null
                ? $"A {Monster.Name} lies within this room.\n\n-----\n"
                : "There are no monsters in this room.\n\n-----\n";

            return description + "\n" + itemsDescription + "\n" + monsterDescription;
        }
    }
}