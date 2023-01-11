using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using System.Text.Json;

namespace CommandsService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor (IServiceScopeFactory scopeFactory, IMapper mapper, ILogger<EventProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _logger = logger;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.BoardComputerPublished:
                    {
                        AddBoardComputer(message);
                    }
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notifcationMessage)
        {
            _logger.LogInformation("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDTO>(notifcationMessage);

            switch (eventType.Event)
            {
                case "BoardComputer_Published":
                    {
                        _logger.LogInformation("--> Platform Published Event Detected");

                        return EventType.BoardComputerPublished;
                    }
                default:
                    {
                        _logger.LogInformation("--> Could not determine the event type");

                        return EventType.Undetermined;
                    }
            }
        }

        private void AddBoardComputer(string fabricPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ICommandRepository>();

                var fabricPublishedDto = JsonSerializer.Deserialize<BoardComputerPublishedDTO>(fabricPublishedMessage);

                try
                {
                    var plat = _mapper.Map<BoardComputer>(fabricPublishedDto);
                    if (!repository.ExternalBoardComputerExists(plat.ExternalID))
                    {
                        repository.CreateBoardComputer(plat);
                        repository.SaveChanges();

                        _logger.LogInformation("--> Platform added!");
                    }
                    else
                    {
                        _logger.LogWarning("--> Platform already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError($"--> Could not add Platform to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        BoardComputerPublished,
        Undetermined
    }
}
