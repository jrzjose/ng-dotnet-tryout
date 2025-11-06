using expenses_api.Models;
using Microsoft.EntityFrameworkCore;

namespace expenses_api.Data;

public class AppDbContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public string DbPath { get; }

    public AppDbContext() 
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        //DbPath = System.IO.Path.Join(path, "/dblite/mylitle.db");
        DbPath = "/workspace/expenses-api/dblite/mylitle.db";
        // https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli#install-entity-framework-core
    }

    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        //DbPath = System.IO.Path.Join(path, "/dblite/mylitle.db");
        DbPath = "/workspace/expenses-api/dblite/mylitle.db";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}