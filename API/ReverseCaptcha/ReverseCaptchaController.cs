using API.ReverseCaptcha.Models;
using Microsoft.AspNetCore.Mvc;
using ReverseCaptcha;

namespace API.ReverseCaptcha
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReverseCaptchaController : ControllerBase
	{
		private IReverseCaptchaService Service { get; }

		public ReverseCaptchaController(IReverseCaptchaService service)
		{
			Service = service;
		}

		[HttpGet]
		public ActionResult<ReverseCaptchaResponse> Calculate([FromQuery]string input)
		{
			if (!int.TryParse(input, out int numericInput))
			{
				return BadRequest($"Specified input [{input}] is not numeric");
			}

			var calculationResult = new ReverseCaptchaResponse()
			{
				Input = input,
				Result = Service.Compute(numericInput).ToString()
			};

			return Ok( calculationResult );
		}
	}
}
