using System.Collections.Generic;
using Jump.Models;

namespace Jump.Services
{
	public interface IStepService
	{
		StepResult DetermineExitSteps(IList<int> jumpOffsets);
	}
}
