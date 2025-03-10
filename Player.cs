﻿using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }
        public void PickUpItem(string item)
        {
            inventory.Add(item);
        }
        public void RemoveItem(string item)
        {
            inventory.Remove(item);
        }
        public string InventoryContents()
        {
            return string.Join(", ", inventory);
        }
    }
}