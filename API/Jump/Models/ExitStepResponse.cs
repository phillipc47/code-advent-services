using System.Collections.Generic;

namespace API.Jump.Models
{
	public class ExitStepResponse
	{
		public string Input { get; set; }
		public IList<int> InputRows { get; set; } = new List<int>();
		public int Result { get; set; }
	}
}
