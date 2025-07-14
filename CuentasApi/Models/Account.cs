namespace CuentasApi.Models;

public class Account
{

    public long Id { get; set; }
    public string? Name { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
}