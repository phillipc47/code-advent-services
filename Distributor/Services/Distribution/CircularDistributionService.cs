using System.Collections.Generic;

namespace Distributor.Services.Distribution
{
	public class CircularDistributionService : IDistributionService
	{
		public bool Redistribute(IList<int> memoryBanks, int startingIndex)
		{
			if ( memoryBanks == null || startingIndex < 0 || startingIndex >= memoryBanks.Count)
			{
				return false;
			}

			int blocksToRedistribute = memoryBanks[startingIndex];
			memoryBanks[startingIndex] = 0;

			int index = startingIndex;
			while (blocksToRedistribute > 0)
			{
				index = (index + 1) % memoryBanks.Count;

				memoryBanks[index] += 1;
				blocksToRedistribute -= 1;
			}

			return true;
		}
	}
}
