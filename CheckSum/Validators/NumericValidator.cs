using System.Collections.Generic;
using System.Linq;
using Domain.Models.Validators;

namespace CheckSum.Validators
{
	public class NumericValidator : INumericValidator
	{
		private bool ProcessRow(string row, out IList<int> result)
		{
			result = new List<int>();
			string[] rawValues = row.Contains(' ') ? row.Split(' ') : new string[ 1 ] { row };

			foreach (var rawValue in rawValues)
			{
				var currentValue = rawValue.Trim();
				if (string.IsNullOrWhiteSpace(currentValue))
				{
					continue;
				}

				if (int.TryParse(currentValue, out int numericValue))
				{
					result.Add(numericValue);
				}
				else
				{
					return false;
				}
			}

			return result.Count > 0;
		}

		private NumericValidationResultEntity CreateFailedResult(string message)
		{
			var result = new NumericValidationResultEntity();
			result.ValidationResult.Messages.Add( message );

			return result;
		}

		public NumericValidationResultEntity Validate(string input)
		{
			if( string.IsNullOrEmpty(input) )
			{
				return CreateFailedResult($"The specified input [{input}] did not contain any data");
			}

			IList<IList<int>> rows = new List<IList<int>>();
			var inputValues = input.Split(',').ToList();
			foreach (var row in inputValues)
			{
				if (!ProcessRow(row, out IList<int> numericRow))
				{
					return CreateFailedResult($"The row with values [{row}] contained non-numeric values");
				}

				rows.Add(numericRow);
			}

			return new NumericValidationResultEntity
			{
				Input = rows,
				ValidationResult = {IsValid = true}
			};
		}
	}
}
