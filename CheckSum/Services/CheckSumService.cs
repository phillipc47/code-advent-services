using System.Collections.Generic;

namespace CheckSum.Services
{
	public class CheckSumService : ICheckSumService
	{
		private class Data
		{
			public int Low { get; set; }
			public int High { get; set; }
		}

		private Data DetermineHighLow(IList<int> row)
		{
			int high = int.MinValue;
			int low = int.MaxValue;

			if (row == null)
			{
				return new Data() {High = 0, Low = 0};
			}

			foreach (var candidate in row)
			{
				if (candidate > high)
				{
					high = candidate;
				}

				if (candidate < low)
				{
					low = candidate;
				}
			}

			return new Data()
			{
				High = high,
				Low = low
			};
		}

		private int ProcessRow(IList<int> row)
		{
			Data data = DetermineHighLow(row);
			return data.High - data.Low;
		}

		public int Compute(IList<IList<int>> input)
		{
			int checksum = 0;

			if (input != null)
			{
				foreach (var row in input)
				{
					checksum += ProcessRow(row);
				}
			}

			return checksum;
		}
	}
}
