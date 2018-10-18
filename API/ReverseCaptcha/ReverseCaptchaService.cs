using System.Linq;
using Microsoft.Extensions.Logging;

namespace API.ReverseCaptcha
{
	public class ReverseCaptchaService : IReverseCaptchaService
	{
		private ILogger<IReverseCaptchaService> Logger { get; }

		public ReverseCaptchaService(ILogger<IReverseCaptchaService> logger)
		{
			Logger = logger;
		}

		public int Compute(int input)
		{
			var digits = input.ToString().Select(digit => int.Parse((digit.ToString()))).ToArray();

			if (!digits.Any() || digits.Length == 0)
			{
				return 0;
			}

			int sum = 0;
			for (int currentPosition = 0, nextPosition = 1; currentPosition < digits.Length; currentPosition++, nextPosition++)
			{
				if (nextPosition >= digits.Length)
				{
					// Circular list, loop around to the first number
					nextPosition = 0;
				}

				if (digits[currentPosition] == digits[nextPosition])
				{
					sum += digits[currentPosition];
				}
			}

			return sum;
		}
	}
}
