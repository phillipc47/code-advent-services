using System.Collections.Generic;
using System.Linq;
using API.Jump.Models;
using CheckSum.Validators;
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

		// Can be made generic if the need arises
		private IList<int> Flatten(IList<IList<int>> sourceLists)
		{
			IList<int> flattenedList = new List<int>();
			return sourceLists.Aggregate(flattenedList, (current, currentList) => current.Concat(currentList).ToList());
		}

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

			var jumpOffsets = Flatten(numericValidationResult.Input);
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
