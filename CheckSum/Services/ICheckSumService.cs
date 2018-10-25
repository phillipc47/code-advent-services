using System.Collections.Generic;

namespace CheckSum.Services
{
	public interface ICheckSumService
	{
		int Compute(IList<IList<int>> input);
	}
}
