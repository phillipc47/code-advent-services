using Domain.Models;

namespace CheckSum.Validators
{
	public interface INumericValidator
	{
		NumericValidationResultEntity Validate(string input);
	}
}
