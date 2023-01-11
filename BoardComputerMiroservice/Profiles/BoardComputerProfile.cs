using AutoMapper;
using BoardComputerMiroservice.Dtos;
using BoardComputerMiroservice.Models;
using BoardComputerMiroservice.Protos;

namespace BoardComputerMiroservice.Profiles
{
    public class BoardComputerProfile : Profile
    {
        public BoardComputerProfile() 
        {
            CreateMap<BoardComputer, BoardComputerReadDTO>();
            CreateMap<BoardComputerCreateDTO, BoardComputer>();
            CreateMap<BoardComputerReadDTO, BoardComputerPublishedDTO>();
            CreateMap<BoardComputer, GrpcBoardComputerModel>()
                .ForMember(dest => dest.BoardComputerId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.Memory, opt => opt.MapFrom(src => src.Memory));
        }
    }
}
