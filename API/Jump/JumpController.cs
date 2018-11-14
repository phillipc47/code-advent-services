using API.Jump.Models;
using CheckSum.Validators;
using Domain.Helpers.Number;
using Jump.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Jump
{
	[Route("api/[controller]")]
	[ApiController]
	public class JumpController : ControllerBase
	{
		private INumericValidator Validator { get; }
		private IStepService Service { get; }

		public JumpController(INumericValidator validator, IStepService service)
		{
			Validator = validator;
			Service = service;
		}

		[HttpGet]
		public ActionResult<ExitStepResponse> DetermineExitSteps([FromQuery] string input)
		{
			var numericValidationResult = Validator.Validate(input);
			if (!numericValidationResult.ValidationResult.IsValid)
			{
				return BadRequest(numericValidationResult.ValidationResult.Messages);
			}

			var jumpOffsets = NumberHelper.Flatten(numericValidationResult.Input);
			var response = new ExitStepResponse()
			{
				Input = input,
				InputRows = jumpOffsets,
				Result = Service.DetermineExitSteps(jumpOffsets).ExitSteps,
			};

			return Ok(response);
		}
	}
}
