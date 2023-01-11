using AutoMapper;
using CommandsService.Models;
using CommandsService.Protos;
using Grpc.Net.Client;

namespace CommandsService.SyncDataServices.Grpc
{
    public class BoardComputerDataClient : IBoardComputerDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<BoardComputerDataClient> _logger;

        public BoardComputerDataClient(IConfiguration configuration, IMapper mapper, ILogger<BoardComputerDataClient> logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<BoardComputer>? ReturnAllBoardComputers()
        {
            _logger.LogInformation($"--> Calling GRPC Service {_configuration["GrpcPlatform"]}");

            var channel = GrpcChannel.ForAddress(_configuration["GrpcPlatform"]);
            var client = new GrpcBoardComputer.GrpcBoardComputerClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllBoardComputers(request);
                return _mapper.Map<IEnumerable<BoardComputer>>(reply.BoardComputer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Couldnot call GRPC Server {ex.Message}");
                return null;
            }
        }
    }
}
