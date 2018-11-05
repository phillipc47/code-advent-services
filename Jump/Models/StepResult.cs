using System.Collections.Generic;
using System.Linq;

namespace Jump.Models
{
	public class StepResult
	{
		public int ExitSteps { get; set; }
		public IList<int> JumpOffsets { get; }

		public StepResult(IList<int> jumpOffsets)
		{
			JumpOffsets = (jumpOffsets == null) ? new List<int>() : new List<int>().Concat(jumpOffsets).ToList();
		}
	}
}
