using Passphrase.Services;
using Xunit;

namespace Passphrase.Tests.Services
{
	public class PassPhraseServiceTest
	{

		private IPassPhraseService CreateService()
		{
			return new PassPhraseService();
		}

		[Fact]
		public void Null_Is_Not_Valid()
		{
			var service = CreateService();

			var result = service.IsValid(null);

			Assert.False(result);
		}

		[Fact]
		public void Empty_Is_Not_Valid()
		{
			var service = CreateService();

			var result = service.IsValid(string.Empty);

			Assert.False(result);
		}

		[Theory]
		[InlineData("word word")]
		[InlineData("someword someword anotherword")]
		[InlineData("aa bb cc dd aa")]
		public void Repeating_Words_Are_Not_Valid(string passPhrase)
		{
			var service = CreateService();

			var result = service.IsValid(passPhrase);

			Assert.False(result);
		}

		[Theory]
		[InlineData("word")]
		[InlineData("Lorem ipsum dolor sit amet, consectetur")]
		[InlineData("aa aaa aaaa aaaaa")]
		public void No_Repeating_Words_Are_Valid(string passPhrase)
		{
			var service = CreateService();

			var result = service.IsValid(passPhrase);

			Assert.True(result);
		}
	}
}
