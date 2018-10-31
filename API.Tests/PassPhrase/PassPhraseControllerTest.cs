using API.PassPhrase;
using API.PassPhrase.Models;
using Moq;
using Passphrase.Services;
using Xunit;

namespace API.Tests.PassPhrase
{
	public class PassPhraseControllerTest
	{
		private IPassPhraseService SetupService(bool expectedValidity)
		{
			var mockService = new Mock<IPassPhraseService>();
			mockService.Setup(service => service.IsValid(It.IsAny<string>())).Returns(expectedValidity);

			return mockService.Object;
		}

		private PassPhraseValidityResponse InvokeService(PassPhraseController controller, string input)
		{
			var controllerResponse = controller.IsValid(input);
			return ControllerTestHelper<PassPhraseValidityResponse>.Successful(controllerResponse);
		}

		private void CheckInvocation(PassPhraseController controller, string input = "", bool expectedValidity = false)
		{
			var response = InvokeService(controller, input);

			Assert.Equal(expectedValidity, response.Result);
			Assert.Equal(input, response.Input);
		}

		[Theory]
		[InlineData("something", true)]
		[InlineData("something else", false)]
		public void IsValid_Delegates_To_Service(string input, bool expectedValidity)
		{
			var service = SetupService(expectedValidity);

			var controller = new PassPhraseController(service);

			CheckInvocation(controller, input, expectedValidity);
		}

	}
}
