using System.Collections.Generic;

namespace API.CheckSum.Models
{
	public class CheckSumResponse
	{
		public string Input { get; set; }
		public IList<IList<int>> InputRows { get; set; } = new List<IList<int>>();
		public string Result { get; set; }
	}
}
