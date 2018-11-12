using System.Collections.Generic;
using System.Linq;
using Distributor.Services.BankSelector;
using Distributor.Services.Distribution;

namespace Distributor.Services.Distributor
{
	public class DistributorService : IDistributorService
	{
		private IBankSelector BankSelector { get; }
		private IDistributionService DistributionService { get; }

		private string CreateCyclePattern(IList<int> memoryBanks)
		{
			return string.Join(", ", memoryBanks.Select(block => block.ToString()).ToArray());
		}

		private int DetermineDistributionIndex(IList<int> memoryBanks, int selectedBankIndex)
		{
			return (selectedBankIndex + 1) % memoryBanks.Count;
		}

		public DistributorService(IBankSelector bankSelector, IDistributionService distributionService)
		{
			BankSelector = bankSelector;
			DistributionService = distributionService;
		}

		public int CountCycles(IList<int> memoryBanks)
		{
			// Yep, the copying back and forth of the list gets really inefficient, maybe look at this later if it becomes a performance concern

			int cycleCount = 0;
			if (memoryBanks != null && memoryBanks.Count > 0)
			{
				string cyclePattern;
				var distributedMemoryBanks = memoryBanks.ToList();
				ISet<string> cyclePatterns = new HashSet<string>();
				do
				{
					if (!BankSelector.SelectBank(distributedMemoryBanks, out int selectedBankIndex))
					{
						return 0;
					}

					cyclePattern = CreateCyclePattern(distributedMemoryBanks);
					cyclePatterns.Add(cyclePattern);

					int distributionIndex = DetermineDistributionIndex(distributedMemoryBanks, selectedBankIndex);
					if (!DistributionService.Redistribute(distributedMemoryBanks, distributionIndex, out var resultsOfDistribution))
					{
						return 0;
					}
					distributedMemoryBanks = resultsOfDistribution.ToList();

					cycleCount += 1;
				} while (!cyclePatterns.Contains(cyclePattern));
			}

			return cycleCount;
		}
	}
}
