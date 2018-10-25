using API.ConfigurationData.Repositories;
using API.ConfigurationData.Services;
using ConfigurationData.Service;
using Domain.Models;
using Moq;
using Xunit;

namespace ConfigurationData.Tests.Services
{
	public class ConfigurationDataServiceTest
	{
		[Fact]
		public void Retrieves_Data_From_Repository()
		{
			var mockRepsository = new Mock<IConfigurationDataRepository>();

			ConfigurationDataEntity expectedConfigurationData = new ConfigurationDataEntity();
			mockRepsository.Setup(repository => repository.LoadConfigurationData()).Returns(expectedConfigurationData);

			IConfigurationDataService service = new ConfigurationDataService(mockRepsository.Object);

			var result = service.Lookup();

			Assert.Same(expectedConfigurationData, result);
			mockRepsository.Verify(x => x.LoadConfigurationData(), Times.Once);
		}
	}
}
