using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace API.Tests
{
	public static class ControllerTestHelper<T> where T : class
	{
		public static T Successful(ActionResult<T> controllerResponse)
		{
			var okResult = controllerResponse.Result as OkObjectResult;
			Assert.NotNull(okResult);
			Assert.Equal(200, okResult.StatusCode);

			Assert.NotNull(okResult.Value);
			var typedResult = okResult.Value as T;
			Assert.NotNull(typedResult);

			return typedResult;
		}

		public static void BadRequestResult(ActionResult<T> controllerResponse)
		{
			var badRequestResult = controllerResponse.Result as BadRequestObjectResult;
			Assert.NotNull(badRequestResult);
		}
	}
}
