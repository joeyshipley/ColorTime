using System;

namespace Color.Core.Application.Responses
{
	public class PlayViewResponse
	{
		public Guid? PlayerId { get; set; } 
		public string PlayerName { get; set; }
		public bool RequestPlayerInfo { get; set; }
		public bool CanPlayGame { get; set; }
	}
}