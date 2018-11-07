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

		private IList<int> Redistribute(IDistributionService service, IList<int> memoryBanks, int startingIndex, bool expectSuccess = true)
		{
			var result = service.Redistribute(memoryBanks, startingIndex, out var listResult);

			if (expectSuccess)
			{
				Assert.True(result);
				Assert.NotNull(listResult);
				Assert.True(memoryBanks.Count == listResult.Count);
			}
			else
			{
				Assert.False(result);
				Assert.NotNull(listResult);
				Assert.True(listResult.Count == 0);
			}

			return listResult;
		}

		private void CheckList(IList<int> list, IList<int> expectedResults)
		{
			Assert.True(list.Count == expectedResults.Count);

			for (int index = 0; index < list.Count; index++)
			{
				Assert.True(list[index] == expectedResults[index]);
			}
		}

		[Fact]
		public void Redistribute_NullList_DoesNothing()
		{
			var service = CreateService();

			Redistribute(service, null, 0, false);
		}

		[Fact]
		public void Redistribute_EmptyList_DoesNothing()
		{
			var service = CreateService();

			var memoryBanks = new List<int>();
			Redistribute(service, memoryBanks, 0, false);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(3)]
		public void Redistribute_StartingIndex_OutOfRange_DoesNothing(int startingIndex)
		{
			var service = CreateService();

			var memoryBanks = new List<int>() { 1, 2, 3 };
			Redistribute(service, memoryBanks, startingIndex, false);

			CheckList(memoryBanks, new List<int>() {1, 2, 3});
		}

		[Theory]
		[InlineData("1, 3, 0, 5", "3, 4, 1, 1", 3)]
		[InlineData("0, 2, 7, 0", "2, 4, 1, 2", 2)]
		[InlineData("0, 1, 2", "0, 1, 2", 0)]
		public void Redistribute_Creates_NewList(string memoryBanksString, string expectedOutputString, int startingIndex)
		{
			var service = CreateService();
			var memoryBanks = CreateMemoryBanks(memoryBanksString);
			var expectedResultBanks = CreateMemoryBanks(expectedOutputString);

			var result = Redistribute(service, memoryBanks, startingIndex);

			CheckList(memoryBanks, CreateMemoryBanks(memoryBanksString));
			CheckList(result, expectedResultBanks);
		}
	}
}
