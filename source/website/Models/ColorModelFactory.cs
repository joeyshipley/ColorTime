using System.Linq;
using Color.Core.Application.Responses;

namespace Color.Website.Models
{
	public class ColorModelFactory
	{
		// NOTE: switch to an automapper if this grows.

		public PlayViewModel CreatePlayViewModel(PlayViewResponse response)
		{
			return new PlayViewModel
			{
				PlayerId = response.PlayerId,
				PlayerName = response.PlayerName,
				Score = response.Score.ToString(),
				RequestPlayerInfo = response.RequestPlayerInfo,
				CanPlayGame = response.CanPlayGame
			};
		}

		public NewPlayerSetupModel CreateNewPlayerSetupModel(NewPlayerSetupResponse response)
		{
			return new NewPlayerSetupModel
			{
				PlayerId = response.PlayerId.ToString(),
				Name = response.Name
			};
		}

		public NextColorRoundModel CreateNextColorRoundModel(NextColorRoundResponse response)
		{
			return new NextColorRoundModel
			{
				GameRoundId = response.GameRoundId.ToString(),
				Answer = response.Answer.ToString(),
				Choices = response.Choices.Select(c => c.ToString())
			};
		}

		public ColorRoundChoiceModel CreateColorRoundChoiceModel(ColorRoundChoiceResponse response)
		{
			return new ColorRoundChoiceModel
			{
				AttemptIsSuccessful = response.AttemptIsSuccessful
			};
		}

		public DisplayScoreModel CreateDisplayScoreModel(DisplayScoreResponse response)
		{
			return new DisplayScoreModel
			{
				Score = response.Score
			};
		}
	}
}