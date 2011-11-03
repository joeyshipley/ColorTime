var Global = {};

Global.eventName = "";
// Use: _elements.actionLink.bind(Global.eventName, function () { alert("rawr"); });

Global.init = function (options) {
	try {
		var settings = $.extend({}, options);

		Global.eventName = ("createTouch" in document) ? "touchend" : "click";

		$.ajaxSetup({
			cache: false,
			traditional: true,
			error: function (xmlHttpRequest) {
				if (xmlHttpRequest.status !== 0) {
					throw new Error("There was an error making an ajax request to " + this.url + "\r\nHTTP Status: " + xmlHttpRequest.status + " " + xmlHttpRequest.statusText);
				}
			}
		});
	} catch (ex) {
		// TODO: fire error logging
	}
};

CORE = {};

var _CORE_AJAX = {
	post: function (url, data, onSuccess, options) {
		var settings = $.extend({
			contentType: 'application/x-www-form-urlencoded',
			dataType: 'json',
			async: true,
			alertOnFailure: true,
			alertOnError: true,
			onFailure: undefined,
			onError: undefined
		}, options);
		_CORE_AJAX._ajax('POST', url, data, onSuccess, settings);
	},
	get: function (url, data, onSuccess, options) {
		var settings = $.extend({
			dataType: 'json',
			async: true,
			alertOnFailure: true,
			alertOnError: true,
			onFailure: undefined,
			onError: undefined
		}, options);
		_CORE_AJAX._ajax('GET', url, data, onSuccess, settings);
	},
	_ajax: function (method, url, data, onSuccess, settings) {
		var _settings = settings;
		$.ajax({
			type: method,
			url: url,
			contentType: _settings.contentType,
			dataType: _settings.dataType,
			data: data,
			async: _settings.async,
			success: function (result) {
				if (result.Succeeded) {
					if (onSuccess !== undefined) { onSuccess(result); return; }
				} else {
					if (_settings.onFailure !== undefined) { _settings.onFailure(result); return; }
					if (_settings.alertOnFailure) { alert(result.FailureMessage); }
				}
			},
			error: function (xhr, status) {
				if (_settings.onError !== undefined) { _settings.onError(status); return; }
				if (_settings.alertOnError !== undefined) { alert("Uh-oh!\n" + status); }
			}
		});
	}
};
CORE.ajax = _CORE_AJAX;

CORE.UI = {};

CORE.UI.dialog = function () {
	var _element;

	this.show = function (options) {
		var settings = $.extend({
			title: "",
			content: "",
			contentUrl: undefined,
			contentUrlData: {},
			modal: true,
			height: undefined,
			width: undefined,
			resizable: false,
			open: undefined,
			close: undefined
		}, options);

		var content = (settings.content === undefined ? "Error loading dialog" : settings.content);
		_element = $(document.createElement('div'));
		_element.addClass("dialogContainer")
			.html(content)
			.dialog({
				dialogClass: 'isDialog',
				width: settings.width,
				minHeight: settings.height,
				title: settings.title,
				modal: settings.modal,
				resizable: settings.resizable,
				closeOnEscape: false,
				open: function () {
					if (settings.contentUrl !== undefined) {
						_element.html("<h1>please wait...</h1>");
						$.ajax({
							url: settings.contentUrl,
							data: settings.contentUrlData,
							success: function(content) {
								_element =  _element.html(content);
								if (settings.open !== undefined) { settings.open(_element); }
							},
							error: function(xhr, status) { _element = _element.html("<h1>Failed to load!</h1><div>" + status + "</div>"); }
						});
					}
				},
				close: function () {
					if (settings.close !== undefined) { settings.close($(this)); }
					if (_element) { _element.remove(); }
				}
		});
	};

	this.hide = function () {
		_element.dialog('close');
	};
};

CORE.Util = {};

CORE.Util.formatValidationMessage = function (errors) {
	var formattedErrors = "";
	for (var i = 0; i < errors.length; i++) {
		formattedErrors += "<div>" + errors[i].Message + "</div>";
	}
	return formattedErrors;
};