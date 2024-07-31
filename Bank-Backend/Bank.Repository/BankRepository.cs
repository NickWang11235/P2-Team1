using Microsoft.EntityFrameworkCore;

public class BankRepository : IBankRepository
{

    private BankContext _bankContext;

    public BankRepository(string connectionString)
    {
        _bankContext = new BankContext(new DbContextOptionsBuilder<BankContext>().UseSqlServer(connectionString).Options);
    }


}