namespace DungeonExplorer
{
    public class Room
    {
        private string description;

        public Room LeftRoom { get; set; }
        public Room RightRoom { get; set; }
        public Items Item { get; set; }

        public Room(string description, Items item = null)
        {
            this.description = description;
            Item = item; 
        }
        public string GetDescription()
        {
            return description + (Item != null ? $"\nThere lies a {Item.Name} on the floor." : "");
        }
    }
}