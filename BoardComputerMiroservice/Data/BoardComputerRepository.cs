using BoardComputerMiroservice.Models;

namespace BoardComputerMiroservice.Data
{
    public class BoardComputerRepository : IBoardComputerRepository
    {
        private readonly AppDdContext _context;
        
        public BoardComputerRepository(AppDdContext context)
        {
            _context = context;
        }

        public void CreateBoardComputer(BoardComputer BoardComputer)
        {
            if (BoardComputer == null)
            {
                throw new ArgumentNullException(nameof(BoardComputer));
            }

            _context.BoardComputers.Add(BoardComputer);
        }

        public IEnumerable<BoardComputer> GetAllBoardComputers()
        {
            return _context.BoardComputers.ToList();
        }

        public BoardComputer? GetBoardComputerById(int id)
        {
            return _context.BoardComputers.FirstOrDefault(BoardComputer => BoardComputer.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
