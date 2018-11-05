using System.Collections.Generic;
using API.CheckSum;
using API.CheckSum.Models;
using CheckSum.Services;
using CheckSum.Validators;
using Moq;
using Xunit;

namespace API.Tests.CheckSum
{
	public class CheckSumControllerTest
	{
		private Mock<ICheckSumService> SetupService(IList<IList<int>> input, int calculationResult = 78)
		{
			var mockRepository = new Mock<ICheckSumService>();
			mockRepository.Setup(service => service.Compute(input)).Returns(calculationResult);

			return mockRepository;
		}

		private CheckSumResponse SuccessfulCompute(CheckSumController controller, string input)
		{
			var controllerResult = controller.Calculate(input);
			return ControllerTestHelper<CheckSumResponse>.Successful(controllerResult);
		}

		private CheckSumController CreateController(IMock<INumericValidator> validatorRepository, IMock<ICheckSumService> serviceRepository)
		{
			return new CheckSumController(validatorRepository.Object, serviceRepository.Object);
		}


		[Fact]
		public void Calculate_Perfoms_Validation()
		{
			var input = string.Empty;
			var validatorRepository = NumericValidatorTestHelper.SetupValidator(null, false);
			var serviceRepository = SetupService(null);

			var controller = CreateController(validatorRepository, serviceRepository);
			var controllerResult = controller.Calculate(input);

			ControllerTestHelper<CheckSumResponse>.BadRequestResult(controllerResult);

			validatorRepository.Verify( validator => validator.Validate(input), Times.Once );
			serviceRepository.Verify( service => service.Compute(null), Times.Never);
		}

		[Fact]
		public void Calculate_Delegates_To_Service()
		{
			var input = "1 2 3, 5 6 8";
			IList<IList<int>> numericInput = new List<IList<int>>
			{
				new List<int>() { 1, 2, 3 },
				new List<int>() { 5, 6, 9 }
			};

			var validatorRepository = NumericValidatorTestHelper.SetupValidator(numericInput);
			const int expectedResult = 378929;
			var serviceRepository = SetupService(numericInput, expectedResult);

			var controller = CreateController(validatorRepository, serviceRepository);
			var result = SuccessfulCompute(controller, input);

			Assert.Equal(expectedResult, int.Parse(result.Result));
			Assert.Equal(input, result.Input);
			Assert.Equal(numericInput, result.InputRows);

			validatorRepository.Verify(validator => validator.Validate(input), Times.Once);
			serviceRepository.Verify(service => service.Compute(numericInput), Times.Once);
		}
	}
}
