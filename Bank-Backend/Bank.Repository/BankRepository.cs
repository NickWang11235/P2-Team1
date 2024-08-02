using Microsoft.EntityFrameworkCore;

public class BankRepository : IBankRepository
{

    private BankContext _bankContext;

    public BankRepository(string connectionString)
    {
        DbContextOptions<BankContext> options;
        options = new DbContextOptionsBuilder<BankContext>()
            .UseSqlServer(connectionString)
            .Options;
        _bankContext = new BankContext(options);
    }
}