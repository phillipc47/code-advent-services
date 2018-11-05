using Domain.Models.Validators;

namespace CheckSum.Validators
{
	public interface INumericValidator
	{
		NumericValidationResultEntity Validate(string input);
	}
}
