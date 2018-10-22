using System.Collections.Generic;
using API.ConfigurationData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ConfigurationModel = API.Infrastructure.ApplicationConfiguration;

namespace API.ConfigurationData
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfigurationDataController : ControllerBase
	{
		private ConfigurationModel.ConfigurationData ConfigurationData { get; }

		public ConfigurationDataController(IOptions<ConfigurationModel.ConfigurationData> configurationDataOptions)
		{
			ConfigurationData = configurationDataOptions.Value;
		}

		[HttpGet]
		public ActionResult<ConfigurationDataResponse> ReadConfiguration()
		{
			//TODO: Inject service, etc.....
			IDictionary<string, ServiceEndpointDetail> endpoints = new Dictionary<string, ServiceEndpointDetail>();

			//TODO: Can use AutoMapper for this
			foreach (var serviceEndpoint in ConfigurationData.ServiceEndpoints)
			{
				endpoints.Add(serviceEndpoint.KeyName, new ServiceEndpointDetail() { Url = serviceEndpoint.Url } );
			}

			var response = new ConfigurationDataResponse() {ServiceEndpoints = endpoints, Something = ConfigurationData.Something };
			return Ok(response);
		}
	}
}
