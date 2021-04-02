using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using web.api.App.Common;
using web.api.App.Teams;

namespace web.api.App.Events.Commands
{
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand, EntityCreatedResult>
    {
        private EventService _eventService;
        private TeamService _teamService;

        public AddEventCommandHandler(EventService eventService, TeamService teamService)
        {
            _eventService = eventService;
            _teamService = teamService;
        }

        public async Task<EntityCreatedResult> Handle(AddEventCommand command, CancellationToken cancellationToken)
        {
            var evt = command.Adapt<Event>();
            if (evt.TeamId == 0)
            {
                var team = await _teamService.GetActiveTeamForUser(command.Username);
                evt.TeamId = team.Id;
            }

            var result = await _eventService.Create(evt);
            return new EntityCreatedResult(result.Id);
        }
    }
}