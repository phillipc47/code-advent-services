using API.CheckSum.Models;
using CheckSum.Services;
using CheckSum.Validators;
using Microsoft.AspNetCore.Mvc;

namespace API.CheckSum
{
	[Route("api/[controller]")]
	[ApiController]
	public class CheckSumController : ControllerBase
	{
		private INumericValidator Validator { get; }
		private ICheckSumService Service { get; }

		public CheckSumController(INumericValidator validator, ICheckSumService service)
		{
			Validator = validator;
			Service = service;
		}

		[HttpGet]
		public ActionResult<CheckSumResponse> Calculate([FromQuery] string input)
		{
			var numericValidationResult = Validator.Validate(input);
			if (!numericValidationResult.ValidationResult.IsValid)
			{
				return BadRequest(numericValidationResult.ValidationResult.Messages);
			}

			var response = new CheckSumResponse()
			{
				Result = Service.Compute(numericValidationResult.Input).ToString(),
				Input = input,
				InputRows = numericValidationResult.Input
			};

			return Ok(response);
		}
	}
}
