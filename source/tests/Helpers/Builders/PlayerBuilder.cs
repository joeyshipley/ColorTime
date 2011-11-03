using System;
using Color.Core.Domain.Entities;

namespace Color.Tests.Helpers.Builders
{
	public class PlayerBuilder
	{
		private Guid? _id;
		private string _name;
		
		public PlayerBuilder(string name)
		{
			_name = name;
		}

		public PlayerBuilder SetId(Guid value)
		{
			_id = value;
			return this;
		}

		public PlayerBuilder AsInvalid()
		{
			_name = string.Empty;
			return this;
		}

		public Player Build()
		{
			var player = Player.CreateFrom(new PlayerCreateCommand
			{
				Name = _name
			});

			if(_id.HasValue)
				player.SetId(_id.Value);

			return player;
		}
	}
}