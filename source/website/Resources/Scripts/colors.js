CORE.UI.Color = {};

CORE.UI.Color.GameRound = function () {
	var _gameRoundId;
	var _answer;
	var _choices;
	var _elements;
	var _urls;
	var _actions;

	this.init = function (data, elements, urls, actions) {
		_gameRoundId = data.GameRoundId;
		_answer = data.Answer;
		_choices = data.Choices;
		_elements = elements;
		_urls = urls;
		_actions = actions;
	};

	this.draw = function () {
		_elements.currentColor.html(_answer);
		var randomIndexOrders = randomizeIndexOrder();
		_elements.colorChoice1.attr("class", "")
			.addClass(_choices[randomIndexOrders.first])
			.bind(Global.inputEventName, function () { chooseColor($(this).attr("class"), $(this)); });
		_elements.colorChoice2.attr("class", "")
			.addClass(_choices[randomIndexOrders.second])
			.bind(Global.inputEventName, function () { chooseColor($(this).attr("class"), $(this)); });
		_elements.colorChoice3.attr("class", "")
			.addClass(_choices[randomIndexOrders.third])
			.bind(Global.inputEventName, function () { chooseColor($(this).attr("class"), $(this)); });
	};

	function chooseColor(color, element) {
		CORE.ajax.post(_urls.submitColorChoise,
			{ GameRoundId: _gameRoundId, ProvidedAnswer: color },
			function (result) {
				if(result.Data.AttemptIsSuccessful) {
					_actions.displayWinningMessage();
					_actions.updateScoreDisplay();
				} else {
					element.attr("class", "invalid").unbind();
				}
			}, {}
		);
	}

	function randomizeIndexOrder()
	{
		var firstColorIndex = CORE.Util.random(3);
		var secondColorIndex = 0;
		var thirdColorIndex = 0;
		var secondSwitch = CORE.Util.random(2);

		switch (firstColorIndex)
		{
			case 1:
				if(secondSwitch === 1) {
					secondColorIndex = 2;
					thirdColorIndex = 3;
				} else {
					secondColorIndex = 3;
					thirdColorIndex = 2;
				}
			break;
			case 2:
				if(secondSwitch === 1) {
					secondColorIndex = 1;
					thirdColorIndex = 3;
				} else {
					secondColorIndex = 3;
					thirdColorIndex = 1;
				}
			break;
			case 3:
				if(secondSwitch === 1) {
					secondColorIndex = 1;
					thirdColorIndex = 2;
				} else {
					secondColorIndex = 2;
					thirdColorIndex = 1;
				}
			break;
		}

		return {
			first: firstColorIndex - 1,
			second: secondColorIndex - 1,
			third: thirdColorIndex - 1
		};
	}
};

CORE.UI.Color.Controller = function () {
	var _elements;
	var _settings;
	var _dialog;
	var _state;
	var _gameRound;

	this.init = function (elements, options) {
		_elements = elements;
		_settings = options;
		CORE.UI.Color.ControllerSettings = options;
		_dialog = new CORE.UI.dialog();
		_state = determineState();
		start();
	};

	function start() {
		if (_state === "requestPlayerInfo") {
			_dialog.show({
				title: "Hi! Who are you?",
				contentUrl: _settings.urls.provideInformationView,
				height: 250,
				width: 500,
				open: function (container) {
					$(".actions .play", container).bind(Global.inputEventName, function () {
						sendPlayerInformation(container);
						return false;
					});
					$(".actions .cancel", container).bind(Global.inputEventName, function () {
						// TODO: implement playing as guest instead of sending home.
						document.location = _settings.urls.home;
						return false;
					});
				},
				close: function (container) {
					container.remove();
				}
			});
		} else if (_state === "canPlayGame") {
			CORE.ajax.post(_settings.urls.getNextColor,
				{ PlayerId: _settings.playerId },
				function (result) {
					_gameRound = new CORE.UI.Color.GameRound();
					_gameRound.init(result.Data, 
						_elements.gameRoundElements, 
						_settings.urls.gameRoundUrls,
						{
							displayWinningMessage: displayWinningMessage,
							updateScoreDisplay: updateScoreDisplay
						}
					);
					_gameRound.draw();
				}, {
				}
			);
		}
	};

	function sendPlayerInformation(container) {
		var data = {
			Name: $("INPUT[name=Name]", container).val()
		};
		CORE.ajax.post(_settings.urls.provideInformationSave,
			data,
			function (result) {
				var url = _settings.urls.play += "?playerId=" + result.Data.PlayerId;
				document.location = url;
			}, {
				onFailure: function (result) {
					if (result.FailureType === "validation") {
						var messages = CORE.Util.formatValidationMessage(result.ValidationMessages);
						$(".validationMessage", container).html(messages);
					} else {
						alert(result.FailureMessage);
					}
				}
			}
		);
	}

	function determineState() {
		var state = "";
		if (_settings.requestPlayerInfo) {
			state = "requestPlayerInfo";
		} else if (_settings.canPlayGame) {
			state = "canPlayGame";
		}
		return state;
	}

	function displayWinningMessage() {
		var content = _elements.youWinContentElement.html();
		_dialog.show({
			title: "Hooray!",
			content: content,
			height: 200,
			width: 350,
			open: function (container) {
				$(".actions .play", container).bind(Global.inputEventName, function () {
					var url = _settings.urls.play += "?playerId=" + _settings.playerId;
					document.location = url;
					return false;
				});
				$(".actions .quit", container).bind(Global.inputEventName, function () {
					document.location = _settings.urls.home;
					return false;
				});
			},
			close: function (container) {
				container.remove();
			}
		});
	}

	function updateScoreDisplay() {
		CORE.ajax.post(_settings.urls.displayScore,
			{ PlayerId: _settings.playerId },
			function (result) {
				_elements.scoreBoardScore.html(result.Data.Score);
			}, {}
		);
	}
};