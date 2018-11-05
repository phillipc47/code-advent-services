using System.Collections.Generic;

namespace Domain.Models.Validators
{
	public class ValidationResultEntity
	{
		public bool IsValid { get; set; } = false;
		public List<string> Messages { get; set; } = new List<string>();
	}
}
