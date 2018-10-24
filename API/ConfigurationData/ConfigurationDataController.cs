using System.Collections.Generic;
using ConfigurationData.Service;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Response = API.ConfigurationData.Models.Response;

namespace API.ConfigurationData
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigurationDataController : ControllerBase
	{
		private IConfigurationDataService Service { get; }

		private Response.ConfigurationDataResponse CreateResponse(ConfigurationDataEntity configurationDataEntity)
		{
			var configurationDataResponse = new Response.ConfigurationDataResponse()
			{
				Something = configurationDataEntity.Something,
				ServiceEndpoints = new Dictionary<string, Response.ServiceEndpointDetail>()
			};
			return configurationDataResponse;
		}

		private void CopyServiceEndpoints(ConfigurationDataEntity configurationDataEntity, Response.ConfigurationDataResponse configurationDataResponse)
		{
			if (configurationDataEntity.ServiceEndpoints != null)
			{
				foreach (var entityEndpoint in configurationDataEntity.ServiceEndpoints)
				{
					configurationDataResponse.ServiceEndpoints.Add(entityEndpoint.Key, new Response.ServiceEndpointDetail() { Url = entityEndpoint.Value.Url });
				}
			}
		}

		public ConfigurationDataController(IConfigurationDataService service)
		{
			Service = service;
		}

		[HttpGet]
		public ActionResult<Response.ConfigurationDataResponse> ReadConfiguration()
		{
			var configurationDataEntity = Service.Lookup();

			//TODO: Automap
			var configurationDataResponse = CreateResponse(configurationDataEntity);
			CopyServiceEndpoints(configurationDataEntity, configurationDataResponse);

			return Ok(configurationDataResponse);
		}
	}
}
