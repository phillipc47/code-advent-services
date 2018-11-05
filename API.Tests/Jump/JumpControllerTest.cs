using System.Collections.Generic;
using API.Jump;
using API.Jump.Models;
using CheckSum.Validators;
using Jump.Models;
using Jump.Services;
using Moq;
using Xunit;

namespace API.Tests.Jump
{
	public class JumpControllerTest
	{
		private Mock<IStepService> SetupService(IList<int> jumpOffsets, int resultExitSteps = 1)
		{
			var mockRepository = new Mock<IStepService>();
			mockRepository.Setup(service => service.DetermineExitSteps(jumpOffsets)).Returns(new StepResult(jumpOffsets) {ExitSteps = resultExitSteps});

			return mockRepository;
		}

		private JumpController CreateController(IMock<INumericValidator> validatorRepository, IMock<IStepService> serviceRepository)
		{
			return new JumpController(validatorRepository.Object, serviceRepository.Object);
		}

		private ExitStepResponse SuccessfulDetermineExitSteps(JumpController controller, string input)
		{
			var controllerResult = controller.DetermineExitSteps(input);
			return ControllerTestHelper<ExitStepResponse>.Successful(controllerResult);
		}

		[Fact]
		public void Exit_Steps_Performs_Validation()
		{
			var input = string.Empty;
			var validatorRepository = NumericValidatorTestHelper.SetupValidator(null, false);
			var serviceRepository = SetupService(null);

			var controller = CreateController(validatorRepository, serviceRepository);
			var controllerResult = controller.DetermineExitSteps(input);

			ControllerTestHelper<ExitStepResponse>.BadRequestResult(controllerResult);

			validatorRepository.Verify(validator => validator.Validate(input), Times.Once);
			serviceRepository.Verify(service => service.DetermineExitSteps(null), Times.Never);
		}

		[Fact]
		public void Exit_Steps_Delegates_To_Service()
		{
			var input = "6 5 4, 3 2 1";
			IList<IList<int>> numericInput = new List<IList<int>>
			{
				new List<int>() { 6, 5, 4 },
				new List<int>() { 3, 2, 1 }
			};
			IList<int> flattenedInput = new List<int>() {6, 5, 4, 3, 2, 1};

			var validatorRepository = NumericValidatorTestHelper.SetupValidator(numericInput);
			const int expectedExitSteps = 337;
			var serviceRepository = SetupService(flattenedInput, expectedExitSteps);

			var controller = CreateController(validatorRepository, serviceRepository);
			var result = SuccessfulDetermineExitSteps(controller, input);

			Assert.Equal(expectedExitSteps, result.Result);
			Assert.Equal(input, result.Input);
			Assert.Equal(flattenedInput, result.InputRows);

			validatorRepository.Verify(validator => validator.Validate(input), Times.Once);
			serviceRepository.Verify(service => service.DetermineExitSteps(flattenedInput), Times.Once);
		}
	}
}
