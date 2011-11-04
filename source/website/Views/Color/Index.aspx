<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Color.Website.Models.PlayViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Color Time!
</asp:Content>

<asp:Content ContentPlaceHolderID="InlineStyles" runat="server">
	<style type="text/css">
	</style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="colorGame">
	<% Html.BeginForm<ColorController>(c => c.View(null)); %> <!-- TODO: post to a play action instead. -->
	<%= Html.Hidden("PlayerId") %>

	<section class="scoreBoard">
		<div>
			<label>Player</label>
			<span class="displayName"><%= Model.PlayerName %></span>
		</div>
		<div class="textright">
			<label>Your Score</label>
			<span class="score"><%= Model.Score %></span>
		</div>
	</section>

	<section class="pickThisColor">
		<span class="message">Can you find the color</span>
		<span id="currentColor">&nbsp;</span>
	</section>

	<section class="colorCards">
		<div id="color1"></div>
		<div id="color2"></div>
		<div id="color3"></div>
	</section>

	<div id="youWin" class="noDisplay">
		<div class="content">
			<h1>You're Right!</h1>
		</div>
		<div class="actions textcenter">
			<a href="#" class="play specialAction">Play Again</a>
			<a href="#" class="quit specialAction smaller">Quit</a>
		</div>
	</div>
	<% Html.EndForm(); %>
</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="ExtendedJavascriptIncludes" runat="server">
	<script src="<%: Url.Content("~/Resources/Scripts/colors.js") %>" type="text/javascript"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="Javascript" runat="server">
	<script type="text/javascript">
		var _controller = new CORE.UI.Color.Controller();

		$(document).ready(function() {
			var elements = {
				newPlayerGreeting: $("#newPlayerGreeting"),
				scoreBoardDisplayName: $(".scoreBoard .displayName"),
				scoreBoardScore: $(".scoreBoard .score"),
				currentColor: $("#currentColorAnswer"),
				selectors: {
					modalValidationMessage: ".validationMessage",
					modalActionsPlay: ".actions .play"
				},
				gameRoundElements: {
					currentColor: $("#currentColor"),
					colorChoice1: $("#color1"),
					colorChoice2: $("#color2"),
					colorChoice3: $("#color3")
				},
				youWinContentElement: $("#youWin")
			};
			var options = {
				playerId: "<%= Model.PlayerId %>",
				requestPlayerInfo: <%= Model.RequestPlayerInfo.ToJavascriptEncode() %>,
				canPlayGame: <%= Model.CanPlayGame.ToJavascriptEncode() %>,
				urls: {
					home: "<%= Html.BuildUrlFromExpression<HomeController>(c => c.Index()) %>",
					play: "<%= Html.BuildUrlFromExpression<ColorController>(c => c.View(null)) %>",
					provideInformationView: "<%= Html.BuildUrlFromExpression<ColorController>(c => c.NewPlayerSetup()) %>",
					provideInformationSave: "<%= Html.BuildUrlFromExpression<ColorController>(c => c.NewPlayerSetup(null)) %>",
					getNextColor: "<%= Html.BuildUrlFromExpression<ColorController>(c => c.NextColorRound(null)) %>",
					displayScore: "<%= Html.BuildUrlFromExpression<ColorController>(c => c.DisplayScore(null)) %>",
					gameRoundUrls: {
						submitColorChoise: "<%= Html.BuildUrlFromExpression<ColorController>(c => c.ColorRoundChoice(null)) %>"
					}
				}
			};
			_controller.init(elements, options);
		});
	</script>
</asp:Content>