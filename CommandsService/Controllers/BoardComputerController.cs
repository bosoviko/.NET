using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardComputerController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BoardComputerController> _logger;

        public BoardComputerController(ICommandRepository repository, IMapper mapper, ILogger<BoardComputerController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        #region API
        [HttpGet("all")]
        public ActionResult<IEnumerable<BoardComputerReadDTO>> GetBoardComputers()
        {
            _logger.LogInformation("--> Getting Platforms from CommandsService");

            return Ok(_mapper.Map<IEnumerable<BoardComputerReadDTO>>(_repository.GetAllBoardComputers()));
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            _logger.LogInformation("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controler");
        }
        #endregion
    }
}
