using System;
using Color.Core.Domain;
using Color.Core.Domain.Entities;

namespace Color.Tests.Helpers.Builders
{
	public class GameRoundBuilder
	{
		private Guid? _id;
		private Player _player;
		private Enums.Colors? _answer;
		private int? _failedAttempts;
		
		public GameRoundBuilder(Player player)
		{
			_player = player;
		}

		public GameRoundBuilder SetId(Guid value)
		{
			_id = value;
			return this;
		}

		public GameRoundBuilder WithAnswer(Enums.Colors value)
		{
			_answer = value;
			return this;
		}

		public GameRoundBuilder WithFailedAttempts(int numberOfAttempts)
		{
			_failedAttempts = numberOfAttempts;
			return this;
		}

		public GameRound Build()
		{
			var gameRound = GameRound.CreateNewGameRoundFor(_player);

			if(_failedAttempts.HasValue)
				for(var i = 0; i < _failedAttempts.Value; i++)
					gameRound.PlayerHasFailedAnAttempt();

			if(_answer.HasValue)
				gameRound.Answer = _answer.Value;

			if(_id.HasValue)
				gameRound.SetId(_id.Value);

			return gameRound;
		}
	}
}