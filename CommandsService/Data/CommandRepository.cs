using CommandsService.Models;

namespace CommandsService.Data
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CommandRepository> _logger;

        public CommandRepository(AppDbContext context, ILogger<CommandRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void CreateCommand(int fabricId, Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.BoardComputerId = fabricId;

            _context.Commands.Add(command);
        }

        public void CreateBoardComputer(BoardComputer fabric)
        {
            if (fabric== null)
            {
                throw new ArgumentNullException(nameof(fabric));
            }

            _context.BoardComputers.Add(fabric);
        }

        public bool ExternalBoardComputerExists(int externaBoardComputerId)
        {
            return _context.BoardComputers.Any(fabric => fabric.ExternalID == externaBoardComputerId);
        }

        public bool BoardComputerExits(int fabricId)
        {
            return _context.BoardComputers.Any(fabric => fabric.Id == fabricId);
        }

        public IEnumerable<BoardComputer> GetAllBoardComputers()
        {
            return _context.BoardComputers.ToList();
        }

        public Command? GetCommand(int fabricId, int commandId)
        {
            return _context.Commands
                .Where(command => command.BoardComputerId == fabricId && command.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForBoardComputer(int fabricId)
        {
            return _context.Commands
                .Where(command => command.BoardComputerId == fabricId)
                .OrderBy(command => command.BoardComputers.Name);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
