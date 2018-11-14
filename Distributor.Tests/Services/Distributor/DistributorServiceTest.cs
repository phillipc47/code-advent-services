using System.Collections.Generic;
using Distributor.Services.BankSelector;
using Distributor.Services.Distribution;
using Distributor.Services.Distributor;
using Moq;
using Xunit;

namespace Distributor.Tests.Services.Distributor
{
	public class DistributorServiceTest
	{
		private Mock<IBankSelector> CreateSelector()
		{
			var mockRepository = new Mock<IBankSelector>();
			return mockRepository;
		}

		private Mock<IDistributionService> CreateDistributionService()
		{
			var mockRepository = new Mock<IDistributionService>();
			return mockRepository;
		}

		private IDistributorService CreateService(IMock<IBankSelector> bankSelector, IMock<IDistributionService> distributionService)
		{
			return new DistributorService(bankSelector.Object, distributionService.Object);
		}

		private void CheckNotExecuted(int result, Mock<IBankSelector> bankSelector, int selectorIndex, Mock<IDistributionService> distributionService)
		{
			Assert.Equal(0, result);
			bankSelector.Verify(selector => selector.SelectBank(null, out selectorIndex), Times.Never);

			IList<int> list = new List<int>();
			distributionService.Verify(distribution => distribution.Redistribute(It.IsAny<IList<int>>(), It.IsAny<int>(), out list), Times.Never);
		}

		[Fact]
		public void CountCycles_NullMemoryBanks()
		{
			int selectorIndex = 1;
			var bankSelector = CreateSelector();
			var distributionService = CreateDistributionService();

			var service = CreateService(bankSelector, distributionService);
			var result = service.CountCycles(null);

			CheckNotExecuted(result, bankSelector, selectorIndex, distributionService);
		}

		[Fact]
		public void CountCycles_EmptyMemoryBanks()
		{
			int selectorIndex = 1;
			var bankSelector = CreateSelector();
			var distributionService = CreateDistributionService();

			var service = CreateService(bankSelector, distributionService);
			var result = service.CountCycles(new List<int>());

			CheckNotExecuted(result, bankSelector, selectorIndex, distributionService);
		}

		[Fact]
		public void CountCycles_NoBankSelected()
		{
			var bankSelector = CreateSelector();

			int notUsed = 0;
			bankSelector.Setup(selector => selector.SelectBank(It.IsAny<IList<int>>(), out notUsed)).Returns(false);

			var distributionService = CreateDistributionService();

			var service = CreateService(bankSelector, distributionService);
			var result = service.CountCycles(new List<int>() { 1, 1, 3 });

			Assert.Equal(0, result);
		}

		[Fact]
		public void CountCycles_RedistributionFails()
		{
			var bankSelector = CreateSelector();

			int selectedBankIndex = 2;
			bankSelector.Setup(selector => selector.SelectBank(It.IsAny<IList<int>>(), out selectedBankIndex)).Returns(true);

			var distributionService = CreateDistributionService();
			IList<int> notUsed = new List<int>();
			distributionService.Setup(distribution => distribution.Redistribute(It.IsAny<IList<int>>(), 0, out notUsed)).Returns(false);

			var service = CreateService(bankSelector, distributionService);
			var result = service.CountCycles(new List<int>() { 1, 1, 3 });

			Assert.Equal(0, result);
		}

		[Fact]
		public void CountCycles_OnePass()
		{
			var bankSelector = CreateSelector();

			int selectedBankIndex = 2;
			bankSelector.Setup(selector => selector.SelectBank(It.IsAny<IList<int>>(), out selectedBankIndex)).Returns(true);

			var distributionService = CreateDistributionService();
			IList<int> distributionResult = new List<int>() { 2, 2, 1 };
			distributionService.Setup(distribution => distribution.Redistribute(It.IsAny<IList<int>>(), selectedBankIndex, out distributionResult)).Returns(true);

			var service = CreateService(bankSelector, distributionService);
			var result = service.CountCycles(new List<int>() { 2, 2, 1 });

			Assert.Equal(1, result);
		}
	}
}
