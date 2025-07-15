using Microsoft.AspNetCore.Mvc;
using CuentasApi.Models;
using CuentasApi.Data;

namespace CuentasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private static List<Transaction> _transactions = InMemoryDb.Transactions;

    // GET: api/transactions
    [HttpGet]
    public ActionResult<IEnumerable<Transaction>> GetTransactions()
    {
        return Ok(_transactions);
    }

    // GET: api/transactions/5
    [HttpGet("{id}")]
    public ActionResult<Transaction> GetTransactionById(long id)
    { 
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);

        if (transaction == null)
        {
            return NotFound();
        }

        return Ok(transaction);
    }

    // POST: api/transactions
    [HttpPost]
    public ActionResult<Transaction> CreateTransaction(Transaction newTransaction)
    {
        newTransaction.Id = _transactions.Any() ? _transactions.Max(t => t.Id) + 1 : 1;
        newTransaction.TransactionDate = DateTime.UtcNow;
        
        _transactions.Add(newTransaction);

        return CreatedAtAction(nameof(GetTransactionById), new { id = newTransaction.Id }, newTransaction);
    }

    // PUT: api/transactions/5
    [HttpPut("{id}")]
    public IActionResult UpdateTransaction(long id, Transaction updatedTransaction)
    {
        if (id != updatedTransaction.Id)
        {
            return BadRequest();
        }

        var existingTransaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (existingTransaction == null)
        {
            return NotFound();
        }

        existingTransaction.AccountId = updatedTransaction.AccountId;
        existingTransaction.Amount = updatedTransaction.Amount;
        existingTransaction.Description = updatedTransaction.Description;
        existingTransaction.Type = updatedTransaction.Type;

        return NoContent();
    }

    // DELETE: api/transactions/5
    [HttpDelete("{id}")]
    public IActionResult DeleteTransaction(long id)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == id);
        if (transaction == null)
        { 
            return NotFound();
        }

        _transactions.Remove(transaction);

        return NoContent();
    }
}
