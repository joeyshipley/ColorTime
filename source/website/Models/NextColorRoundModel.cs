using System.Collections.Generic;

namespace Color.Website.Models
{
	public class NextColorRoundModel
	{
		public string GameRoundId { get; set; }
		public string Answer { get; set; } 
		public IEnumerable<string> Choices { get; set; } 
	}
}