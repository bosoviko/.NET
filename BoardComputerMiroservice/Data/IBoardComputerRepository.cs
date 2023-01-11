using BoardComputerMiroservice.Models;

namespace BoardComputerMiroservice.Data
{
    public interface IBoardComputerRepository
    {
        bool SaveChanges();

        IEnumerable<BoardComputer> GetAllBoardComputers();
        BoardComputer? GetBoardComputerById(int id);
        void CreateBoardComputer(BoardComputer BoardComputer);
    }
}
