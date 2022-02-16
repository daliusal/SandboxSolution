namespace GameStoreAPI.Dtos
{
    public class GameReadDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int PublisherId { get; set; }
        public bool? IsOutOfStock { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
