using System;
using System.Collections.Generic;
using Color.Core.Domain;

namespace Color.Core.Application.Responses
{
	public class NewPlayerSetupResponse
	{
		public NewPlayerSetupResponse()
		{
			Errors = new List<ValidationError>();
		}

		public Guid PlayerId { get; set; }
		public string Name { get; set; } 

		public IEnumerable<ValidationError> Errors { get; set; }
	}
}