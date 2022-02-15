namespace GameStoreAPI.Models
{
    public class GamePOST
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int PublisherId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
