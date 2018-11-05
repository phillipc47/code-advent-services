using System.Collections.Generic;
using Jump.Models;

namespace Jump.Services
{
	public class StepService : IStepService
	{
		private void RecordStep(int currentIndex, StepResult stepResult, ref int stepCounter)
		{
			stepResult.JumpOffsets[currentIndex] += 1;
			stepCounter += 1;
		}

		private bool WeFoundOurWayOut ( int nextIndex, StepResult stepResult)
		{
			return nextIndex < 0 || nextIndex >= stepResult.JumpOffsets.Count;
		}

		private int DetermineCurrentOffset(int currentIndex, StepResult stepResult)
		{
			return stepResult.JumpOffsets[currentIndex];
		}

		//TODO: Look at model, consider domain if appropriate
		public StepResult DetermineExitSteps(IList<int> jumpOffsets)
		{
			var stepResult = new StepResult(jumpOffsets);

			int currentIndex = 0;
			int stepCounter = 0;
			for (int nextIndex = 0; !WeFoundOurWayOut(nextIndex, stepResult); )
			{
				int currentOffset = DetermineCurrentOffset(currentIndex, stepResult);

				RecordStep(currentIndex, stepResult, ref stepCounter);

				currentIndex = nextIndex = currentIndex + currentOffset;
			}

			stepResult.ExitSteps = stepCounter;
			return stepResult;
		}
	}
}
