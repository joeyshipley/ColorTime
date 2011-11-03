<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Color.Website.Models.PlayViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Color Time!
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<% Html.BeginForm<ColorController>(c => c.View(null)); %> <!-- TODO: post to a play action instead. -->
	<%= Html.Hidden("PlayerId") %>

	<section class="scoreBoard">
		<div class="displayName"><%= Model.PlayerName %></div>
		<div class="score">0</div>
	</section>

	<h2>Hey! I'm the color <span id="currentColor">Red</span>! Which one am I?</h2>

	<div id="color1" class="colorCard"></div>
	<div id="color2" class="colorCard"></div>
	<div id="color3" class="colorCard"></div>

	<div id="newPlayerGreeting" class="noDisplay">
		<div class="modal">
			<h1>Welcome to Coloriful!</h1>
			<p>Sorry, but we don't know who you are yet. Go ahead and give us your name so we can tally your score as you play!</p>
			<div>
				<input type="text" id="name" name="name" />
			</div>
			<div class="validationMessage">
			</div>
			<div class="actions">
				<a href="#" class="play primaryAction">Play</a>
			</div>
		</div>
	</div>
	<% Html.EndForm(); %>
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
				}
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
					getNextColor: "",
					submitColorChoise: ""
				}
			};
			_controller.init(elements, options);
		});
	</script>
</asp:Content>