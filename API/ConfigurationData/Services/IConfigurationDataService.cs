using API.ConfigurationData.Models;

namespace API.ConfigurationData.Services
{

	public interface IConfigurationDataService
	{
		ConfigurationDataEntity Lookup();
	}
}
