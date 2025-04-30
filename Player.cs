using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public Weapon EquippedWeapon { get; set; }

        // Contains the players stats and equipped weapon
        public Player(string name, int health, int attack) 
        {
            Name = name;
            Health = health;
            Attack = attack;

            EquippedWeapon = null;
        }
    }
}