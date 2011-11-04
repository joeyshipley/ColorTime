<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
	What is Coloriful?
</asp:Content>

<asp:Content ContentPlaceHolderID="InlineStyles" runat="server">
	<style type="text/css">
		SECTION.panel
		{
			width: 55%;
		}
		DIV.panel
		{
			width: 42%;
			margin-bottom: 0.5em;
		}
	</style>
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
	<section class="panel">
		<h1>What's this Coloriful thing and how the heck did you do it?</h1>
		<p>Coloriful was made to show the basis of a test enforced, ASP.Net MVC app. From front to back, here's a list of the bits and parts.</p>
		<h2>The Insides</h2>
		<div class="panel">
			<ul>
				<li>ASP.Net MVC, C#</li>
				<li>StructureMap</li>
				<li>Machine.Specification</li>
				<li>Rhino Mocks</li>
				<li>LINQ</li>
				<li>NHibernate</li>
				<li>Fluent NHibernate</li>
			</ul>
		</div>
		<div class="panel">
			<ul>
				<li>HTML5</li>
				<li>CSS3</li>
				<li>lessCSS</li>
				<li>Class based JavaScript</li>
				<li>JQuery</li>
				<li>AJax</li>
			</ul>
		</div>
		<h2>Ok, but...</h2>
		<p>Now, I know what you're thinking; no way, right? Well, just poke around under the hood. You never know, you might end up liking it.</p>
		<p><a href="https://github.com/joeyshipley/ColorTime" target="_blank">ColorTime</a> on GitHub.com</p>
	</section>
</asp:Content>