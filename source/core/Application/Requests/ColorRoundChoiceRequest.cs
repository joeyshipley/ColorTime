﻿using System;
using Color.Core.Domain;

namespace Color.Core.Application.Requests
{
	public class ColorRoundChoiceRequest
	{
		public Guid GameRoundId { get; set; }
		public Enums.Colors ProvidedAnswer { get; set; }
	}
}