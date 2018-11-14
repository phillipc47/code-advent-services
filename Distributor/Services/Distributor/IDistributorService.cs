using System.Collections.Generic;

namespace Distributor.Services.Distributor
{
	public interface IDistributorService
	{
		int CountCycles(IList<int> memoryBanks);
	}
}
