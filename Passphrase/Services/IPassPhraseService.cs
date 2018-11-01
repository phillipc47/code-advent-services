namespace Passphrase.Services
{
	public interface IPassPhraseService
	{
		bool IsValid(string candidate);
	}
}
