using CommandsService.Models;

namespace CommandsService.SyncDataServices.Grpc
{
    public interface IBoardComputerDataClient
    {
        IEnumerable<BoardComputer> ReturnAllBoardComputers();
    }
}
