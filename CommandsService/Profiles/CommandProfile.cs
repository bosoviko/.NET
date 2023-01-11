using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using CommandsService.Protos;

namespace CommandsService.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile() 
        {
            CreateMap<BoardComputer, BoardComputerReadDTO>();
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<Command, CommandReadDTO>();
            CreateMap<BoardComputerPublishedDTO, BoardComputer>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            CreateMap <GrpcBoardComputerModel, BoardComputer> ()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.BoardComputerId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Commands, opt => opt.Ignore());
        }
    }
}
