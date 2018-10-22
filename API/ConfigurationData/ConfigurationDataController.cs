using API.ConfigurationData.Models.Response;
using API.ConfigurationData.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.ConfigurationData
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigurationDataController : ControllerBase
	{
		private IConfigurationDataService Service { get; }


		public ConfigurationDataController(IConfigurationDataService service)
		{
			Service = service;
		}

		[HttpGet]
		public ActionResult<ConfigurationDataResponse> ReadConfiguration()
		{
			return Ok(Service.Lookup());
		}
	}
}
