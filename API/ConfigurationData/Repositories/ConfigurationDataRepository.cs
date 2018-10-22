using System.Collections.Generic;
using Microsoft.Extensions.Options;
using APIModel = API.Infrastructure.ApplicationConfiguration;
using EntityModel = API.ConfigurationData.Models;

namespace API.ConfigurationData.Repositories
{
	public class ConfigurationDataRespository: IConfigurationDataRepository
	{
		private APIModel.ConfigurationData ConfigurationData { get; }

		private IDictionary<string, EntityModel.ServiceEndpointDetail> BuildEndpoints()
		{
			IDictionary<string, EntityModel.ServiceEndpointDetail> endpoints =
				new Dictionary<string, EntityModel.ServiceEndpointDetail>();

			//TODO: Can use AutoMapper for this
			foreach (var serviceEndpoint in ConfigurationData.ServiceEndpoints)
			{
				endpoints.Add(serviceEndpoint.KeyName, new EntityModel.ServiceEndpointDetail() { Url = serviceEndpoint.Url });
			}

			return endpoints;
		}

		public ConfigurationDataRespository(IOptions<APIModel.ConfigurationData> configurationDataOptions)
		{
			ConfigurationData = configurationDataOptions.Value;
		}

		public EntityModel.ConfigurationDataEntity LoadConfigurationData()
		{
			return new EntityModel.ConfigurationDataEntity()
			{
				Something = ConfigurationData.Something,
				ServiceEndpoints = BuildEndpoints()
			};
		}
	}
}
