using DungeonExplorer;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
   internal class Game
    {
        private Player player;
        private Room currentRoom;
        private Testing testing;
        private Inventory inventory = new Inventory();
        public Game(GameMap map)
        {
            // Initialisation of the game with one room and one player
            currentRoom = map.StartRoom;
            player = new Player("Gerrard", 50, 10);
            testing = new Testing();
        }
        public void Controls(string direction)
        {
            switch (direction.ToLower())
            {
                case "left":
                    if (currentRoom.Monster != null)
                    {
                        Console.WriteLine("You cannot leave the room while a monster is present. You must defeat it first.");
                        return;
                    }
                    if (currentRoom.LeftRoom != null)
                    {
                        currentRoom = currentRoom.LeftRoom;
                        Console.WriteLine("\nYou have entered the new room\n");
                        Console.WriteLine(currentRoom.GetDescription());

                        while (currentRoom.Items.Count > 0)
                        {
                            Console.WriteLine("Would you like to pick up an item? (yes/no)\n");
                            string response = Console.ReadLine().ToLower();
                            if (response == "yes")
                            {
                                Console.WriteLine("\nWhich item would you like to pick up? (Enter the name)\n");
                                string itemName = Console.ReadLine();
                                var selectedItem = currentRoom.Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                                if (selectedItem != null)
                                {
                                    inventory.PickUpItem(selectedItem);
                                    currentRoom.Items.Remove(selectedItem);
                                    Console.WriteLine($"\nYou picked up the {selectedItem.Name}.\n");
                                }
                                else
                                {
                                    Console.WriteLine("\nItem not found in the room.");
                                }
                            }
                            if (response == "no")
                            {
                                Console.WriteLine("\nYou left the item in the room.\n");
                                break; 
                            }
                            if (response != "yes" && response != "no")
                            {
                                Console.WriteLine("\nInvalid response, please check above!\n");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYou can't go that way.");
                    }
                    break;
                case "right":
                    if (currentRoom.Monster != null)
                    {
                        Console.WriteLine("You cannot leave the room while a monster is present. You must defeat it first.");
                        return;
                    }
                    if (currentRoom.RightRoom != null)
                    {
                        currentRoom = currentRoom.RightRoom;
                        Console.WriteLine("\nYou have entered the new room\n");
                        Console.WriteLine(currentRoom.GetDescription());

                        while (currentRoom.Items.Count > 0)
                        {
                            Console.WriteLine("Would you like to pick up an item? (yes/no)\n");
                            string response = Console.ReadLine().ToLower();
                            if (response == "yes")
                            {
                                Console.WriteLine("\nWhich item would you like to pick up? (Enter the name)\n");
                                string itemName = Console.ReadLine();
                                var selectedItem = currentRoom.Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
                                if (selectedItem != null)
                                {
                                    inventory.PickUpItem(selectedItem);
                                    currentRoom.Items.Remove(selectedItem);
                                    Console.WriteLine($"\nYou picked up the {selectedItem.Name}.\n");
                                }
                                else
                                {
                                    Console.WriteLine("\nItem not found in the room.");
                                }
                            }
                            if (response == "no")
                            {
                                Console.WriteLine("\nYou left the item in the room.\n");
                                break;
                            }
                            if (response != "yes" && response != "no")
                            {
                                Console.WriteLine("\nInvalid response, please check above!\n");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYou can't go that way.");
                    }
                    break;
                case "stats":
                    PlayerStats();
                    break;
                case "equip":
                    EquipItem();
                    break;
                case "use":
                    UseItem();
                    break;
                case "attack":
                    if (currentRoom.Monster != null)
                    {
                        Combat(currentRoom.Monster);
                    }
                    else
                    {
                        Console.WriteLine("\nThere is no monster to attack in this room.\n");
                    }
                    break;
                case "exit":
                    break;
                default:
                    Console.WriteLine("\nInvalid response, please check above!");
                    break;
            }
        }
        public void Start()
        {
            // Starts the game
            bool playing = true;

            while (playing)
            {
                Console.WriteLine("You wake up in a dark mysterious room. \nVarious sounds can be heard from up ahead with lots of danger close. \nWhat will you do here? \nGoodluck.\n");
                Console.WriteLine(currentRoom.GetDescription());
                Console.WriteLine("What would you like to do? (left/right/attack/stats/use/equip/exit)\n");
                string input = Console.ReadLine();
                input = input.ToLower();

                Controls(input);
                // End of the game
                if (input == "exit")
                {
                    Console.WriteLine("\nThank you for playing!\n");
                    playing = false;
                }
            }
        }
        private void PlayerStats()
        {   // Adds the players stats 
            Console.WriteLine("\n" + "Name: " + player.Name);
            Console.WriteLine("Health: " + player.Health + " HP");
            Console.WriteLine("Attack: " + player.Attack);
            Console.WriteLine("Inventory: " + inventory.InventoryContents());
            Console.WriteLine("Equipped weapon: " + (player.EquippedWeapon != null ? player.EquippedWeapon.Name : "None") + "\n");
        }
        private void EquipItem()
        {
            // Equips the item from the inventory
            var weapons = inventory.GetWeapons().ToList();
            if (weapons.Count() == 0)
            {
                Console.WriteLine("\nYou have no weapons in your inventory to equip.");
                return;
            }
            Console.WriteLine("\nWhich weapon would you like to equip? \n");
            foreach (var weapon in weapons)
            {
                Console.WriteLine($"- {weapon.Name} - Attack Power: {weapon.AttackPower}\n");
            }
            string weaponName = Console.ReadLine();
            var selectedWeapon = inventory.GetWeapons().FirstOrDefault(w => w.Name.Equals(weaponName, StringComparison.OrdinalIgnoreCase));
            if (selectedWeapon != null)
            {
                if (player.EquippedWeapon != null)
                {
                    Console.WriteLine($"\nYou have unequipped the {player.EquippedWeapon.Name}.");
                    player.Attack -= player.EquippedWeapon.AttackPower; 
                    inventory.PickUpItem(player.EquippedWeapon);
                }

                Console.WriteLine($"\nYou have equipped the {selectedWeapon.Name}.\n");
                inventory.RemoveItem(selectedWeapon);
                player.EquippedWeapon = selectedWeapon;
                player.Attack += selectedWeapon.AttackPower; 
            }
            else
            {
                Console.WriteLine("\nItem not found in inventory.");
            }
        }
        private void UseItem()
        {
            // Uses the item from the inventory
            var potions = inventory.GetPotions().ToList();
            if (potions.Count() == 0)
            {
                Console.WriteLine("\nYou have no potions in your inventory to use.");
                return;
            }
            Console.WriteLine("Your inventory contains the following potions: \n");
            foreach (var potion in potions)
            {
                Console.WriteLine($"- {potion.Name} - Healing Amount: {potion.Health}\n");
            }
            Console.WriteLine("Which potion would you like to use? \n");
            string potionName = Console.ReadLine();
            var selectedPotion = inventory.GetPotions().FirstOrDefault(p => p.Name.Equals(potionName, StringComparison.OrdinalIgnoreCase));
            if (selectedPotion != null)
            {
                player.Health += selectedPotion.Health;
                inventory.RemoveItem(selectedPotion);
                Console.WriteLine($"You have used the {selectedPotion.Name}. Your health is now {player.Health} HP.\n");

                if (player.Health > 100)
                {
                    player.Health = 100; // Cap the health at 100
                    Console.WriteLine("You have reached max health.");
                }
            }
            else
            {
                Console.WriteLine("\nItem not found in inventory.");
            }
        }
        public void Combat(Monsters monster)
        {
            Console.WriteLine($"\nA {monster.Name} has been found. It has {monster.Health} HP.");

            while (player.Health > 0 && monster.Health > 0)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use Potion");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    monster.Health -= player.Attack;
                    Console.WriteLine($"You attack the {monster.Name} for {player.Attack} damage.");
                    if (monster.Health > 0)
                    {
                        Console.WriteLine($"The {monster.Name} has {monster.Health} HP left.");
                        monster.Attack(player);
                    }
                }
                else if (choice == "2")
                {
                    if (inventory.GetPotions().Count() == 0)
                    {
                        Console.WriteLine("\nYou have no potions to use.\n");
                        continue;
                    }
                    if (inventory.GetPotions().Count() > 0)
                    {
                        UseItem();
                    }
                }
                // Testing 
                //else if (choice == "3")
                //{
                //    Console.WriteLine("good turn");
                //}
                else
                {
                    Console.WriteLine("Invalid choice. Please choose again.");
                    continue;
                }
                if (monster.Health <= 0)
                {
                    Console.WriteLine($"\nYou have defeated the {monster.Name}!");
                    currentRoom.Monster = null;
                    Console.WriteLine($"You have {player.Health} HP left.\n");
                }
                if (monster.Health > 0 && player.Health > 0)
                {
                    monster.Attack(player);
                    Console.WriteLine($"You have {player.Health} HP left.\n");
                }
                if (player.Health <= 0)
                {
                    Console.WriteLine("You have been defeated! Game over.");
                    System.Environment.Exit(0); 
                }
            }
        }
    }
}