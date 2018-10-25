using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ReverseCaptcha.Tests
{
	public class ReverseCaptchaServiceTest
	{
		private IReverseCaptchaService CreateService()
		{
			var mockRepository = new Mock<ILogger<IReverseCaptchaService>>();
			ILogger<IReverseCaptchaService> logger = mockRepository.Object;

			IReverseCaptchaService service = new ReverseCaptchaService(logger);

			return service;
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(9)]
		public void Compute_One_Digit(int oneDigitInput)
		{
			IReverseCaptchaService service = CreateService();

			int result = service.Compute(oneDigitInput);

			Assert.Equal(0, result);
		}

		[Theory]
		[InlineData(1234)]
		[InlineData(1010)]
		public void No_Consecutive_Matching_Digits(int input)
		{
			IReverseCaptchaService service = CreateService();

			int result = service.Compute(input);

			Assert.Equal(0, result);
		}

		[Theory]
		[InlineData(15668, 6)]
		[InlineData(1122, 3)]
		public void Sums_Consecutive_Matching_Digits(int input, int expectedResult)
		{
			IReverseCaptchaService service = CreateService();

			int result = service.Compute(input);

			Assert.Equal(expectedResult, result);
		}

		[Theory]
		[InlineData(91212129, 9)]
		[InlineData(1111, 4)]
		public void Matches_Last_Digit_With_First(int input, int expectedResult)
		{
			IReverseCaptchaService service = CreateService();

			int result = service.Compute(input);

			Assert.Equal(expectedResult, result);
		}
	}
}
