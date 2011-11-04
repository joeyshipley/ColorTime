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
		private readonly IGameRoundRepository _gameRoundRepository;
		private readonly IUnitOfWorkFactory _unitOfWorkFactory;
		private readonly IGameRoundScorerSpecification _gameRoundScorerSpecification;

		public ColorService(IPlayerRepository playerRepository, IGameRoundRepository gameRoundRepository, IUnitOfWorkFactory unitOfWorkFactory, IGameRoundScorerSpecification gameRoundScorerSpecification)
		{
			_playerRepository = playerRepository;
			_gameRoundRepository = gameRoundRepository;
			_unitOfWorkFactory = unitOfWorkFactory;
			_gameRoundScorerSpecification = gameRoundScorerSpecification;
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
				Score = player.CalculateScore(),
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

		public NextColorRoundResponse NextColorRound(NextColorRoundRequest request)
		{
			var player = _playerRepository.Get(request.PlayerId);
			var gameRound = GameRound.CreateNewGameRoundFor(player);
			using(var uow = _unitOfWorkFactory.BeginTransaction())
			{
				player.GameRounds.Add(gameRound);
				_playerRepository.Save(player);
				uow.Commit();
			}
			
			return new NextColorRoundResponse
			{
				GameRoundId = gameRound.Id,
				Answer = gameRound.Answer,
				Choices = gameRound.GetChoices()
			};
		}

		public ColorRoundChoiceResponse ColorRoundChoice(ColorRoundChoiceRequest request)
		{
			var gameRound = _gameRoundRepository.Get(request.GameRoundId);
			var success = request.ProvidedAnswer == gameRound.Answer;
			if(!success)
				gameRound.PlayerHasFailedAnAttempt();
			else
			{
				var score = _gameRoundScorerSpecification.DetermineScoreFromGameRound(gameRound);
				gameRound.SetScore(score);
			}

			using(var uow = _unitOfWorkFactory.BeginTransaction())
			{
				_gameRoundRepository.Save(gameRound);
				uow.Commit();
			}

			return new ColorRoundChoiceResponse
			{
				AttemptIsSuccessful = success
			};
		}

		public DisplayScoreResponse DisplayScore(DisplayScoreRequest request)
		{
			var player = _playerRepository.Get(request.PlayerId);
			return new DisplayScoreResponse
			{
				Score = player.CalculateScore()
			};
		}
	}
}