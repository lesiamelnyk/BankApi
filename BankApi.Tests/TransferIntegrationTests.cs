using Xunit;
using BankApi.Services;
using BankApi.Models;
using BankApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Tests;

public class TransferIntegrationTests
{
    [Fact]
    public void Transfer_ShouldChangeBalances()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;

        using var context = new AppDbContext(options);

        var user = new User { Username = "test" };
        context.Users.Add(user);

        var acc1 = new Account { IBAN = "UA1111111111", Balance = 1000, User = user };
        var acc2 = new Account { IBAN = "UA2222222222", Balance = 500, User = user };

        context.Accounts.AddRange(acc1, acc2);
        context.SaveChanges();

        var service = new TransferService(context);

        var request = new TransferRequest
        {
            FromIBAN = acc1.IBAN,
            ToIBAN = acc2.IBAN,
            Currency = "USD",
            Amount = 100
        };

        var result = service.ProcessTransfer(request);

        Assert.True(result);
        Assert.Equal(900, acc1.Balance);
        Assert.Equal(600, acc2.Balance);
    }
}