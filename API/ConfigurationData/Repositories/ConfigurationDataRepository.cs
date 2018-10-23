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
			IDictionary<string, EntityModel.ServiceEndpointDetail> endpoints = new Dictionary<string, EntityModel.ServiceEndpointDetail>();

			//TODO: Can use AutoMapper for this
			if (ConfigurationData.ServiceEndpoints != null)
			{
				foreach (var serviceEndpoint in ConfigurationData.ServiceEndpoints)
				{
					endpoints.Add(serviceEndpoint.KeyName, new EntityModel.ServiceEndpointDetail() {Url = serviceEndpoint.Url});
				}
			}

			return endpoints;
		}

		private EntityModel.ConfigurationDataEntity CreateEmptyModel()
		{
			return new EntityModel.ConfigurationDataEntity()
			{
				ServiceEndpoints = new Dictionary<string, EntityModel.ServiceEndpointDetail>(),
				Something = string.Empty
			};
		}

		private EntityModel.ConfigurationDataEntity CreatePopulatedModel()
		{
			return new EntityModel.ConfigurationDataEntity()
			{
				Something = ConfigurationData.Something == null ? string.Empty : ConfigurationData.Something,
				ServiceEndpoints = BuildEndpoints()
			};
		}

		public ConfigurationDataRespository(IOptions<APIModel.ConfigurationData> configurationDataOptions)
		{
			ConfigurationData = configurationDataOptions.Value;
		}

		public EntityModel.ConfigurationDataEntity LoadConfigurationData()
		{
			if (ConfigurationData == null)
			{
				return CreateEmptyModel();
			}

			return CreatePopulatedModel();
		}
	}
}
