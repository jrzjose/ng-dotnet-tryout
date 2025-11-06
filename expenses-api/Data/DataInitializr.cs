using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using expenses_api.Models;

namespace expenses_api.Data;

public class DataInitializr 
{
    // private readonly AppDbContext dbContext;

    // public DataInitializr(AppDbContext context)
    // {
    //     dbContext = context;
    // }

    public void build() 
    {
        // Note: This sample requires the database to be created before running.
        AppDbContext dbContext = new AppDbContext();
        Console.WriteLine($"Database path: {dbContext.DbPath}.");

        bool exists = dbContext.Transactions.Any();
        if (!exists) {
            Console.WriteLine("Inserting seed transactions...");
            dbContext.Add(new Transaction { Id=1, Type="Sale",     Amount=10.1, Category="AAA", CreatedAt=DateTime.Today, UpdatedAt=DateTime.Today });
            dbContext.Add(new Transaction { Id=2, Type="Transfer", Amount=10.1, Category="BBB", CreatedAt=DateTime.Today, UpdatedAt=DateTime.Today });
            dbContext.Add(new Transaction { Id=3, Type="Payment",  Amount=10.1, Category="CCC", CreatedAt=DateTime.Today, UpdatedAt=DateTime.Today });
            dbContext.SaveChangesAsync();
        }
        else 
        {
            Console.WriteLine("Transactions already exists...");
        }
    }    
}