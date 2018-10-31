using System.Collections.Generic;

namespace Passphrase.Services
{
	public class PassPhraseService : IPassPhraseService
	{
		public bool IsValid(string candidate)
		{
			ISet<string> encounteredWords = new HashSet<string>();

			if (string.IsNullOrEmpty(candidate))
			{
				return false;
			}

			var words = candidate.Split(' ');
			foreach (var word in words)
			{
				string candidateWord = word.Trim();
				if (candidateWord == string.Empty)
				{
					continue;
				}

				if (encounteredWords.Contains(word))
				{
					return false;
				}

				encounteredWords.Add(candidateWord);
			}

			return true;
		}
	}
}
