using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Distributor.Models
{
	public class CycleCountResponse
	{
		public string Input { get; set; }
		public IList<int> InputList { get; set; } = new List<int>();
		public int Result { get; set; }
	}
}
