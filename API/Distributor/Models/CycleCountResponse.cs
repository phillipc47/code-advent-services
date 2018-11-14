using System.Collections.Generic;

namespace API.Distributor.Models
{
	public class CycleCountResponse
	{
		public string Input { get; set; }
		public IList<int> InputList { get; set; } = new List<int>();
		public int Result { get; set; }
	}
}
