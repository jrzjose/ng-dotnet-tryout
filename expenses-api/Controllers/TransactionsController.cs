using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;


using expenses_api.Models;
using expenses_api.Dtos;
using expenses_api.Data;
using expenses_api.Services;

namespace expenses_api.Controllers;

[EnableCors("AllowAll")]
[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ILogger<TransactionsController> _logger;
    private readonly ITransactionsService _transactionService;

    public TransactionsController(ILogger<TransactionsController> logger, ITransactionsService transactionService)
    {
        _logger = logger;
        _transactionService = transactionService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // List<Transaction> items = new List<Transaction>
        // {
        //     new Transaction { Id=1, Type="Sale",     Amount=10.1, Category="AAA", CreatedAt=DateTime.Today, UpdatedAt=DateTime.Today },
        //     new Transaction { Id=2, Type="Transfer", Amount=10.1, Category="BBB", CreatedAt=DateTime.Today, UpdatedAt=DateTime.Today },
        //     new Transaction { Id=3, Type="Payment",  Amount=10.1, Category="CCC", CreatedAt=DateTime.Today, UpdatedAt=DateTime.Today }
        // };
        // return Ok(items);

        var nameIdentifierClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(nameIdentifierClaim))
            return BadRequest("Could not get the user id");

        if (!int.TryParse(nameIdentifierClaim, out int userId))
            return BadRequest();

        var allTransactions = _transactionService.GetAll(userId);
        return Ok(allTransactions);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var transactionDb = _transactionService.GetById(id);
        if (transactionDb == null)
            return NotFound();

        return Ok(transactionDb);
    }

    [HttpPost]
    public IActionResult CreateTransaction([FromBody]TransactionDto payload)
    {
        var nameIdentifierClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(nameIdentifierClaim))
            return BadRequest("Could not get the user id");

        if(!int.TryParse(nameIdentifierClaim, out int userId))
            return BadRequest();

        var newTrasaction = _transactionService.Add(payload, userId);
        return Ok(newTrasaction);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTransaction(int id, [FromBody]TransactionDto payload)
    {
        var updatedTransaction = _transactionService.Update(id, payload);
        if (updatedTransaction == null)
            return NotFound();

        return Ok(updatedTransaction);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTransaction(int id)
    {
        _transactionService.Delete(id);
        return Ok();
    }

}
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-9.0&tabs=visual-studio