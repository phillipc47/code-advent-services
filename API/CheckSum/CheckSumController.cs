using API.CheckSum.Models;
using CheckSum.Validation;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.CheckSum
{
	[Route("api/[controller]")]
	[ApiController]
	public class CheckSumController : ControllerBase
	{
		private INumericValidator Validator { get; }

		public CheckSumController(INumericValidator validator)
		{
			Validator = validator;
		}

		[HttpGet]
		public ActionResult<CheckSumResponse> Calculate([FromQuery] string input)
		{
			NumericValidationResultEntity numericValidationResult = Validator.Validate(input);
			if (!numericValidationResult.ValidationResult.IsValid)
			{
				return BadRequest(numericValidationResult.ValidationResult.Messages);
			}

			//TODO: Invoke service
			return Ok(numericValidationResult.Input);
		}
	}
}
