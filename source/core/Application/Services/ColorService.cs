using System.Linq;
using Color.Core.Application.Infrastructure;
using Color.Core.Application.Repositories;
using Color.Core.Application.Requests;
using Color.Core.Application.Responses;
using Color.Core.Domain.Entities;

namespace Color.Core.Application.Services
{
	public class ColorService : IColorService
	{
		private readonly IPlayerRepository _playerRepository;
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;

		public ColorService(IPlayerRepository playerRepository, IUnitOfWorkFactory unitOfWorkFactory)
		{
			_playerRepository = playerRepository;
			_unitOfWorkFactory = unitOfWorkFactory;
		}

		public PlayViewResponse PlayView(PlayViewRequest request)
		{
			if(!request.PlayerId.HasValue)
				return new PlayViewResponse
				{
					CanPlayGame = false,
					RequestPlayerInfo = true
				};

			var player = _playerRepository.Get(request.PlayerId.Value);
			return new PlayViewResponse
			{
				PlayerId = player.Id,
				PlayerName = player.Name,
				CanPlayGame = true,
				RequestPlayerInfo = false
			};
		}

		public NewPlayerSetupResponse NewPlayerSetup(NewPlayerSetupRequest request)
		{
			var errors = Player.ValidateNewPlayer(request);
			if(errors.Any())
				return new NewPlayerSetupResponse
				{
					Errors = errors
				};

			var newPlayer = Player.CreateFrom(request);
			using(var uow = _unitOfWorkFactory.BeginTransaction())
			{
				_playerRepository.Save(newPlayer);
				uow.Commit();
			}

			return new NewPlayerSetupResponse
			{
				PlayerId = newPlayer.Id,
				Name = newPlayer.Name
			};
		}
	}
}