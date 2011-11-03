<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="container">
	<% Html.BeginForm(); %>
		<section class="content">
			<h1>Ready to Get Started?</h1>
			<p>Sorry, but we don't know who you are yet. Go ahead and give us your name so we can tally-up your score for you!</p>
			<div class="textcenter">
				<%= Html.TextBox("Name", string.Empty, new { @class="test" }) %>
			</div>
		</section>
		<section class="validationMessage">
		</section>
		<section class="actions">
			<a href="#" class="primaryAction play">Play</a>
			<a href="#" class="secondaryAction cancel">No Thanks</a>
		</section>
	<% Html.EndForm(); %>
</div>