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
            var rustySword = new Weapon("Rusty Sword", "an old fashioned weapon", 5);
            var steelSword = new Weapon("Steel Sword", "a newly forged weapon", 15);

            var healthPotion = new Potion("Health Potion", "a mysteriously brewed potion", 20);

            var room1 = new Room("testing room 1", rustySword);
            var room2 = new Room("testing room 2", healthPotion);
            var room3 = new Room("testing room 3");
            var room4 = new Room("testing room 4");
            var room5 = new Room("testing room 5");
            var room6 = new Room("testing room 6");
            var room7 = new Room("testing room 7");
            var bossRoom = new Room("testing room boss room");

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
