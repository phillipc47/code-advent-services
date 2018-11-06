using System.Collections.Generic;
using System.Linq;
using Distributor.Services.BankSelector;
using Domain.Helpers.Number;
using Xunit;

namespace Distributor.Tests.Services.BankSelector
{
	public class BankSelectorServiceTest
	{
		private IBankSelector CreateService()
		{
			return new BankSelectorService();
		}

		private void CheckNotFound(bool selectBankResult, int index)
		{
			Assert.False(selectBankResult);
			Assert.Equal(0, index);
		}

		private IList<int> CreateMemoryBanks(string commaDelimetedList)
		{
			return NumberHelper.CreateList(commaDelimetedList);
		}

		[Fact]
		public void SelectBank_EmptyList_ReturnsFalse()
		{
			var service = CreateService();

			var result = service.SelectBank(new List<int>(), out int index);

			CheckNotFound(result, index);
		}

		[Fact]
		public void SelectBank_NullList_ReturnsFalse()
		{
			var service = CreateService();

			var result = service.SelectBank(null, out int index);

			CheckNotFound(result, index);
		}

		[Theory]
		[InlineData("0, 2, 1, 7, 2, 1", 3)]
		[InlineData("12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11", 0)]
		[InlineData("44, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 45", 11)]
		[InlineData("0", 0)]
		[InlineData("0, 0, 0", 0)]
		[InlineData("100, 100, 100, 100, 100, 100, 100, 100", 0)]
		[InlineData("44, 100, 100, 100", 1)]
		public void SelectBank_FindsTheHighestBlockCountIndex(string commaDelimetedList, int expectedIndex)
		{
			var memoryBanks = CreateMemoryBanks(commaDelimetedList);
			var service = CreateService();

			var result = service.SelectBank(memoryBanks, out int index);

			Assert.True(result);
			Assert.Equal(expectedIndex, index);
		}
	}
}
