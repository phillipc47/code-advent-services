using AutoMapper;
using Domain.Models;

namespace API.ConfigurationData.Models.Response
{
	public class ConfigurationDataProfile : Profile
	{
		public ConfigurationDataProfile()
		{
			CreateMap<ConfigurationDataEntity, ConfigurationDataResponse>();
		}
	}
}
