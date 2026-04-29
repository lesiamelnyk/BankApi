using Xunit;
using BankApi.Validators;

namespace BankApi.Tests;

public class TransferValidatorTests
{
    [Fact]
    public void ValidString_ShouldReturnTrue()
    {
        var input = "UA1234567890;UA9876543210;USD;100.50";

        var result = TransferValidator.ValidateString(input);

        Assert.True(result);
    }

    [Fact]
    public void InvalidIban_ShouldReturnFalse()
    {
        var input = "1234567890;UA9876543210;USD;100";

        var result = TransferValidator.ValidateString(input);

        Assert.False(result);
    }

    [Fact]
    public void InvalidCurrency_ShouldReturnFalse()
    {
        var input = "UA1234567890;UA9876543210;ABC;100";

        var result = TransferValidator.ValidateString(input);

        Assert.False(result);
    }

    [Fact]
    public void NegativeAmount_ShouldReturnFalse()
    {
        var input = "UA1234567890;UA9876543210;USD;-100";

        var result = TransferValidator.ValidateString(input);

        Assert.False(result);
    }

    [Fact]
    public void TooManyDecimals_ShouldReturnFalse()
    {
        var input = "UA1234567890;UA9876543210;USD;100.123";

        var result = TransferValidator.ValidateString(input);

        Assert.False(result);
    }
}