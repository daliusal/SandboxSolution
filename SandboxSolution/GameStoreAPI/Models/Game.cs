using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameStoreAPI.Models
{
    public partial class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int PublisherId { get; set; }
        public DateTime CreationTime { get; set; }
        public bool? IsOutOfStock { get; set; }
        public bool IsDeleted { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public virtual Publisher Publisher { get; set; } = null!;
    }
}
