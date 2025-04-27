using DungeonExplorer;
using System;
using System.ComponentModel;
using System.Diagnostics;
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
                    if (currentRoom.LeftRoom != null)
                    {
                        currentRoom = currentRoom.LeftRoom;
                        Console.WriteLine("You have entered the new room\n");
                        Console.WriteLine(currentRoom.GetDescription());

                        if (currentRoom.Item != null)
                        {
                            Console.WriteLine("Would you like to pick it up? (yes/no)");
                            string response = Console.ReadLine().ToLower();
                            if (response == "yes")
                            {
                                string itemName = currentRoom.Item.Name;
                                inventory.PickUpItem(currentRoom.Item);
                                currentRoom.Item = null;
                                Console.WriteLine($"You picked up the {itemName}.");
                            }
                            else
                            {
                                Console.WriteLine("You left the item behind.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }
                    break;
                case "right":
                    if (currentRoom.RightRoom != null)
                    {
                        currentRoom = currentRoom.RightRoom;
                        Console.WriteLine("You have entered the new room\n");
                        Console.WriteLine(currentRoom.GetDescription());

                        if (currentRoom.Item != null)
                        {
                            Console.WriteLine("Would you like to pick it up? (yes/no)");
                            string response = Console.ReadLine().ToLower();
                            if (response == "yes")
                            {
                                string itemName = currentRoom.Item.Name;
                                inventory.PickUpItem(currentRoom.Item);
                                currentRoom.Item = null;
                                Console.WriteLine($"You picked up the {itemName}.");
                            }
                            else
                            {
                                Console.WriteLine("You left the item behind.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.");
                    }
                    break;
                case "stats":
                    PlayerStats();
                    break;
                case "equip":
                    EquipItem();
                    break;
                default:
                    Console.WriteLine("Invalid response, please check above!");
                    break;
            }
        }
        public void Start()
        {
            // Starts the game
            bool playing = true;

            while (playing)
            {
                Console.WriteLine("What would you like to do? (left/right/stats/exit)\n");
                string input = Console.ReadLine();
                input = input.ToLower();

                Controls(input);
                // End of the game
                if (input == "exit")
                {
                    Console.WriteLine("goodbye");
                    playing = false;
                }
            }
        }
        private void PlayerStats()
        {   // Adds the players stats 
            Console.WriteLine("\n" + "Name: " + player.Name);
            Console.WriteLine("Health: " + player.Health + " HP");
            Console.WriteLine("Attack: " + player.Attack);
            Console.WriteLine("Inventory: " + inventory.InventoryContents() + "\n");
            Console.WriteLine("Equipped weapon: ", player.EquippedWeapon);
        }
        private void EquipItem()
        {
            // Equips the item from the inventory
            var weapons = inventory.GetWeapons().ToList();
            if (weapons.Count() == 0)
            {
                Console.WriteLine("You have no weapons in your inventory to equip.");
                return;
            }
            Console.WriteLine("Which item would you like to equip? \n");
            foreach (var weapon in weapons)
            {
                Console.WriteLine($"- {weapon.Name} - Attack Power: {weapon.AttackPower}");
            }
            string weaponName = Console.ReadLine();
            var selectedWeapon = inventory.GetWeapons().FirstOrDefault(w => w.Name.Equals(weaponName, StringComparison.OrdinalIgnoreCase));
            if (selectedWeapon != null)
            {
                player.EquippedWeapon = selectedWeapon;
                Console.WriteLine($"You have equipped the {selectedWeapon.Name}.\n");
                player.Attack += selectedWeapon.AttackPower; 
            }
            else
            {
                Console.WriteLine("Item not found in inventory.");
            }
        }
        private void UseItem()
        {
            // Uses the item from the inventory
            var potions = inventory.GetPotions().ToList();
            if (potions.Count() == 0)
            {
                Console.WriteLine("You have no potions in your inventory to use.");
                return;
            }
            Console.WriteLine("Which potion would you like to use? \n");
            foreach (var potion in potions)
            {
                Console.WriteLine($"- {potion.Name} - Healing Amount: {potion.Health}");
            }
            string potionName = Console.ReadLine();
            var selectedPotion = inventory.GetPotions().FirstOrDefault(p => p.Name.Equals(potionName, StringComparison.OrdinalIgnoreCase));
            if (selectedPotion != null)
            {
                player.Health += selectedPotion.Health;
                inventory.RemoveItem(selectedPotion);
                Console.WriteLine($"You have used the {selectedPotion.Name}. Your health is now {player.Health} HP.\n");
            }
            else
            {
                Console.WriteLine("Item not found in inventory.");
            }
        }
    }
}

//// Displays description for the room and the initial player stats
//Console.WriteLine(currentRoom.GetDescription());
//PlayerStats();

//// Asks if they would like to pickup the health potion
//Console.WriteLine("Would you like to pickup the health potion at the door? yes or no\n");
//string response = Console.ReadLine();
//response.ToLower();

//// If the player picks up the potion, adds to inventory and displays the potion in inventory
//if (response == "yes")
//{
//    inventory.PickUpItem("Health potion (50HP)");

//    // Checks to see if the potion exists in the players inventory
//    bool result = testing.IsItemInInventory(inventory, "Health potion (50HP)");
//    Debug.Assert(result == true);

//    Console.WriteLine("\nYou picked up the health potion");
//    PlayerStats();
//}
//// If the players doesn't pickup the potion, it dissapears behind
//else if (response == "no")
//{
//    Console.WriteLine("\nYou left the health potion behind");
//}
//else
//{
//    Console.WriteLine("\nYou left the health potion behind");
//}

//// If they pick it up, they are then asked if they want to use it
//if (inventory.InventoryContents().Contains("Health potion (50HP)"))

//// This asks the question to if they would like to use the potion
//{
//    Console.WriteLine("You have picked up the health potion. Would you like to use it? yes or no\n");
//    string response2 = Console.ReadLine();
//    response2.ToLower();

//    // If they would like to use it, consumes the potion and adds 50 health to the player
//    switch (response2)
//    {
//        case "yes":
//            Console.WriteLine("\nYou have used the health potion");
//            player.Health = player.Health + 50;
//            inventory.RemoveItem("Health potion (50HP)");
//            PlayerStats();
//            break;
//        case "no":
//            Console.WriteLine("You left the item in your inventory");
//            break;
//        default:
//            Console.WriteLine("You left the item in your inventory");
//            break;
//    }
//