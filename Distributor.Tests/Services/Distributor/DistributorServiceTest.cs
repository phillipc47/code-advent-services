using System.Collections.Generic;
using Distributor.Services.BankSelector;
using Distributor.Services.Distribution;
using Distributor.Services.Distributor;
using Domain.Helpers.Number;
using Moq;
using Xunit;

namespace Distributor.Tests.Services.Distributor
{
	public class DistributorServiceTest
	{
		private Mock<IBankSelector> CreateSelector(int expectedIndex, bool expectedReturnValue)
		{
			var mockRepository = new Mock<IBankSelector>();

			mockRepository.Setup(selector => selector.SelectBank(It.IsAny<IList<int>>(), out expectedIndex)).Returns(expectedReturnValue);

			return mockRepository;
		}

		private Mock<IDistributionService> CreateDistributionService(bool expectedReturnValue)
		{
			var mockRepository = new Mock<IDistributionService>();

			//mockRepository.Setup(service => service.Redistribute(It.IsAny<IList<int>>(), It.IsAny<int>())).Returns(expectedReturnValue);

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
			//distributionService.Verify(distribution => distribution.Redistribute(It.IsAny<IList<int>>(), It.IsAny<int>()), Times.Never);
		}

		[Fact]
		public void foo()
		{
			var selector = CreateSelector(1, true);
			var distributionService = CreateDistributionService(true);

			var service = CreateService(selector, distributionService);
			//service.CountCycles()

		}

		[Fact]
		public void CountCycles_NullMemoryBanks()
		{
			int selectorIndex = 1;
			var bankSelector = CreateSelector(selectorIndex, true);
			var distributionService = CreateDistributionService(true);

			var service = CreateService(bankSelector, distributionService);
			var result = service.CountCycles(null);

			CheckNotExecuted(result, bankSelector, selectorIndex, distributionService);
		}

		[Fact]
		public void CountCycles_EmptyMemoryBanks()
		{
			int selectorIndex = 1;
			var bankSelector = CreateSelector(selectorIndex, true);
			var distributionService = CreateDistributionService(true);

			var service = CreateService(bankSelector, distributionService);
			var result = service.CountCycles(new List<int>());

			CheckNotExecuted(result, bankSelector, selectorIndex, distributionService);
		}

		//TODO: Start here
		////[Theory]
		////[InlineData("1, 2, 3, 4", 5)]
		////[InlineData("4")]
		//[Fact]
		//public void CountCycles_Delegates()
		//{
		//	int selectorIndex = 2;
		//	var bankSelector = CreateSelector(selectorIndex, true);
		//	var distributionService = CreateDistributionService(true);

		//	var service = CreateService(bankSelector, distributionService);

		//	var memoryBanks = NumberHelper.CreateList(commaDelimetedList);
		//	var result = service.CountCycles(memoryBanks);

		//	Assert.Equal(expectedCycleCount, result);
		//}
	}
}
