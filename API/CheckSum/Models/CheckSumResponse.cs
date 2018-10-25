using System.Collections.Generic;

namespace API.CheckSum.Models
{
	public class CheckSumResponse
	{
		public IList<IList<int>> InputRows { get; set; }
		public string CheckSum { get; set; }
	}
}
