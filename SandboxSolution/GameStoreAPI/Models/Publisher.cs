using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameStoreAPI.Models
{
    public partial class Publisher
    {
        public Publisher()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
    }
}
