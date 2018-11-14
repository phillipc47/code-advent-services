using API.Distributor.Models;
using CheckSum.Validators;  //TODO: Move out of Checksum to higher level
using Distributor.Services.Distributor;
using Domain.Helpers.Number;
using Microsoft.AspNetCore.Mvc;

namespace API.Distributor
{
	[Route("api/[controller]")]
	[ApiController]
	public class DistributorController : ControllerBase
	{
		private IDistributorService Distributor { get; }
		private INumericValidator Validator { get;  }


		public DistributorController(INumericValidator validator, IDistributorService distributor)
		{
			Validator = validator;
			Distributor = distributor;
		}

		[HttpGet]
		public ActionResult<CycleCountResponse> CountCycles([FromQuery] string input)
		{
			var validationResult = Validator.Validate(input);

			if (!validationResult.ValidationResult.IsValid)
			{
				return BadRequest(validationResult.ValidationResult.Messages);
			}

			var memoryBanks = NumberHelper.Flatten(validationResult.Input);
			var response = new CycleCountResponse()
			{
				Result = Distributor.CountCycles(memoryBanks),
				Input = input,
				InputList = memoryBanks
			};

			return Ok(response);
		}
	}
}
