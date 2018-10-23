using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using API.ConfigurationData;
using API.ConfigurationData.Models;
using API.ConfigurationData.Models.Response;
using API.ConfigurationData.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Xunit;
using ServiceEndpointDetail = API.ConfigurationData.Models.ServiceEndpointDetail;

namespace API.Tests
{
	public class ConfigurationDataControllerTest
	{
		private ConfigurationDataResponse SuccessfulRead(ConfigurationDataController controller)
		{
			var controllerResult = controller.ReadConfiguration();
			Assert.NotNull(controllerResult);

			var okResult = controllerResult.Result as OkObjectResult;
			Assert.NotNull(okResult);
			Assert.Equal(200, okResult.StatusCode);
			Assert.NotNull(okResult.Value);

			var result = okResult.Value as ConfigurationDataResponse;
			Assert.NotNull(result);

			return result;
		}

		private IConfigurationDataService SetupConfigurationDataService(ConfigurationDataEntity expectedResult)
		{
			var mockRepository = new Mock<IConfigurationDataService>();
			mockRepository.Setup(service => service.Lookup()).Returns(expectedResult);

			return mockRepository.Object;
		}


		[Fact]
		public void ReadConfiguration_Delegates_To_Service()
		{
			ConfigurationDataEntity expectedResult = new ConfigurationDataEntity()
			{
				Something = "some data",
				ServiceEndpoints = new ConcurrentDictionary<string, ServiceEndpointDetail>()
			};
			IConfigurationDataService service = SetupConfigurationDataService(expectedResult);

			var controller = new ConfigurationDataController(service);
			var result = SuccessfulRead(controller);

			Assert.Equal(expectedResult.Something, result.Something);
			Assert.Equal(expectedResult.ServiceEndpoints.Count, result.ServiceEndpoints.Count);
		}

		[Fact]
		public void ReadConfiguration_Without_Service_Endpoints()
		{
			ConfigurationDataEntity expectedResult = new ConfigurationDataEntity()
			{
				Something = "some data",
				ServiceEndpoints = null
			};
			IConfigurationDataService service = SetupConfigurationDataService(expectedResult);

			var controller = new ConfigurationDataController(service);
			var result = SuccessfulRead(controller);

			Assert.Equal(expectedResult.Something, result.Something);

			Assert.NotNull(result.ServiceEndpoints);
			Assert.Equal(0, result.ServiceEndpoints.Count);
		}

		[Fact]
		public void ReadConfiguration_Copies_Every_Service_Endpoint()
		{
			IDictionary<string, ServiceEndpointDetail> serviceEndpoints = new Dictionary<string, ServiceEndpointDetail>();
			serviceEndpoints.Add("key1", new ServiceEndpointDetail() {Url = "url1"} );
			serviceEndpoints.Add("key2", new ServiceEndpointDetail() {Url = "url2" } );

			ConfigurationDataEntity expectedResult = new ConfigurationDataEntity()
			{
				Something = "some other data",
				ServiceEndpoints = serviceEndpoints
			};
			IConfigurationDataService service = SetupConfigurationDataService(expectedResult);

			var controller = new ConfigurationDataController(service);
			var result = SuccessfulRead(controller);

			Assert.Equal(expectedResult.Something, result.Something);

			// We can modularize the endpoint checks if we have future need of such methods
			Assert.NotNull(result.ServiceEndpoints);
			Assert.Equal(expectedResult.ServiceEndpoints.Count, result.ServiceEndpoints.Count);

			Assert.Equal(expectedResult.ServiceEndpoints["key1"].Url, result.ServiceEndpoints["key1"].Url);
			Assert.Equal(expectedResult.ServiceEndpoints["key2"].Url, result.ServiceEndpoints["key2"].Url);
		}
	}
}
