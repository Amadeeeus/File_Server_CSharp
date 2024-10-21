using AutoMapper;
using FileServer.Models.DTOs;
using FileServer.Models.Views;

namespace FileServer.Mappers;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<IFormFile, FileDTO>()
            .ForMember(a => a.FileName, opt => opt.MapFrom(a => a.FileName))
            .ForMember(a => a.ContentType, opt => opt.MapFrom(a => a.ContentType));
        CreateMap<FileView, FileDTO>().ReverseMap()
            .ForMember(a => a.FileName, opt => opt.MapFrom(a => a.FileName))
            .ForMember(a => a.ContentType, opt => opt.MapFrom(a => a.ContentType))
            .ForMember(a=>a.Id, opt => opt.MapFrom(a=>a.Id));
    }
}