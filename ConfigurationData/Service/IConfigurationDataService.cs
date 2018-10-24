using Domain.Models;

namespace ConfigurationData.Service
{

	public interface IConfigurationDataService
	{
		ConfigurationDataEntity Lookup();
	}
}
