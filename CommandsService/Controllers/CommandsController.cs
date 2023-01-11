using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/fabrics/{fabricId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CommandRepository> _logger;

        public CommandsController(ICommandRepository repository, IMapper mapper, ILogger<CommandRepository> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        #region API
        [HttpGet("all")]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommandsForBoardComputer(int fabricId)
        {
            _logger.LogInformation($"--> Hit GetCommandsForBoardComputer: {fabricId}");

            if (!_repository.BoardComputerExits(fabricId))
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDTO>(_repository.GetCommandsForBoardComputer(fabricId)));
        }

        [HttpGet("by-command-id/{commandId}", Name = "GetCommandForBoardComputer")]
        public ActionResult<CommandReadDTO> GetCommandForBoardComputer(int fabricId, int commandId)
        {
            _logger.LogInformation($"--> Hit GetCommandForBoardComputer: {fabricId} / {commandId}");

            if (!_repository.BoardComputerExits(fabricId))
            {
                return NotFound();
            }

            var command = _repository.GetCommand(fabricId, commandId);

            if (command == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommandForBoardComputer(int fabricId, CommandCreateDTO commandCreateDTO)
        {
            _logger.LogInformation($"--> Hit CreateCommandForBoardComputer: {fabricId}");

            if (!_repository.BoardComputerExits(fabricId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandCreateDTO);

            _repository.CreateCommand(fabricId, command);
            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(command);

            return CreatedAtRoute(nameof(GetCommandForBoardComputer),
                new { fabricId = fabricId, commandId = commandReadDTO.Id }, commandReadDTO);
        }
        #endregion
    }
}
