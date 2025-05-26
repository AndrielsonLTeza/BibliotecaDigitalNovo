using System.ComponentModel.DataAnnotations;

namespace MonolitoBackend.Core.DTOs
{
    public class CreateGenreDTO
    {
         [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres")]
         public string Name { get; set; } 
       
       
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string Description { get; set; } 
    }
}    