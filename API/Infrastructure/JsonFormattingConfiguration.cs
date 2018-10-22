using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Infrastructure
{
	public static class JsonFormattingConfiguration
	{
		public static void Configure(IMvcBuilder mvcBuilder)
		{
			mvcBuilder.AddJsonOptions(options =>
			{
				options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				options.SerializerSettings.Formatting = Formatting.Indented;
			});
		}
	}
}
