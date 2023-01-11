using CommandsService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepository
    {
        bool SaveChanges();

        // BoardComputers
        IEnumerable<BoardComputer> GetAllBoardComputers();
        void CreateBoardComputer(BoardComputer fabric);
        bool BoardComputerExits(int fabricId);
        bool ExternalBoardComputerExists(int externaBoardComputerId);

        // Commands
        IEnumerable<Command> GetCommandsForBoardComputer(int fabricId);
        Command? GetCommand(int fabricId, int commandId);
        void CreateCommand(int fabricId, Command command);
    }
}
