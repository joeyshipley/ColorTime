using System.Collections.Generic;
using System.Linq;
using Color.Core.Domain.Infrastructure;

namespace Color.Core.Domain.Entities
{
	public class Player : BaseEntity
	{
		private string _name;
		private IList<GameRound> _gameRounds;

		public virtual string Name
		{
			get { return _name; }
			private set { _name = value; }
		}

		public virtual IList<GameRound> GameRounds
		{
			get { return _gameRounds ?? (_gameRounds = new List<GameRound>()); }
			internal set { _gameRounds = value; }
		}

		public int CalculateScore()
		{
			var score = GameRounds.Sum(g => g.Score != null ? g.Score.Value : 0);
			return score;
		}

		public static IEnumerable<ValidationError> ValidateNewPlayer(PlayerCreateCommand command)
		{
			var errors = new List<ValidationError>();
			if(string.IsNullOrEmpty(command.Name))
				errors.Add(new ValidationError { Type = "Name Required", Message = "Name is required to continue." });

			return errors;
		}

		public static Player CreateFrom(PlayerCreateCommand command)
		{
			// NOTE: if the player entity creation become more complicted, break this into a entity creation factory.
			return new Player
			{
				Name = command.Name
			};
		}
	}

	public class PlayerCreateCommand
	{
		public string Name { get; set; }
	}
}