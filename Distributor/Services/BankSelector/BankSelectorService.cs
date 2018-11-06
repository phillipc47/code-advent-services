using System.Collections.Generic;

namespace Distributor.Services.BankSelector
{
	public class BankSelectorService : IBankSelector
	{
		private bool FindHighestBlockCount(IList<int> memoryBanks, out int highestBlockValue)
		{
			highestBlockValue = 0;
			int bankWithMostBlocksIndex = int.MinValue;
			for (int index = 0; index < memoryBanks.Count; index++)
			{
				if (memoryBanks[index] > highestBlockValue)
				{
					bankWithMostBlocksIndex = index;
					highestBlockValue = memoryBanks[index];
				}
			}

			return bankWithMostBlocksIndex != -1;
		}

		private bool FindIndexOfFirstMemoryBankWithBlockCount(int blockCount, IList<int> memoryBanks, out int index)
		{
			for (index = 0; index < memoryBanks.Count; index++)
			{
				if (memoryBanks[index] == blockCount)
				{
					return true;
				}
			}

			return false;
		}

		public bool SelectBank(IList<int> memoryBanks, out int index)
		{
			if (memoryBanks != null)
			{
				if (FindHighestBlockCount(memoryBanks, out int highestBlockCount))
				{
					return FindIndexOfFirstMemoryBankWithBlockCount(highestBlockCount, memoryBanks, out index);
				}
			}

			index = 0;
			return false;
		}
	}
}
