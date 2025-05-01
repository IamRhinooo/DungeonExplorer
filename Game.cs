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
        // Contains the player, room, testing, inventory and the game map 
        private Player player;
        private Room currentRoom;
        private Testing testing;
        private Inventory inventory = new Inventory();
        private GameMap gameMap;
        public Game(GameMap map)
        {
            // Initialisation of the game 
            // The game map is created and the player is created
            currentRoom = map.StartRoom;
            player = new Player("You", 50, 10);
            testing = new Testing();
            gameMap = map;
        }

        // Contains the controlls which the player can enter throughout the game
        // Also contains the left and right navigation through the rooms
        public void Controls(string direction)
        {
            switch (direction.ToLower())
            {
                // Allows the player to go left
                case "left":
                    // Checks if there is a monster in the room
                    // If there is, the player cannot leave the room
                    if (currentRoom.Monster != null)
                    {
                        Console.WriteLine("\nYou cannot leave the room while a monster is present. You must defeat it first.");
                        return;
                    }
                    if (currentRoom.LeftRoom != null)
                    {
                        currentRoom = currentRoom.LeftRoom;
                        Console.WriteLine("\nYou have entered the new room\n");
                        Console.WriteLine(currentRoom.GetDescription());

                        // Checks if there is items in the room
                        // If there is, the player can pick them up
                        while (currentRoom.Items.Count > 0)
                        {
                            Console.WriteLine("Would you like to pick up an item? (yes/no)\n");
                            string response = Console.ReadLine().ToLower();
                            if (response == "yes")
                            {
                                Console.WriteLine("\nWhich item would you like to pick up? (Enter the name)\n");
                                string itemName = Console.ReadLine();
                                // Uses a LINQ query to find the item in the room
                                var selectedItem = currentRoom.Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

                                // If the item is found, it is added to the inventory and removed from the room
                                if (selectedItem != null)
                                {
                                    inventory.PickUpItem(selectedItem);
                                    currentRoom.Items.Remove(selectedItem);
                                    Console.WriteLine($"\nYou picked up the {selectedItem.Name}.\n");
                                    
                                    // Debugging line to check if the item was added to inventory
                                    bool result = testing.IsItemInInventory(inventory, selectedItem.Name);
                                    Debug.Assert(result == true);
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
                            // Error handling for any input that isn't yes or no
                            if (response != "yes" && response != "no")
                            {
                                Console.WriteLine("\nInvalid response, please check above!\n");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYou can't go that way.\n");
                    }
                    break;
                // Allows the player to go left
                case "right":
                    //Checks if there is a monster in the room
                    // If there is, the player cannot leave the room
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

                        // Checks if there is items in the room
                        // If there is, the player can pick them up
                        while (currentRoom.Items.Count > 0)
                        {
                            Console.WriteLine("Would you like to pick up an item? (yes/no)\n");
                            string response = Console.ReadLine().ToLower();
                            if (response == "yes")
                            {
                                Console.WriteLine("\nWhich item would you like to pick up? (Enter the name)\n");
                                string itemName = Console.ReadLine();
                                // Uses a LINQ query to find the item in the room
                                var selectedItem = currentRoom.Items.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

                                // If the item is found, it is added to the inventory and removed from the room
                                if (selectedItem != null)
                                {
                                    inventory.PickUpItem(selectedItem);
                                    currentRoom.Items.Remove(selectedItem);
                                    Console.WriteLine($"\nYou picked up the {selectedItem.Name}.\n");

                                    // Debugging line to check if the item was added to inventory
                                    bool result = testing.IsItemInInventory(inventory, selectedItem.Name);
                                    Debug.Assert(result == true);
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
                            // Error handling for any input that isn't yes or no
                            if (response != "yes" && response != "no")
                            {
                                Console.WriteLine("\nInvalid response, please check above!\n");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nYou can't go that way.\n");
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
                    // Checks if there is a monster in the room
                    // If there is, the player can attack it
                    if (currentRoom.Monster != null)
                    {
                        Combat(currentRoom.Monster);
                    }
                    // Error handling for no enemy in the room
                    else
                    {
                        Console.WriteLine("\nThere is no monster to attack in this room.\n");
                    }
                    break;
                case "exit":
                    break;
                // Error handling for any input that isn't on the controlls list
                default:
                    Console.WriteLine("\nInvalid response, please check above!");
                    break;
            }
        }
        public void Start()
        {
            // Starts the game
            bool playing = true;
            Console.WriteLine("You wake up in a dark mysterious room. \nVarious sounds can be heard from up ahead with lots of potential danger. \nWhat will you do here? \nGoodluck.\n");

            while (playing)
            // Has the necessary information always needed for the player below
            // They can always see the description of the room and the controlls within the game
            {
                Console.WriteLine(currentRoom.GetDescription());
                Console.WriteLine("What would you like to do? (left/right/attack/stats/use/equip/exit)\n");
                string input = Console.ReadLine();
                input = input.ToLower();

                Controls(input);
                // Ends the game if the player types exit
                if (input == "exit")
                {
                    Console.WriteLine("\nThank you for playing!\n");
                    playing = false;
                }
            }
        }
        private void PlayerStats()
        // Displays the players stats
        // This contains all of the necessary information about the player
        {
            Console.WriteLine("\n" + "Name: " + player.Name);
            Console.WriteLine("Health: " + player.Health + " HP");
            Console.WriteLine("Attack: " + player.Attack);
            Console.WriteLine("Inventory: " + inventory.InventoryContents());
            Console.WriteLine("Equipped weapon: " + (player.EquippedWeapon != null ? player.EquippedWeapon.Name : "None") + "\n");
        }

        // Allows the player to equip a weapon from their inventory
        private void EquipItem()
        {
            // Displays the weapons in their inventory
            var weapons = inventory.GetWeapons().ToList();
            // Checks if there are any weapons in the inventory
            // If there are none, the player cannot equip anything
            if (weapons.Count() == 0)
            {
                Console.WriteLine("\nYou have no weapons in your inventory to equip.");
                return;
            }
            Console.WriteLine($"\nWhich weapon would you like to equip? Use \"Cancel\" to stop\n");
            // Displays the weapons in the inventory with more information
            foreach (var weapon in weapons)
            {
                Console.WriteLine($"- {weapon.Name} - Attack Power: {weapon.AttackPower}\n");
            }
            string weaponName = Console.ReadLine();
            // Uses a LINQ query to find the weapon in the inventory
            var selectedWeapon = inventory.GetWeapons().FirstOrDefault(w => w.Name.Equals(weaponName, StringComparison.OrdinalIgnoreCase));

            // If the weapon is found, it is equipped but checks if there is an item equipped already
            if (selectedWeapon != null)
            {
                // If there is, it unequips the current weapon
                if (player.EquippedWeapon != null)
                {
                    Console.WriteLine($"\nYou have unequipped the {player.EquippedWeapon.Name}.");
                    player.Attack -= player.EquippedWeapon.AttackPower; 
                    inventory.PickUpItem(player.EquippedWeapon);
                }

                // Equips the new weapon and adjusts the players attack power
                Console.WriteLine($"\nYou have equipped the {selectedWeapon.Name}.\n");
                inventory.RemoveItem(selectedWeapon);
                player.EquippedWeapon = selectedWeapon;
                player.Attack += selectedWeapon.AttackPower; 
            }
            if (weaponName == "cancel")
            {
                Console.WriteLine("\nYou stuck with your current weapon.\n");
            }
            // Error handling for item not found
            else
            {
                Console.WriteLine("\nItem not found in inventory.");
            }
        }

        // Allows the player to use a potion from their inventory
        private void UseItem()
        {
            // Uses the item from the inventory
            var potions = inventory.GetPotions().ToList();
            // Checks if there are any potions in the inventory
            // If there are none, the player cannot use any
            if (potions.Count() == 0)
            {
                Console.WriteLine("\nYou have no potions in your inventory to use.\n");
                return;
            }
            // Displays the potions in the inventory with more information
            Console.WriteLine("\nYour inventory contains the following potions: \n");
            foreach (var potion in potions)
            {
                Console.WriteLine($"- {potion.Name} - Healing Amount: {potion.Health}\n");
            }
            Console.WriteLine("Which potion would you like to consume? Use \"Cancel\" to stop\n");
            string potionName = Console.ReadLine();
            // Uses a LINQ query to find the potion in the inventory
            var selectedPotion = inventory.GetPotions().FirstOrDefault(p => p.Name.Equals(potionName, StringComparison.OrdinalIgnoreCase));
            if (selectedPotion != null)
            // If the potion is found, it is used and adds the health to the player
            {
                player.Health += selectedPotion.Health;
                inventory.RemoveItem(selectedPotion);
                Console.WriteLine($"You have used the {selectedPotion.Name}. Your health is now {player.Health} HP.\n");

                // Checks if the player is at max health and if they are, it sets them to 100
                if (player.Health > 100)
                {
                    player.Health = 100;
                    Console.WriteLine("You have reached max health.");
                }
            }
            // Error handling for item not found
            else
            {
                Console.WriteLine("\nItem not found in inventory.");
            }
        }

        // Allows the player to attack a monster and heal 
        public void Combat(Monsters monster)
        {
            Console.WriteLine($"\nA {monster.Name} has been found. It has {monster.Health} HP.");

            // Checks if the players and the monsters health is above 0
            while (player.Health > 0 && monster.Health > 0)
            {
                Console.WriteLine("What would you like to do? (1/2)");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use Potion\n");

                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    monster.Health -= player.Attack;
                    Console.WriteLine($"\nYou attack the {monster.Name} for {player.Attack} damage.");
                    // Checks if the monster is dead after the players attack
                    if (monster.Health > 0)
                    {
                        Console.WriteLine($"The {monster.Name} has {monster.Health} HP left.\n");
                    }
                }
                // Allows the player to use a potion
                else if (choice == "2")
                {
                    // Error handling for no potions in the inventory
                    if (inventory.GetPotions().Count() == 0)
                    {
                        Console.WriteLine("\nYou have no potions to use.\n");
                        continue;
                    }
                    // Links to the UseItem method
                    if (inventory.GetPotions().Count() > 0)
                    {
                        UseItem();
                    }
                }
                // Used for testing the players death's during creation

                //else if (choice == "3")
                //{
                //    Console.WriteLine("You have died.");
                //}

                // Error handling for any input that isn't 1 or 2
                else
                {
                    Console.WriteLine("Invalid choice. Please choose again.");
                    continue;
                }
                // Checks if the monster is dead after the players attack
                if (monster.Health <= 0)
                {
                    Console.WriteLine($"\nYou have defeated the {monster.Name}!");

                    // Checks if the player is in the boss room and ends the game if they are
                    if (currentRoom == gameMap.BossRoom)
                    {
                        Console.WriteLine("\nCongratulations, you are free from the Chicken Jockey's lair!");
                        // Exits the game after winning
                        System.Environment.Exit(0);
                    }

                    // Removes the monster and displays the players health
                    currentRoom.Monster = null;
                    Console.WriteLine($"You have {player.Health} HP left.\n");
                }
                // Allows the monster to attack the player
                if (monster.Health > 0 && player.Health > 0)
                {
                    monster.Attack(player);
                    Console.WriteLine($"You have {player.Health} HP left.\n");
                }
                // Checks if the player is dead after the monster attacks
                if (player.Health <= 0)
                {
                    Console.WriteLine("You have been defeated! Game over.");
                    // If they are, ends the game
                    System.Environment.Exit(0); 
                }
            }
        }
    }
}
