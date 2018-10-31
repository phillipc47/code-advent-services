using API.ReverseCaptcha;
using API.ReverseCaptcha.Models;
using Moq;
using ReverseCaptcha;
using Xunit;

namespace API.Tests.ReverseCaptcha
{
    public class ReverseCaptchaControllerTest
    {
	    private ReverseCaptchaResponse SuccessfulCalculate(ReverseCaptchaController controller, string input)
	    {
		    var controllerResult = controller.Calculate(input);
		    return ControllerTestHelper<ReverseCaptchaResponse>.Successful(controllerResult);
	    }

	    [Fact]
	    public void Calculate_Delegates_To_Service()
	    {
			//Given
		    const int expectedCalculationResult = 5;
		    const string expectedCalculationInput = "115";

			var mockRepository = new Mock<IReverseCaptchaService>();
		    mockRepository.Setup(service => service.Compute(It.IsAny<int>())).Returns(expectedCalculationResult);

			//When
		    var controller = new ReverseCaptchaController(mockRepository.Object);
		    var revereCaptchaResult = SuccessfulCalculate(controller, expectedCalculationInput);

			//Then
		    Assert.Equal(expectedCalculationInput, revereCaptchaResult.Input);
			var calculationResult = int.Parse(revereCaptchaResult.Result);

			Assert.Equal(expectedCalculationResult, calculationResult);
	    }

	    [Theory]
		[InlineData("some string")]
	    [InlineData("abcdefg")]
	    [InlineData(null)]
		public void Calculate_Validates_Input(string controllerInput)
	    {
		    var mockRepository = new Mock<IReverseCaptchaService>();

			var controller = new ReverseCaptchaController(mockRepository.Object);

		    var controllerResult = controller.Calculate(controllerInput);

			ControllerTestHelper<ReverseCaptchaResponse>.BadRequestResult(controllerResult);
	    }
    }
}
