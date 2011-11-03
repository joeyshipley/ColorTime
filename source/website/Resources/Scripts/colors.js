CORE.UI.Color = {};

CORE.UI.Color.Controller = function () {
	var _elements;
	var _settings;
	var _dialog;
	var _state;

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
					$(".actions .play", container).bind(Global.eventName, function () {
						sendPlayerInformation(container);
						return false;
					});
					$(".actions .cancel", container).bind(Global.eventName, function () {
						// TODO: implement playing as guest instead of sending home.
						document.location = _settings.urls.home;
						return false;
					});
				},
				close: function (container) {
					container.remove();
				}
			});
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
		} else if (_settings.CanPlayGame) {
			state = "canPlayGame";
		}
		return state;
	}
};