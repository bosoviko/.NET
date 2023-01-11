using BoardComputerMiroservice.Dtos;

namespace BoardComputerMiroservice.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendBoardComputerToCommand(BoardComputerReadDTO BoardComputer);
    }
}
