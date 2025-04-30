using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    // An abstract class used to create the base for items
    public abstract class Items
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Items(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
    // Inhibits from the abstract class Items and used for weapons
    public class Weapon : Items
    {
        public int AttackPower { get; set; }
        public Weapon(string name, string description, int attackPower) : base(name, description)
        {
            AttackPower = attackPower;
        }
        public override string ToString()
        {
            return Name;
        } 
    }
    // Inhibits from the abstract class Items and used for potions
    public class Potion : Items
    {
        public int Health { get; set; }
        public Potion(string name, string description, int health) : base(name, description)
        {
            Health = health;
        }
    }
}
