using expenses_api.Models;
using expenses_api.Dtos;

namespace expenses_api.Services;

public interface ITransactionsService
{
    List<Transaction> GetAll(int userId);
    Transaction? GetById(int id);
    Transaction Add(TransactionDto transaction, int userId);
    Transaction? Update(int id, TransactionDto transaction);
    void Delete(int id);
}