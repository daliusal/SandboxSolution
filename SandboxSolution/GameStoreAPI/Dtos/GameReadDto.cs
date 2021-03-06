namespace GameStoreAPI.Dtos
{
    public class GameReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int PublisherId { get; set; }
        public bool? IsOutOfStock { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
