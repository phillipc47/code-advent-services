using System.Collections.Generic;

namespace Domain.Models
{
	public class NumericValidationResultEntity
	{
		public ValidationResultEntity ValidationResult { get; set; } = new ValidationResultEntity();
		public IList<IList<int>> Input { get; set; } = new List<IList<int>>();
	}
}
