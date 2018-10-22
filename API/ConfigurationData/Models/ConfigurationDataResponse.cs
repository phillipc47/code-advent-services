using System.Collections.Generic;

namespace API.ConfigurationData.Models
{
	public class ServiceEndpointDetail
	{
		public string Url { get; set; }
	}

	public class ConfigurationDataResponse
	{
		public IDictionary<string, ServiceEndpointDetail> ServiceEndpoints;

		public string Something { get; set; }
	}
}
