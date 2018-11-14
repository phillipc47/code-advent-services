using System.Collections.Generic;
using API.Distributor;
using API.Distributor.Models;
using CheckSum.Validators;
using Distributor.Services.Distributor;
using Domain.Models.Validators;
using Moq;
using Xunit;

namespace API.Tests.Distributor
{
	public class DistributorControllerServiceTest
	{
		private Mock<INumericValidator> CreateValidator(bool expectedResult = false)
		{
			var validatorRepository = new Mock<INumericValidator>();
			validatorRepository.Setup(validator => validator.Validate(It.IsAny<string>())).Returns(new NumericValidationResultEntity() {ValidationResult = new ValidationResultEntity() {IsValid = expectedResult } } );

			return validatorRepository;
		}

		private Mock<IDistributorService> CreateDistributor(int expectedCycles = 0)
		{
			var distributorServiceRepository = new Mock<IDistributorService>();
			distributorServiceRepository.Setup(distributor => distributor.CountCycles(It.IsAny<IList<int>>())).Returns(expectedCycles);

			return distributorServiceRepository;
		}

		private DistributorController CreateController(IMock<INumericValidator> validatorRepository, IMock<IDistributorService> distributorServiceRepository)
		{
			return new DistributorController(validatorRepository.Object, distributorServiceRepository.Object);
		}

		private void VerifyValidatorCalled(Mock<INumericValidator> validatorRepository, Times times)
		{
			validatorRepository.Verify(validator => validator.Validate(It.IsAny<string>()), times);
		}

		private void VerifyDistributorCalled(Mock<IDistributorService> serviceRepository, Times times)
		{
			serviceRepository.Verify(distributor => distributor.CountCycles(It.IsAny<IList<int>>()), times);
		}

		[Fact]
		public void CountCycles_ValidationFails()
		{
			var validator = CreateValidator();
			var distributor = CreateDistributor();

			var controller = CreateController(validator, distributor);
			var controllerResult = controller.CountCycles("Some Input that the validator does not like");

			ControllerTestHelper<CycleCountResponse>.BadRequestResult(controllerResult);
			VerifyValidatorCalled(validator, Times.Once());
			VerifyDistributorCalled(distributor, Times.Never());
		}

		[Theory]
		[InlineData("1, 2, 3, 4", 12)]
		[InlineData("", 7)]
		public void CountCycles_DelegatesToDistributor(string input, int expectedCycleCount)
		{
			var validator = CreateValidator(true);
			var distributor = CreateDistributor(expectedCycleCount);

			var controller = CreateController(validator, distributor);
			var controllerResult = controller.CountCycles(input);

			var cycleCountResponse = ControllerTestHelper<CycleCountResponse>.Successful(controllerResult);
			Assert.Equal(input, cycleCountResponse.Input);
			Assert.Equal(expectedCycleCount, cycleCountResponse.Result);

			VerifyValidatorCalled(validator, Times.Once());
			VerifyDistributorCalled(distributor, Times.Once());
		}
	}
}
