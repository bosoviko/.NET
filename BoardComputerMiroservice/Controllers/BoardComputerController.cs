using AutoMapper;
using BoardComputerMiroservice.AsyncDataServices;
using BoardComputerMiroservice.Data;
using BoardComputerMiroservice.Dtos;
using BoardComputerMiroservice.Models;
using BoardComputerMiroservice.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardComputerMiroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardComputerController : ControllerBase
    {
        private readonly IBoardComputerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly ILogger<BoardComputerController> _logger;
        private readonly IMessageBusClient _messageBusClient;

        public BoardComputerController(IBoardComputerRepository repository, IMapper mapper, ICommandDataClient commandDataClient, 
            IMessageBusClient messageBusClient, ILogger<BoardComputerController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
            _messageBusClient = messageBusClient;
            _logger = logger;
        }

        #region API
        [HttpGet("all")]
        public ActionResult<IEnumerable<BoardComputerReadDTO>> GetAllBoardComputers()
        {
            _logger.LogInformation("--> Getting Platforms....");

            return Ok(_mapper.Map<IEnumerable<BoardComputerReadDTO>>(_repository.GetAllBoardComputers()));
        }

        [HttpGet("get/{id}", Name = "GetBoardComputerById")]
        public ActionResult<BoardComputerReadDTO> GetBoardComputerById(int id) 
        {
            var BoardComputer = _repository.GetBoardComputerById(id);

            if (BoardComputer != null)
            {
                return Ok(_mapper.Map<BoardComputerReadDTO>(BoardComputer));
            }

            return NotFound();
        }

        [HttpPost("create")]
        public async Task<ActionResult<BoardComputerReadDTO>> CreateBoardComputer(BoardComputerCreateDTO BoardComputerCreateDTO)
        {
            var BoardComputerToCreate = _mapper.Map<BoardComputer>(BoardComputerCreateDTO);

            _repository.CreateBoardComputer(BoardComputerToCreate);
            _repository.SaveChanges();

            var BoardComputerReadDTO = _mapper.Map<BoardComputerReadDTO>(BoardComputerToCreate);

            try
            {
                await _commandDataClient.SendBoardComputerToCommand(BoardComputerReadDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Could not send synchronously: {ex.Message}");
            }

            try
            {
                var BoardComputerPublishedDTO = _mapper.Map<BoardComputerPublishedDTO>(BoardComputerReadDTO);
                BoardComputerPublishedDTO.Event = "BoardComputer_Published";
                _messageBusClient.PublishNewBoardComputer(BoardComputerPublishedDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetBoardComputerById), new { Id = BoardComputerReadDTO.Id }, BoardComputerReadDTO);
        }
        #endregion
    }
}
