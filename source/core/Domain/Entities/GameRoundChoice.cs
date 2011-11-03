using Color.Core.Domain.Infrastructure;

namespace Color.Core.Domain.Entities
{
	public class GameRoundChoice : BaseEntity
	{
		private GameRound _gameRound;
		private Enums.Colors _choice;

		internal GameRoundChoice() {}

		public GameRoundChoice(GameRound gameRound)
		{
			_gameRound = gameRound;
		}

		public virtual GameRound GameRound
		{
			get { return _gameRound; }
		}

		public Enums.Colors Choice
		{
			get { return _choice; }
			set { _choice = value; }
		}
	}
}