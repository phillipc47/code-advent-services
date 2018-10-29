using System.Collections.Generic;
using CheckSum.Services;
using Xunit;

namespace CheckSum.Tests.Services
{
	public class CheckSumServiceTest
	{
		[Fact]
		public void Compute_Null_Input()
		{
			ICheckSumService service = new CheckSumService();

			var result = service.Compute(null);

			Assert.Equal(0, result);
		}

		[Fact]
		public void Compute_Null_Rows_Are_Not_Processed()
		{
			ICheckSumService service = new CheckSumService();

			IList<IList<int>> input = new List<IList<int>>()
			{
				new List<int>() {10, 7, 3}, // High: 10, Low: 3,  RowChecksum: 10 - 3 = 7
				null, 
				new List<int>() { 45, 19, 63 }    // High: 63, Low: 19,  RowChecksum: 63 - 19 = 44
			};

			var result = service.Compute(input);

			Assert.Equal(51, result);
		}

		[Fact]
		public void Compute_One_Entry_Rows()
		{
			ICheckSumService service = new CheckSumService();

			IList<IList<int>> input = new List<IList<int>>()
			{
				new List<int>() {5}, 
				new List<int>() {7}, 
				new List<int>() {2}
			};

			var result = service.Compute(input);

			Assert.Equal(0, result); 
		}

		[Fact]
		public void Compute_Performs_Sum_Of_High_Low_Differences()
		{
			ICheckSumService service = new CheckSumService();

			IList<IList<int>> input = new List<IList<int>>()
			{
				new List<int>() {5, 1, 9, 5}, // High: 9, Low: 1,  RowChecksum: 9 - 1 = 8
				new List<int>() {7, 5, 3}, // High: 7, Low: 3,  RowChecksum: 7 - 3 = 4
				new List<int>() {2, 4, 6, 8} // High: 8, Low: 2,  RowChecksum: 8 - 2 = 6
			};

			var result = service.Compute(input);  

			Assert.Equal(18, result); // 8 + 4 + 6
		}
	}
}
