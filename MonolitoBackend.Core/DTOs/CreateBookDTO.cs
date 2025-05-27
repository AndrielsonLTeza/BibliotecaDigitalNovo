using System.ComponentModel.DataAnnotations;

namespace MonolitoBackend.Core.DTOs
{
    public class CreateBookDTO
    {
        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(200, ErrorMessage = "O título deve ter no máximo 200 caracteres")]
        public required string Title { get; set; }
        
        [Required(ErrorMessage = "O autor é obrigatório")]
        [StringLength(100, ErrorMessage = "O autor deve ter no máximo 100 caracteres")]
        public required string Author { get; set; }
        
        [Required(ErrorMessage = "O ISBN é obrigatório")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "O ISBN deve ter entre 10 e 13 caracteres")]
        public required string ISBN { get; set; }
        
        [Required(ErrorMessage = "O ano de publicação é obrigatório")]
        [Range(1000, 2100, ErrorMessage = "O ano de publicação deve estar entre 1000 e 2100")]
        public required int PublishedYear { get; set; }
        
        [Required(ErrorMessage = "O gênero é obrigatório")]
        public required int GenreId { get; set; }
    }
}    