using System.Collections.Generic;
using API.ConfigurationData.Repositories;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using APIModel = API.Infrastructure.ApplicationConfiguration;
using EntityModel = API.ConfigurationData.Models;

namespace API.Tests.ConfigurationData.Repositories
{
	public class ConfigurationDataRepositoryTest
	{
		[Fact]
		public void LoadConfiguration_Uses_Injected_Configuration_Model()
		{
			var expectedConfigurationModel =
				new APIModel.ConfigurationData
				{
					ServiceEndpoints = new List<APIModel.ServiceEndpointDetail>(),
					Something = "something"
				};

			var mockRepository = new Mock<IOptions<APIModel.ConfigurationData>>();
			mockRepository.Setup(options => options.Value).Returns(expectedConfigurationModel);

			var repository = new ConfigurationDataRespository(mockRepository.Object);
			var entityModelResult = repository.LoadConfigurationData();

			Assert.NotNull(entityModelResult);
			Assert.Equal(expectedConfigurationModel.Something, entityModelResult.Something);

			Assert.NotNull(entityModelResult.ServiceEndpoints);
			Assert.Equal(expectedConfigurationModel.ServiceEndpoints.Count, entityModelResult.ServiceEndpoints.Count);
		}

		[Fact]
		public void LoadConfiguration_No_Configuration_Model()
		{
			var mockRepository = new Mock<IOptions<APIModel.ConfigurationData>>();
			mockRepository.Setup(options => options.Value).Returns((APIModel.ConfigurationData) null);

			var repository = new ConfigurationDataRespository(mockRepository.Object);
			var entityModelResult = repository.LoadConfigurationData();

			Assert.NotNull(entityModelResult);
			Assert.Equal(string.Empty, entityModelResult.Something);

			Assert.NotNull(entityModelResult.ServiceEndpoints);
			Assert.Equal(0, entityModelResult.ServiceEndpoints.Count);
		}

		[Fact]
		public void LoadConfiguration_No_Something()
		{
			var expectedConfigurationModel =
				new APIModel.ConfigurationData
				{
					ServiceEndpoints = new List<APIModel.ServiceEndpointDetail>(),
					Something = null
				};

			var mockRepository = new Mock<IOptions<APIModel.ConfigurationData>>();
			mockRepository.Setup(options => options.Value).Returns(expectedConfigurationModel);

			var repository = new ConfigurationDataRespository(mockRepository.Object);
			var entityModelResult = repository.LoadConfigurationData();

			Assert.NotNull(entityModelResult);
			Assert.NotNull(entityModelResult.Something);
			Assert.Equal(string.Empty, entityModelResult.Something);
		}

		private void CheckEndpoint(APIModel.ServiceEndpointDetail apiEndpoint, EntityModel.ConfigurationDataEntity entityModel)
		{
			EntityModel.ServiceEndpointDetail entityEndpoint = entityModel.ServiceEndpoints[apiEndpoint.KeyName];
			Assert.NotNull(entityEndpoint);
			Assert.Equal(apiEndpoint.Url, entityEndpoint.Url);
		}

		[Fact]
		public void LoadConfiguration_Copies_Over_Endpoints()
		{
			var expectedServiceEndpoints = new List<APIModel.ServiceEndpointDetail>
			{
				new APIModel.ServiceEndpointDetail() {KeyName = "firstKey", Url = "first url"},
				new APIModel.ServiceEndpointDetail() {KeyName = "secondKey", Url = "second url"}
			};

			var expectedConfigurationModel =
				new APIModel.ConfigurationData
				{
					ServiceEndpoints = expectedServiceEndpoints,
					Something = "does not matter for this test"
				};

			var mockRepository = new Mock<IOptions<APIModel.ConfigurationData>>();
			mockRepository.Setup(options => options.Value).Returns(expectedConfigurationModel);

			var repository = new ConfigurationDataRespository(mockRepository.Object);
			var entityModelResult = repository.LoadConfigurationData();

			Assert.NotNull(entityModelResult);

			Assert.NotNull(entityModelResult.ServiceEndpoints);
			Assert.Equal(expectedConfigurationModel.ServiceEndpoints.Count, entityModelResult.ServiceEndpoints.Count);

			// Can modularize this if need arises
			CheckEndpoint(expectedServiceEndpoints[0], entityModelResult);
			CheckEndpoint(expectedServiceEndpoints[1], entityModelResult);
		}
	}
}
