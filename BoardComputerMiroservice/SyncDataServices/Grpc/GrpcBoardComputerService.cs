using AutoMapper;
using BoardComputerMiroservice.Data;
using BoardComputerMiroservice.Protos;
using Grpc.Core;

namespace BoardComputerMiroservice.SyncDataServices.Grpc
{
    public class GrpcBoardComputerService : GrpcBoardComputer.GrpcBoardComputerBase
    {
        private readonly IBoardComputerRepository _repository;
        private readonly ILogger<GrpcBoardComputerService> _logger;
        private readonly IMapper _mapper;

        public GrpcBoardComputerService(IBoardComputerRepository repository, ILogger<GrpcBoardComputerService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override Task<BoardComputerResponse> GetAllBoardComputers(GetAllRequest request, ServerCallContext context)
        {
            var response = new BoardComputerResponse();
            var BoardComputers = _repository.GetAllBoardComputers();

            foreach (var BoardComputer in BoardComputers)
            {
                response.BoardComputer.Add(_mapper.Map<GrpcBoardComputerModel>(BoardComputer));
            }

            return Task.FromResult(response);
        }
    }
}
