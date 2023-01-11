using BoardComputerMiroservice.Dtos;

namespace BoardComputerMiroservice.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewBoardComputer(BoardComputerPublishedDTO BoardComputerPublishedDTO);
    }
}
