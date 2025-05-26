using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MonolitoBackend.Core.Entities
{
    public class Genre
    {        
        public   int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
         [JsonIgnore]  // Ignora esta propriedade na serialização JSON
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}