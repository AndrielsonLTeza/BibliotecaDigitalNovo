// MappingProfile.cs
using AutoMapper;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.DTOs;

namespace MonolitoBackend.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Book mappings
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : string.Empty));
            
            CreateMap<CreateBookDTO, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore());
            
            CreateMap<UpdateBookDTO, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore());

            // Genre mappings
            CreateMap<Genre, GenreDTO>();
            
            CreateMap<CreateGenreDTO, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.Ignore());
            
            CreateMap<UpdateGenreDTO, Genre>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.Ignore());
        }
    }
}