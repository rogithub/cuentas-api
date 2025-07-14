using Microsoft.AspNetCore.Mvc;
using CuentasApi.Models;

namespace CuentasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    // --- NUESTRA BASE DE DATOS EN MEMORIA (MOCK) ---
    // Usamos 'static' para que la lista persista entre diferentes peticiones HTTP.
    private static List<Account> _accounts = new List<Account>
    {
        new Account { Id = 1, Name = "Cuenta de Ahorros", Balance = 1500.75m, CreatedAt = DateTime.UtcNow },
        new Account { Id = 2, Name = "Tarjeta de Crédito", Balance = -450.50m, CreatedAt = DateTime.UtcNow.AddDays(-30) },
        new Account { Id = 3, Name = "Inversiones", Balance = 12500.00m, CreatedAt = DateTime.UtcNow.AddDays(-90) }
    };

    // GET: api/accounts
    [HttpGet]
    public ActionResult<IEnumerable<Account>> GetAccounts()
    {
        return Ok(_accounts);
    }

    // GET: api/accounts/5
    [HttpGet("{id}")]
    public ActionResult<Account> GetAccountById(long id)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);

        if (account == null)
        {
            return NotFound(); // Buena práctica: Devolver 404 si no se encuentra.
        }

        return Ok(account);
    }

    // POST: api/accounts
    [HttpPost]
    public ActionResult<Account> CreateAccount(Account newAccount)
    {
        // Simulamos que la BD asigna un nuevo ID.
        newAccount.Id = _accounts.Any() ? _accounts.Max(a => a.Id) + 1 : 1;
        newAccount.CreatedAt = DateTime.UtcNow;
        
        _accounts.Add(newAccount);

        // Buena práctica: Devolver 201 Created con la ubicación del nuevo recurso.
        return CreatedAtAction(nameof(GetAccountById), new { id = newAccount.Id }, newAccount);
    }

    // PUT: api/accounts/5
    [HttpPut("{id}")]
    public IActionResult UpdateAccount(long id, Account updatedAccount)
    {
        if (id != updatedAccount.Id)
        {
            return BadRequest(); // El ID de la URL debe coincidir con el del cuerpo.
        }

        var existingAccount = _accounts.FirstOrDefault(a => a.Id == id);
        if (existingAccount == null)
        {
            return NotFound();
        }

        // Actualizamos las propiedades.
        existingAccount.Name = updatedAccount.Name;
        existingAccount.Balance = updatedAccount.Balance;

        return NoContent(); // Buena práctica: Devolver 204 NoContent en una actualización exitosa.
    }

    // DELETE: api/accounts/5
    [HttpDelete("{id}")]
    public IActionResult DeleteAccount(long id)
    {
        var account = _accounts.FirstOrDefault(a => a.Id == id);
        if (account == null)
        { 
            return NotFound();
        }

        _accounts.Remove(account);

        return NoContent(); // Buena práctica: Devolver 204 NoContent en un borrado exitoso.
    }
}