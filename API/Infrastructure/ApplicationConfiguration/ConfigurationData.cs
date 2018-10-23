using System.Collections.Generic;

namespace API.Infrastructure.ApplicationConfiguration
{
	public class ServiceEndpointDetail
	{
		public string KeyName { get; set; }
		public string Url { get; set; }
	}

	public class ConfigurationData
	{
		//public IDictionary<string, ServiceEndpointDetail> ServiceEndpoints { get; } = new Dictionary<string, ServiceEndpointDetail>();

		public IList<ServiceEndpointDetail> ServiceEndpoints { get; set; } = new List<ServiceEndpointDetail>();

		public string Something { get; set; }
	}
}
