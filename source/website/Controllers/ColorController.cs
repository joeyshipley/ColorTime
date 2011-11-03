using System;
using System.Linq;
using System.Web.Mvc;
using Color.Core.Application.Requests;
using Color.Core.Application.Services;
using Color.Website.Models;
using JsonResult=Color.Website.Infrastructure.ActionResults.JsonResult;

namespace Color.Website.Controllers
{
	public class ColorController : Controller
	{
		private readonly IColorService _colorService;
		private readonly ColorModelFactory _colorModelFactory;

		public ColorController(IColorService colorService, ColorModelFactory colorModelFactory)
		{
			_colorService = colorService;
			_colorModelFactory = colorModelFactory;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult View(PlayViewRequest request)
		{
			var response = _colorService.PlayView(request);
			var model = _colorModelFactory.CreatePlayViewModel(response);
			return View("Index", model);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public PartialViewResult NewPlayerSetup()
		{
			return PartialView("NewPlayerSetup");
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public JsonResult NewPlayerSetup(NewPlayerSetupRequest request)
		{
			try
			{
				var response = _colorService.NewPlayerSetup(request);
				if(response.Errors.Any())
					return JsonResult.CreateFailure("Validation Error", response.Errors, new {});

				var model = _colorModelFactory.CreateNewPlayerSetupModel(response);
				return JsonResult.CreateSuccess(model);
			}
			catch (Exception ex)
			{
				return JsonResult.CreateFailure(ex.Message, ex);
			}
		}
	}
}
