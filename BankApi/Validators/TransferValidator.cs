using System.Text.RegularExpressions;

namespace BankApi.Validators;

public static class TransferValidator
{
    public static bool ValidateString(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var parts = input.Split(';');
        if (parts.Length != 4)
            return false;

        var from = parts[0];
        var to = parts[1];
        var currency = parts[2];
        var amountStr = parts[3];

        // IBAN
        if (!IsValidIban(from) || !IsValidIban(to))
            return false;

        // Currency (ISO 4217 basic check)
        var validCurrencies = new[] { "USD", "EUR", "UAH" };
        if (!validCurrencies.Contains(currency.ToUpper()))
            return false;

        // Amount
        if (!decimal.TryParse(
        amountStr,
        System.Globalization.NumberStyles.AllowDecimalPoint,
        System.Globalization.CultureInfo.InvariantCulture,
        out var amount))
            return false;

        if (amount <= 0)
            return false;

        var partsAmount = amountStr.Split('.');
        if (partsAmount.Length == 2 && partsAmount[1].Length > 2)
            return false;

        return true;
    }

    private static bool IsValidIban(string iban)
    {
        if (iban.Length < 10)
            return false;

        if (!char.IsLetter(iban[0]) || !char.IsLetter(iban[1]))
            return false;

        return iban.Skip(2).All(char.IsDigit);
    }
}