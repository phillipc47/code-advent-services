using EntityModel = API.ConfigurationData.Models;

namespace API.ConfigurationData.Repositories
{
	public interface IConfigurationDataRepository
	{
		EntityModel.ConfigurationDataEntity LoadConfigurationData();
	}
}
