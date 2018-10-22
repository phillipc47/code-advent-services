using System.Collections.Generic;

//TODO: Move this to Domain Models so as not to be confused with Repository Entity, because this is NOT the repository model
namespace API.ConfigurationData.Models
{
	public class ServiceEndpointDetail
	{
		public string Url { get; set; }
	}

	public class ConfigurationDataEntity
	{
		public IDictionary<string, ServiceEndpointDetail> ServiceEndpoints;

		public string Something { get; set; }
	}
}
