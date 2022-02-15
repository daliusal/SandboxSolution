using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SandboxSolution.Models
{
    public class StoreContext : DbContext
    {
        //TODO: Add DbSets
        public DbSet<Game>? Games { get; set; }
        public DbSet<Publisher>? Publisher { get; set; }
        
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
    }

    //TODO: Add model classes
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }       
        [MaxLength(100)]
        [Required]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double Price { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
        public bool IsOutOfStock { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
    }

    public class Publisher
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Game>? Games { get; set; }
    }
}
