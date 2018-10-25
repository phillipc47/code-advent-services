using AutoMapper;
using ConfigurationData.Service;
using Microsoft.AspNetCore.Mvc;
using Response = API.ConfigurationData.Models.Response;

namespace API.ConfigurationData
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigurationDataController : ControllerBase
	{
		private IConfigurationDataService Service { get; }
		private IMapper Mapper { get; }

		public ConfigurationDataController(IConfigurationDataService service, IMapper mapper)
		{
			Service = service;
			Mapper = mapper;
		}

		[HttpGet]
		public ActionResult<Response.ConfigurationDataResponse> ReadConfiguration()
		{
			var configurationDataEntity = Service.Lookup();

			var configurationDataResponse = Mapper.Map<Response.ConfigurationDataResponse>(configurationDataEntity);
			return Ok(configurationDataResponse);
		}
	}
}
