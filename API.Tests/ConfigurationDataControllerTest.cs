using System;
using System.Collections.Generic;
using System.Text;
using API.ConfigurationData;
using API.ConfigurationData.Models;
using API.ConfigurationData.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace API.Tests
{
	public class ConfigurationDataControllerTest
	{
		[Fact]
		public void ReadConfiguration_Delegates_To_Service()
		{
			var mockRepository = new Mock<IConfigurationDataService>();
			ConfigurationDataEntity expectedResult = new ConfigurationDataEntity();
			mockRepository.Setup(service => service.Lookup()).Returns(expectedResult);

			var controller = new ConfigurationDataController(mockRepository.Object);

			var controllerResult = controller.ReadConfiguration();
			Assert.NotNull(controllerResult);

			var okResult = controllerResult.Result as OkObjectResult;
			Assert.NotNull(okResult);
			Assert.Equal(200, okResult.StatusCode);

			//TODO: Check the result coming back okResult.Value
		}
	}
}
