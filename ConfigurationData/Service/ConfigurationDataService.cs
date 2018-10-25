using API.ConfigurationData.Repositories;
using ConfigurationData.Service;
using Domain.Models;

namespace API.ConfigurationData.Services
{
	public class ConfigurationDataService : IConfigurationDataService
	{
		private IConfigurationDataRepository Repository { get; }

		public ConfigurationDataService(IConfigurationDataRepository repository)
		{
			Repository = repository;
		}

		public ConfigurationDataEntity Lookup()
		{
			var data = Repository.LoadConfigurationData();

			return data;
		}
	}
}
