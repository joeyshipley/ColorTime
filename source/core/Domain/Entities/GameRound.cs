using System;
using System.Collections.Generic;
using System.Linq;
using Color.Core.Domain.Infrastructure;

namespace Color.Core.Domain.Entities
{
	public class GameRound : BaseEntity
	{
		private readonly Player _player;
		private readonly DateTime _date;
		private readonly IList<GameRoundChoice> _choices;
		private Enums.Colors _answer;
		private int _failedAttempts;
		private int? _score;

		internal GameRound() {}

		public GameRound(Player player, IEnumerable<Enums.Colors> choices, Enums.Colors answer)
		{
			_player = player;
			_date = DateTime.Now;
			_choices = convertFrom(choices);
			_answer = answer;
		}

		public virtual Player Player
		{
			get { return _player; }
		}

		public virtual DateTime Date
		{
			get { return _date; }
		}

		public virtual IEnumerable<GameRoundChoice> Choices 
		{
		    get { return _choices; }
		}

		public virtual Enums.Colors Answer
		{
			get { return _answer; }
			internal set { _answer = value; }
		}

		public virtual int FailedAttempts
		{
			get { return _failedAttempts; }
		}

		public virtual int? Score
		{
			get { return _score; }
		}

		public IEnumerable<Enums.Colors> GetChoices()
		{
			return convertFrom(_choices);
		}

		public void PlayerHasFailedAnAttempt()
		{
			_failedAttempts++;
		}

		public void SetScore(int score)
		{
			_score = score;
		}

		// NOTE: If there ends up being multiple situations a game can be created for/in then move this into a domain entity factory.
		public static GameRound CreateNewGameRoundFor(Player player)
		{
			var colors = Enum.GetValues(typeof(Enums.Colors)).Cast<Enums.Colors>().ToList();
			var choices = new List<Enums.Colors>();
			var answer = getRandomColor(colors);
			choices.Add(answer);

			for(var i = choices.Count(); i < 3; i++)
				choices.Add(getRandomColor(colors));

			return new GameRound(player, choices, answer);
		}

		private static Enums.Colors getRandomColor(IList<Enums.Colors> colors)
		{
			var random = new Random();
			var choices = colors.ToArray();
			var selectedColor = (Enums.Colors) colors.ToArray().GetValue(random.Next(choices.Length));
			colors.Remove(selectedColor);
			return selectedColor;
		}

		private IList<GameRoundChoice> convertFrom(IEnumerable<Enums.Colors> choices)
		{
			return choices.Select(color => new GameRoundChoice(this) 
			{
				Choice = color 
			}).ToList();
		}

		private IEnumerable<Enums.Colors> convertFrom(IEnumerable<GameRoundChoice> choices)
		{
			return choices.Select(color => color.Choice).ToList();
		}
	}
}