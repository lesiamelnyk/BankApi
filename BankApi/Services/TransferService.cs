using BankApi.Models;
using BankApi.Data;
using BankApi.Validators;

namespace BankApi.Services;

public class TransferService : ITransferService
{
    private readonly AppDbContext _context;

    public TransferService(AppDbContext context)
    {
        _context = context;
    }

    public bool ProcessTransfer(TransferRequest request)
    {
        if (!TransferValidator.ValidateString($"{request.FromIBAN};{request.ToIBAN};{request.Currency};{request.Amount}"))
            return false;

        var from = _context.Accounts.FirstOrDefault(a => a.IBAN == request.FromIBAN);
        var to = _context.Accounts.FirstOrDefault(a => a.IBAN == request.ToIBAN);

        if (from == null || to == null) return false;
        if (from.Balance < request.Amount) return false;

        from.Balance -= request.Amount;
        to.Balance += request.Amount;

        _context.SaveChanges();

        return true;
    }
}