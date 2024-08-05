namespace BankBackend.Repository;

using Microsoft.EntityFrameworkCore;

using BankBackend.Models;

public class BankContext : DbContext
{

    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public BankContext(DbContextOptions<BankContext> options) : base(options) { }

}