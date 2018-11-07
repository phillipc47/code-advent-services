using System.Collections.Generic;
using System.Linq;

namespace Distributor.Services.Distribution
{
	public class CircularDistributionService : IDistributionService
	{
		public bool Redistribute(IList<int> memoryBanks, int startingIndex, out IList<int> result)
		{
			if ( memoryBanks == null || startingIndex < 0 || startingIndex >= memoryBanks.Count)
			{
				result = new List<int>();
				return false;
			}

			result = memoryBanks.ToList();
			int blocksToRedistribute = result[startingIndex];
			result[startingIndex] = 0;

			int index = startingIndex;
			while (blocksToRedistribute > 0)
			{
				index = (index + 1) % result.Count;

				result[index] += 1;
				blocksToRedistribute -= 1;
			}

			return true;
		}
	}
}
