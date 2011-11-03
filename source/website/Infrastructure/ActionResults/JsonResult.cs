using System.Collections.Generic;
using System.Linq;
using Color.Core.Domain;

namespace Color.Website.Infrastructure.ActionResults
{
	public class JsonResult : System.Web.Mvc.JsonResult
	{
		public static JsonResult CreateSuccess(object data)
		{
			return new JsonResult { Data = new { Succeeded = true, ValidationMessages = new string[] {}, Data = data } };
		}

		public static JsonResult CreateFailure(string failureMessage, object data)
		{
			return new JsonResult { Data = new { Succeeded = false, FailureType = "unknown", FailureMessage = failureMessage, ValidationMessages = new string[] {}, Data = data} };
		}

		public static JsonResult CreateFailure(string failureMessage, IEnumerable<ValidationError> validationErrors, object data)
		{
			return new JsonResult { Data = new { Succeeded = false, FailureType = "validation", FailureMessage = failureMessage, ValidationMessages = validationErrors.Select(vm => new { vm.Type, vm.Message }).ToArray(), Data = data} };
		}
	}
}