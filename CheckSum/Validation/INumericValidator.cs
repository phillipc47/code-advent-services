using Domain.Models;

namespace CheckSum.Validation
{
	public interface INumericValidator
	{
		NumericValidationResultEntity Validate(string input);
	}
}
