namespace BankApi.Models;

public class TransferRequest
{
    public string FromIBAN { get; set; }
    public string ToIBAN { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
}