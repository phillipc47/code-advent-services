using System.Collections.Generic;
using CheckSum.Validators;
using Domain.Models.Validators;
using Moq;

namespace API.Tests
{
	public static class NumericValidatorTestHelper
	{
		public static Mock<INumericValidator> SetupValidator(IList<IList<int>> input, bool isValid = true)
		{
			var validationResult = new NumericValidationResultEntity
			{
				ValidationResult = { IsValid = isValid },
				Input = input
			};

			var mockRepository = new Mock<INumericValidator>();
			mockRepository.Setup(validator => validator.Validate(It.IsAny<string>())).Returns(validationResult);

			return mockRepository;
		}
	}
}
