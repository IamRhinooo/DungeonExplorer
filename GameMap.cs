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
        public Room BossRoom { get; set; }

        public GameMap()
        {
            var rustySword = new Weapon("Rusty Sword", "ol' fashioned and sometimes reliable", 10);
            var ironSword = new Weapon("Iron Sword", "used a long time ago, still sturdy", 15);
            var steelSword = new Weapon("Steel Sword", "a newly forged weapon", 20);

            var lesserHealthPotion = new Potion("Lesser Health Potion", "a deteriorated potion", 20);
            var healthPotion = new Potion("Health Potion", "a mysteriously brewed potion", 30);
            var greaterHealthPotion = new Potion("Greater Health Potion", "a powerful potion", 50);

            var goblin = new Monsters("Goblin", 50, 20);
            var skeleton = new Monsters("Skeleton", 40, 20);
            var bat = new Monsters("Bat", 15, 5);
            var spider = new Monsters("Spider", 20, 10);
            var zombieKing = new Monsters("Chicken Jockey", 80, 25);

            var room1 = new Room("-----\n\nReception\n", null);
            var room2 = new Room("-----\n\nSpider's Nest\n", spider, rustySword, lesserHealthPotion);
            var room3 = new Room("-----\n\nBat's Nest\n", bat, rustySword, lesserHealthPotion);
            var room4 = new Room("-----\n\nPiano Room\n", null, ironSword, healthPotion);
            var room5 = new Room("-----\n\nDining Room\n", null, ironSword, healthPotion);
            var room6 = new Room("-----\n\nGoblin's Camp\n", goblin, steelSword, greaterHealthPotion);
            var room7 = new Room("-----\n\nSkeleton's Fort\n", skeleton, steelSword, greaterHealthPotion);
            var bossRoom = new Room("-----\n\nChicken Jockey's Construction\n", zombieKing, greaterHealthPotion);

            room1.LeftRoom = room2;
            room1.RightRoom = room3;
            room2.LeftRoom = room4;
            room3.RightRoom = room5;
            room4.RightRoom = room6;
            room5.LeftRoom = room7;
            room6.RightRoom = bossRoom;
            room7.LeftRoom = bossRoom;

            StartRoom = room1;
            BossRoom = bossRoom;
        }
    }
}
