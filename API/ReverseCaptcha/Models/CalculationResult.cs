using Newtonsoft.Json;

namespace API.ReverseCaptcha.Models
{
	public class CalculationResult
	{
		//TODO: Apply global json settings, lowercase, with - delimeters as well
		[JsonProperty(PropertyName = "input")]
		public string Input { get; set; }

		[JsonProperty(PropertyName = "result")]
		public string Result { get; set; }
	}

}
