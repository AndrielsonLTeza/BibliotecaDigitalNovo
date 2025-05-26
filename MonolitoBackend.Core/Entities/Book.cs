using System;

namespace MonolitoBackend.Core.Entities
{
    public class Book
    {
        public   int Id { get; set; }
        public required string Title { get; set; }
        public required  string Author { get; set; }
        public required string ISBN { get; set; }
        public  int PublishedYear { get; set; }
        
        public   int GenreId { get; set; }
        public required  Genre Genre { get; set; }
    }
}