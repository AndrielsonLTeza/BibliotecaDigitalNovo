using AutoMapper;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.DTOs;

namespace MonolitoBackend.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configure seus mapeamentos aqui
            CreateMap<Book, BookDTO>();
            CreateMap<CreateBookDTO, Book>();
            // Adicione outros mapeamentos necessários
        }
    }
}
