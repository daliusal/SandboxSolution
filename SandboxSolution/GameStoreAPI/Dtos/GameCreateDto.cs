using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Dtos
{
    public class GameCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int PublisherId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
