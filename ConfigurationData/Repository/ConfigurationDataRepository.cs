using System.Collections.Generic;
using Domain.Models;
using Microsoft.Extensions.Options;
using DomainConfiguration = Domain.Infrastructure.ApplicationConfiguration;

namespace API.ConfigurationData.Repositories
{
	public class ConfigurationDataRespository: IConfigurationDataRepository
	{
		private DomainConfiguration.ConfigurationData ConfigurationData { get; }

		private IDictionary<string, ServiceEndpointDetail> BuildEndpoints()
		{
			IDictionary<string, ServiceEndpointDetail> endpoints = new Dictionary<string, ServiceEndpointDetail>();

			//TODO: Can use AutoMapper for this
			if (ConfigurationData.ServiceEndpoints != null)
			{
				foreach (var serviceEndpoint in ConfigurationData.ServiceEndpoints)
				{
					endpoints.Add(serviceEndpoint.KeyName, new ServiceEndpointDetail() {Url = serviceEndpoint.Url});
				}
			}

			return endpoints;
		}

		private ConfigurationDataEntity CreateEmptyModel()
		{
			return new ConfigurationDataEntity()
			{
				ServiceEndpoints = new Dictionary<string, ServiceEndpointDetail>(),
				Something = string.Empty
			};
		}

		private ConfigurationDataEntity CreatePopulatedModel()
		{
			return new ConfigurationDataEntity()
			{
				Something = ConfigurationData.Something == null ? string.Empty : ConfigurationData.Something,
				ServiceEndpoints = BuildEndpoints()
			};
		}

		public ConfigurationDataRespository(IOptions<DomainConfiguration.ConfigurationData> configurationDataOptions)
		{
			ConfigurationData = configurationDataOptions.Value;
		}

		public ConfigurationDataEntity LoadConfigurationData()
		{
			if (ConfigurationData == null)
			{
				return CreateEmptyModel();
			}

			return CreatePopulatedModel();
		}
	}
}
