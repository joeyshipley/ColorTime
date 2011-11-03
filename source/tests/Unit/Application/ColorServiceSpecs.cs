using System;
using System.Linq;
using Color.Core.Application.Repositories;
using Color.Core.Application.Requests;
using Color.Core.Application.Responses;
using Color.Core.Application.Services;
using Color.Core.Domain;
using Color.Core.Domain.Entities;
using Color.Tests.Helpers.Builders;
using Color.Tests.Infrastructure;
using Machine.Specifications;
using Rhino.Mocks;

namespace Color.Tests.Unit.Application
{
	[Subject("Application, Color Service, Play Game")]
	public class when_someone_tries_to_play_the_game_and_we_dont_know_who_the_player_is : BaseSpec<ColorService>
	{
		private static PlayViewResponse _response;

		Establish context = () => {};

		Because of = () => 
		{
			var request = new PlayViewRequest
			{
				PlayerId = null
			};
			_response = Mocks.ClassUnderTest.PlayView(request);
		};

		It should_ask_who_the_user_is = () =>  _response.RequestPlayerInfo.ShouldBeTrue();

		It should_not_load_the_current_game_round = () =>  _response.CanPlayGame.ShouldBeFalse();
	}

	[Subject("Application, Color Service, Play Game")]
	public class when_someone_tries_to_play_the_game_and_we_know_who_the_player_is : BaseSpec<ColorService>
	{
		private static PlayViewResponse _response;

		Establish context = () => 
		{
			var player = new PlayerBuilder("Mocked Player")
				.SetId(Guid.NewGuid())
				.Build();
			Mocks.Get<IPlayerRepository>()
				.Stub(r => r.Get(Guid.Empty))
				.IgnoreArguments()
				.Return(player);
		};

		Because of = () => 
		{
			var request = new PlayViewRequest
			{
				PlayerId = Guid.NewGuid()
			};
			_response = Mocks.ClassUnderTest.PlayView(request);
		};

		It should_not_ask_who_the_user_is = () =>  _response.RequestPlayerInfo.ShouldBeFalse();

		It should_allow_loading_the_current_game_round = () => _response.CanPlayGame.ShouldBeTrue();

		It should_return_the_players_id = () => _response.PlayerId.HasValue.ShouldBeTrue();
	}

	[Subject("Application, Color Service, New Player Setup")]
	public class when_a_new_player_is_telling_us_there_information_and_no_name_was_provided : BaseSpec<ColorService>
	{
		private static NewPlayerSetupResponse _response;

		Establish context = () => {};

		Because of = () => 
		{
			var request = new NewPlayerSetupRequest
			{
				Name = string.Empty
			};
			_response = Mocks.ClassUnderTest.NewPlayerSetup(request);
		};

		It should_let_the_player_know_there_was_problems = () =>  _response.Errors.Any(e => e.Type.Equals("Name Required", StringComparison.OrdinalIgnoreCase)).ShouldBeTrue();
	}

	[Subject("Application, Color Service, New Player Setup")]
	public class when_a_new_player_is_telling_us_there_information_and_all_info_was_provided : BaseSpec<ColorService>
	{
		private static Player _player;

		Establish context = () => 
		{
			var repository = Mocks.Get<IPlayerRepository>();
			repository
				.Stub(r => r.Save(null))
				.Callback((Player entity) =>
				{
					_player = entity;
					return true;
				});
		};

		Because of = () => 
		{
			var request = new NewPlayerSetupRequest
			{
				Name = "My Name"
			};
			Mocks.ClassUnderTest.NewPlayerSetup(request);
		};

		It should_create_a_new_player = () => _player.ShouldNotBeNull();
	}

	[Subject("Application, Color Service, Playing the Game")]
	public class when_playing_the_game_and_the_player_chooses_the_wrong_color : BaseSpec<ColorService>
	{
		private static ColorRoundChoiceResponse _response;
		private static GameRound _gameRound;

		Establish context = () =>
		{
			var player = new PlayerBuilder("player 1").Build();
			var gameRound = new GameRoundBuilder(player).WithAnswer(Enums.Colors.Purple).Build();
			var repository = Mocks.Get<IGameRoundRepository>();
			repository.Stub(r => r.Get(Guid.Empty))
				.IgnoreArguments()
				.Return(gameRound);
			repository.Stub(r => r.Save(null))
				.Callback((GameRound entity) =>
				{
					_gameRound = entity;
					return true;
				});
		};

		Because of = () => 
		{
			var request = new ColorRoundChoiceRequest
			{
				GameRoundId = Guid.NewGuid(),
				ProvidedAnswer = Enums.Colors.Blue
			};
			_response = Mocks.ClassUnderTest.ColorRoundChoice(request);
		};

		It should_let_the_player_know_there_choice_was_wrong = () => _response.AttemptIsSuccessful.ShouldBeFalse();

		It should_keep_track_of_the_total_failures = () => (_gameRound.FailedAttempts > 0).ShouldBeTrue();
	}

	[Subject("Application, Color Service, Playing the Game")]
	public class when_playing_the_game_and_the_player_chooses_the_right_color : BaseSpec<ColorService>
	{
		private static ColorRoundChoiceResponse _response;
		private static GameRound _gameRound;

		Establish context = () =>
		{
			var player = new PlayerBuilder("player 1").Build();
			var gameRound = new GameRoundBuilder(player).WithAnswer(Enums.Colors.Blue).Build();
			var repository = Mocks.Get<IGameRoundRepository>();
			repository.Stub(r => r.Get(Guid.Empty))
				.IgnoreArguments()
				.Return(gameRound);
			repository.Stub(r => r.Save(null))
				.Callback((GameRound entity) =>
				{
					_gameRound = entity;
					return true;
				});
		};

		Because of = () => 
		{
			var request = new ColorRoundChoiceRequest
			{
				GameRoundId = Guid.NewGuid(),
				ProvidedAnswer = Enums.Colors.Blue
			};
			_response = Mocks.ClassUnderTest.ColorRoundChoice(request);
		};

		It should_let_the_player_know_they_were_right = () => _response.AttemptIsSuccessful.ShouldBeTrue();

		It should_award_points_to_the_player = () => _gameRound.Score.HasValue.ShouldBeTrue();
	}
}