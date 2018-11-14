using System.Collections.Generic;
using System.Linq;

namespace Domain.Helpers.Number
{
	public class NumberHelper 
	{
		public  static IList<int> CreateList(string commaDelimetedList)
		{
			return
				commaDelimetedList.Split(',')
					.Where(possibleNumber => int.TryParse(possibleNumber, out _))
					.Select(int.Parse)
					.ToList();
		}

		// Can be made with generics if the need arises
		public static IList<int> Flatten(IList<IList<int>> sourceLists)
		{
			IList<int> flattenedList = new List<int>();
			return sourceLists.Aggregate(flattenedList, (current, currentList) => current.Concat(currentList).ToList());
		}
	}
}
