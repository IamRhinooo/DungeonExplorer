using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Monsters
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }

        // Constructor for the Monsters class and allows the creation of monsters in the GameMap.cs
        public Monsters(string name, int health, int attackPower)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
        }

        // Used for the combat for the monster against the player in Game.cs
        public void Attack(Player player)
        {
            Console.WriteLine($"{Name} attacks {player.Name} for {AttackPower} damage!");
            player.Health -= AttackPower;
        }
    }
}
