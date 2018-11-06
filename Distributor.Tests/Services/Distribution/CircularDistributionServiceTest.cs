using System.Collections.Generic;
using System.Linq;
using Distributor.Services.Distribution;
using Domain.Helpers.Number;
using Xunit;

namespace Distributor.Tests.Services.Distribution
{
	public class CircularDistributionServiceTest
	{
		public IDistributionService CreateService()
		{
			return new CircularDistributionService();
		}

		private IList<int> CreateMemoryBanks(string commaDelimetedList)
		{
			return NumberHelper.CreateList(commaDelimetedList);
		}

		[Fact]
		public void Redistribute_NullList_DoesNothing()
		{
			var service = CreateService();

			var result = service.Redistribute(null, 0);

			Assert.False(result);
		}

		[Fact]
		public void Redistribute_EmptyList_DoesNothing()
		{
			var service = CreateService();

			var memoryBanks = new List<int>();
			var result = service.Redistribute(memoryBanks, 0);

			Assert.False(result);
			Assert.True(memoryBanks.Count == 0);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(3)]
		public void Redistribute_StartingIndex_OutOfRange_DoesNothing(int startingIndex)
		{
			var service = CreateService();

			var memoryBanks = new List<int>() { 1, 2, 3 };
			var result = service.Redistribute(memoryBanks, startingIndex);

			Assert.False(result);
			Assert.True(memoryBanks.Count == 3);
			Assert.Equal(1, memoryBanks[0]);
			Assert.Equal(2, memoryBanks[1]);
			Assert.Equal(3, memoryBanks[2]);
		}

		[Theory]
		[InlineData("1, 3, 0, 5", "3, 4, 1, 1", 3)]
		[InlineData("0, 2, 7, 0", "2, 4, 1, 2", 2)]
		[InlineData("0, 1, 2", "0, 1, 2", 0)]
		public void Redistribute_UpdatesValues_InList(string memoryBanksString, string expectedOutputString, int startingIndex)
		{
			var service = CreateService();
			var memoryBanks = CreateMemoryBanks(memoryBanksString);
			var expectedResultBanks = CreateMemoryBanks(expectedOutputString);

			var result = service.Redistribute(memoryBanks, startingIndex);

			Assert.True(result);

			var resultList = expectedResultBanks.Except(memoryBanks).ToList();
			Assert.True(resultList.Count == 0);
		}
	}
}
