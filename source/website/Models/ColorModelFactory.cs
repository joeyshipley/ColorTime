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
	}
}