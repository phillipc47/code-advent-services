using API.PassPhrase.Models;
using Microsoft.AspNetCore.Mvc;
using Passphrase.Services;

namespace API.PassPhrase
{
	[Route("api/[controller]")]
	[ApiController]
	public class PassPhraseController : ControllerBase
	{
		private IPassPhraseService Service { get; }

		public PassPhraseController(IPassPhraseService service)
		{
			Service = service;
		}

		[HttpGet]
		public ActionResult<PassPhraseValidityResponse> IsValid([FromQuery] string input)
		{
			var validityResponse = new PassPhraseValidityResponse()
			{
				Input = input,
				Result = Service.IsValid(input)
			};

			return Ok(validityResponse);
		}
	}
}
