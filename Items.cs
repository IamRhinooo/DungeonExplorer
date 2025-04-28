using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Items
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Items(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
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
    public class Potion : Items
    {
        public int Health { get; set; }
        public Potion(string name, string description, int health) : base(name, description)
        {
            Health = health;
        }
    }
}
