using System.Collections.Generic;

namespace Distributor.Services.Distribution
{
	public interface IDistributionService
	{
		bool Redistribute(IList<int> memoryBanks, int startingIndex, out IList<int> result);
	}
}
