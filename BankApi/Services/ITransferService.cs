using BankApi.Models;

namespace BankApi.Services;

public interface ITransferService
{
    bool ProcessTransfer(TransferRequest request);
}