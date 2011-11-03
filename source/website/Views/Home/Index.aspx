<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Home of the Coloriful
</asp:Content>

<asp:Content ContentPlaceHolderID="InlineStyles" runat="server">
	<style type="text/css">
		.panel.left
		{
			width: 45%;
		}
		.panel.right
		{
			float: right;
			width: 40%;
		}
		.scores .panel
		{
			width: 42%;
		}
	</style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="homePage">
	<section class="panel left">
		<h1>Welcome Color Connoisseur!</h1>
		<h3>Feeling a bit of the ol'Color Confusion?</h3>
		<p>Have you ever thought to yourself "Gee, I would love to know that color is," or perhaps "That color is amazing, its a shame I haven't a clue to what it is." Well, you're in luck! Here at Coloriful&trade;&reg;&copy; we pride ourselves in our vast knowledge of primary and secondary colors alike!</p>
		<h3>Well, Step Right Up!</h3>
		<p>You with your inexcuseable lack of color correctness, and us the masters of all things color, are a perfect fit. Take a deep breath, relax, and enjoy the moment. Once you start playing, it'll be one emotional roller coaster you can't stop!</p>
		<h3>
			<a class="play" href="<%: Html.BuildUrlFromExpression<ColorController>(c => c.View(null)) %>">Let's Play &raquo;</a>
		</h3>
	</section>
	<section class="panel right scores">
		<h1>High Scores</h1>
		<div>
			<h2>Monthly</h2>
			<p>Coming soon</p>
		</div>
		<div>
			<h2>Alltime</h2>
			<p>Coming soon</p>
		</div>
	</section>
</div>
</asp:Content>
