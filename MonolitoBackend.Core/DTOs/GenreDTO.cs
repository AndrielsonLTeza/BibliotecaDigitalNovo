using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MonolitoBackend.Core.DTOs
{
    public class GenreDTO
    {
        public  int Id { get; set; }
       [Required]
       public string Name { get; set; } = string.Empty;

       [Required]
       public string Description { get; set; } = string.Empty;
        // Inicializando Books com uma lista vazia por padrão
        public List<BookDTO> Books { get; set; } = new List<BookDTO>();
    }
}
