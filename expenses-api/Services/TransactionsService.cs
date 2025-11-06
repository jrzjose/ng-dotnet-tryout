using expenses_api.Models;
using expenses_api.Dtos;
using expenses_api.Data;

namespace expenses_api.Services;

public class TransactionsService(AppDbContext context) : ITransactionsService
{
    public List<Transaction> GetAll(int userId)
    {
        var allTransactions = context.Transactions.Where(n => n.UserId == userId).ToList();
        return allTransactions;
    }

    public Transaction? GetById(int id)
    {
        var transactionDb = context.Transactions.FirstOrDefault(n => n.Id == id);
        return transactionDb;

    }

    public Transaction Add(TransactionDto transaction, int userId)
    {
        var newTransaction = new Transaction()
        {
            Amount = transaction.Amount,
            Type = transaction.Type,
            Category = transaction.Category,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = userId
        };

        context.Transactions.Add(newTransaction);
        context.SaveChanges();

        return newTransaction;
    }

    public Transaction? Update(int id, TransactionDto transaction)
    {
        var transactionDb = context.Transactions.FirstOrDefault(n => n.Id == id);
        if (transactionDb != null)
        {
            transactionDb.Type = transaction.Type;
            transactionDb.Amount = transaction.Amount;
            transactionDb.Category = transaction.Category;
            transactionDb.UpdatedAt = DateTime.UtcNow;

            context.Transactions.Update(transactionDb);
            context.SaveChanges();
        }

        return transactionDb;
    }

        public void Delete(int id)
    {
        var transactionDb = context.Transactions.FirstOrDefault(n => n.Id == id);
        if (transactionDb != null)
        {
            context.Transactions.Remove(transactionDb);
            context.SaveChanges();
        }
    }
}