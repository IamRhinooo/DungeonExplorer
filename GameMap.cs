using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class GameMap
    {
        public Room StartRoom { get; set; }

        public GameMap()
        {
            var rustySword = new Weapon("Rusty Sword", "ol' fashioned and sometimes reliable", 5);
            var ironSword = new Weapon("Steel Sword", "used a long time ago", 10);
            var steelSword = new Weapon("Iron Sword", "a newly forged weapon", 15);
            var titaniumSword = new Weapon("Iron Sword", "a newly forged weapon", 15);

            var healthPotion = new Potion("Health Potion", "a mysteriously brewed potion", 20);

            var goblin = new Monsters("Goblin", 10, 5);

            var room1 = new Room("testing room 1\n", null);
            var room2 = new Room("testing room 2\n", goblin, rustySword, healthPotion);
            var room3 = new Room("testing room 3\n", null, healthPotion, steelSword);
            var room4 = new Room("testing room 4\n", null, steelSword);
            var room5 = new Room("testing room 5\n");
            var room6 = new Room("testing room 6\n");
            var room7 = new Room("testing room 7\n");
            var bossRoom = new Room("testing room boss room\n");

            room1.LeftRoom = room2;
            room1.RightRoom = room3;
            room2.LeftRoom = room4;
            room3.RightRoom = room5;
            room4.RightRoom = room6;
            room5.LeftRoom = room7;
            room6.RightRoom = bossRoom;
            room7.LeftRoom = bossRoom;

            StartRoom = room1;
        }
    }
}
