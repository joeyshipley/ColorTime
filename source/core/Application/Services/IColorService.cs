using Color.Core.Application.Requests;
using Color.Core.Application.Responses;

namespace Color.Core.Application.Services
{
	public interface IColorService
	{
		 PlayViewResponse PlayView(PlayViewRequest request);
		 NewPlayerSetupResponse NewPlayerSetup(NewPlayerSetupRequest request);
		 NextColorRoundResponse NextColorRound(NextColorRoundRequest request);
		 ColorRoundChoiceResponse ColorRoundChoice(ColorRoundChoiceRequest request);
	}
}