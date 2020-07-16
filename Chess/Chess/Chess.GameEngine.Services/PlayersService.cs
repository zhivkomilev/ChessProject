using AutoMapper;
using Chess.Core.DataAccess;
using Chess.Core.Domain.Interfaces;
using Chess.Core.Services;
using Chess.GameEngine.DataAccess.Entities;
using Chess.GameEngine.Models;

namespace Chess.GameEngine.Services
{
    public class PlayersService : BaseEntityService<Player, PlayerModel>, IPlayerService
    {
        public PlayersService(IUnitOfWork unitOfWork, 
            IDateTimeProvider dateTimeProvider,
            IMapper mapper)
            :base(unitOfWork, dateTimeProvider, mapper)
        {

        }
    }
}