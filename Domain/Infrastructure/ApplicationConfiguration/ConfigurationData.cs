using System.Collections.Generic;

namespace Domain.Infrastructure.ApplicationConfiguration
{
	public class ServiceEndpointDetail
	{
		public string KeyName { get; set; }
		public string Url { get; set; }
	}

	public class ConfigurationData
	{
		public IList<ServiceEndpointDetail> ServiceEndpoints { get; set; } = new List<ServiceEndpointDetail>();

		public string Something { get; set; }
	}
}
