using System.ComponentModel.DataAnnotations;


namespace MonolitoBackend.Core.DTOs
{
    public class UpdateGenreDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
        public required string Name { get; set; }
        
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public required string Description { get; set; }
    }
}    