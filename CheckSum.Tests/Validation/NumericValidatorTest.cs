using CheckSum.Validation;
using Domain.Models;
using Xunit;

namespace CheckSum.Tests.Validation
{
	public class NumericValidatorTest
	{
		private INumericValidator CreateValidator()
		{
			return new NumericValidator();
		}

		private void CheckFailedValidation(NumericValidationResultEntity result)
		{
			Assert.NotNull(result);
			Assert.False(result.ValidationResult.IsValid);
			Assert.Single(result.ValidationResult.Messages);
			Assert.Equal(0, result.Input.Count);
		}

		private void CheckSuccessfulValidation(NumericValidationResultEntity result)
		{
			Assert.NotNull(result);
			Assert.True(result.ValidationResult.IsValid);
			Assert.Empty(result.ValidationResult.Messages);
		}

		[Fact]
		public void Validate_Null()
		{
			var validator = CreateValidator();

			var result = validator.Validate(null);

			CheckFailedValidation(result);
		}

		[Fact]
		public void Validate_Empty()
		{
			var validator = CreateValidator();

			var result = validator.Validate(string.Empty);

			CheckFailedValidation(result);
		}

		[Fact]
		public void Validate_Only_WhiteSpace()
		{
			var validator = CreateValidator();

			var result = validator.Validate("       ");

			CheckFailedValidation(result);
		}

		[Theory]
		[InlineData("5")]
		[InlineData("1")]
		[InlineData("3")]
		public void Validate_One_Number(string input)
		{
			var validator = CreateValidator();

			var result = validator.Validate(input);

			Assert.NotNull(result);
			Assert.True(result.ValidationResult.IsValid);
			Assert.Empty(result.ValidationResult.Messages);
			Assert.Equal(1, result.Input.Count);
			Assert.Equal(int.Parse(input), result.Input[0][0]);
		}

		[Theory]
		[InlineData("1, 2, 3")]
		[InlineData("1,    2, 3    ")]
		[InlineData("1      ,      2 ,   3   ")]
		public void Validate_Multiple_Rows_With_Spaces(string input)
		{
			var validator = CreateValidator();

			var result = validator.Validate(input);

			CheckSuccessfulValidation(result);

			Assert.Equal(3, result.Input.Count);
			Assert.Equal(1, result.Input[0][0]);
			Assert.Equal(2, result.Input[1][0]);
			Assert.Equal(3, result.Input[2][0]);
		}

		[Theory]
		[InlineData("5, 9, abcd, 10")]
		[InlineData("lorem")]
		public void Validate_Non_Numeric(string input)
		{
			var validator = CreateValidator();

			var result = validator.Validate(input);

			CheckFailedValidation(result);
		}


	}
}
