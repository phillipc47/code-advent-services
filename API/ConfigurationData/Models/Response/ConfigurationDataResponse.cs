﻿using System.Collections.Generic;

namespace API.ConfigurationData.Models.Response
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
