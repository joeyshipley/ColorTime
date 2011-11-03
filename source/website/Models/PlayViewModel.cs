using System;

namespace Color.Website.Models
{
	public class PlayViewModel
	{
		public Guid? PlayerId { get; set; } 
		public string PlayerName { get; set; }
		public bool RequestPlayerInfo { get; set; }
		public bool CanPlayGame { get; set; }
	}
}