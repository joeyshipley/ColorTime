using System;
using System.Collections.Generic;
using Color.Core.Domain;

namespace Color.Core.Application.Responses
{
	public class NextColorRoundResponse
	{
		public NextColorRoundResponse()
		{
			Choices = new List<Enums.Colors>();
		}

		public Guid GameRoundId { get; set; }
		public Enums.Colors Answer { get; set; } 
		public IEnumerable<Enums.Colors> Choices { get; set; }
	}
}