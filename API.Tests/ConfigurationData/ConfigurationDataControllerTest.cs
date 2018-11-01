using System.Collections.Concurrent;
using API.ConfigurationData;
using API.ConfigurationData.Models.Response;
using AutoMapper;
using ConfigurationData.Service;
using Domain.Models;
using Moq;
using Xunit;
using ServiceEndpointDetail = Domain.Models.ServiceEndpointDetail;

namespace API.Tests.ConfigurationData
{
	public class ConfigurationDataControllerTest
	{
		private ConfigurationDataResponse SuccessfulRead(ConfigurationDataController controller)
		{
			var controllerResult = controller.ReadConfiguration();
			return ControllerTestHelper<ConfigurationDataResponse>.Successful(controllerResult);
		}

		private IMapper SetupMapper(ConfigurationDataResponse expectedResponse)
		{
			var mockMapper = new Mock<IMapper>();
			mockMapper.Setup(mapper => mapper.Map<ConfigurationDataResponse>(It.IsAny<ConfigurationDataEntity>()))
				.Returns(expectedResponse);

			return mockMapper.Object;
		}

		private IConfigurationDataService SetupConfigurationDataService(ConfigurationDataEntity expectedResult)
		{
			var mockService = new Mock<IConfigurationDataService>();
			mockService.Setup(service => service.Lookup()).Returns(expectedResult);

			return mockService.Object;
		}


		[Fact]
		public void ReadConfiguration_Delegates_To_Service()
		{
			ConfigurationDataEntity expectedEntity = new ConfigurationDataEntity()
			{
				Something = "some data",
				ServiceEndpoints = new ConcurrentDictionary<string, ServiceEndpointDetail>()
			};
			IConfigurationDataService service = SetupConfigurationDataService(expectedEntity);

			ConfigurationDataResponse expectedResponse = new ConfigurationDataResponse();
			IMapper mapper = SetupMapper(expectedResponse);

			var controller = new ConfigurationDataController(service, mapper);
			var response = SuccessfulRead(controller);

			Assert.Same(expectedResponse, response);
		}
	}
}
