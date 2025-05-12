using System;
using System.Collections.Generic;

namespace MonolitoBackend.Core.Entities
{
    public class Genre
    {
        public Genre()
        {
            Books = new List<Book>();
        }
        
        public   int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        
        public required  ICollection<Book> Books { get; set; }
    }
}