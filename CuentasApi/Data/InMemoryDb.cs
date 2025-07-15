using CuentasApi.Models;
namespace CuentasApi.Data;

public static class InMemoryDb
{
    public static List<Transaction> Transactions = new List<Transaction>
    {
        new Transaction { Id = 1, AccountId = 1, Amount = 100.00m, Description = "Salario", Type = TransactionType.Income, TransactionDate = DateTime.UtcNow.AddDays(-10) },
        new Transaction { Id = 2, AccountId = 1, Amount = 25.50m, Description = "Supermercado", Type = TransactionType.Expense, TransactionDate = DateTime.UtcNow.AddDays(-8) },
        new Transaction { Id = 3, AccountId = 2, Amount = 50.00m, Description = "Pago de tarjeta", Type = TransactionType.Expense, TransactionDate = DateTime.UtcNow.AddDays(-5) },
        new Transaction { Id = 4, AccountId = 3, Amount = 1200.00m, Description = "Venta de acciones", Type = TransactionType.Income, TransactionDate = DateTime.UtcNow.AddDays(-2) }
    };
    

    // --- NUESTRA BASE DE DATOS EN MEMORIA (MOCK) ---
    // Usamos 'static' para que la lista persista entre diferentes peticiones HTTP.
    public static List<Account> Accounts = new List<Account>
    {
        new Account { Id = 1, Name = "Cuenta de Ahorros", Balance = 1500.75m, CreatedAt = DateTime.UtcNow },
        new Account { Id = 2, Name = "Tarjeta de Crédito", Balance = -450.50m, CreatedAt = DateTime.UtcNow.AddDays(-30) },
        new Account { Id = 3, Name = "Inversiones", Balance = 12500.00m, CreatedAt = DateTime.UtcNow.AddDays(-90) }
    };

}