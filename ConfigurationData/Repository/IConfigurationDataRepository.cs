using Domain.Models;

namespace API.ConfigurationData.Repositories
{
	public interface IConfigurationDataRepository
	{
		ConfigurationDataEntity LoadConfigurationData();
	}
}
