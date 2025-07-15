namespace CuentasApi.Models;

public enum TransactionType
{
    Income,
    Expense
}

public class Transaction
{
    public long Id { get; set; }
    public long AccountId { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public TransactionType Type { get; set; }
    public DateTime TransactionDate { get; set; }
}
