using System.Collections.Generic;

namespace Distributor.Services.BankSelector
{
	public interface IBankSelector
	{
		bool SelectBank(IList<int> memoryBanks, out int index);
	}
}
