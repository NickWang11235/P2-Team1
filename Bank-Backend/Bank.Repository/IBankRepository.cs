public interface IBankRepository
{
    //find a user given userId
    public User? GetUserByUserId(int userId);
    //return all the users in the database
    public List<User> GetAllUsers();
    //find all the users of the account with accountId
    public List<User> GetUsersByAccountId(int accountId);


    //find an account given accountId
    public Account? GetAccountByAccountId(int accountId);
    //return all the accounts in the database
    public List<Account> GetAllAccounts();
    //find all the accounts of user with userId
    public List<Account> GetAccountsByUserId(int userId);
    //find all the primary accounts of user with userId
    public List<Account> GetPrimaryAccountsByUserId(int userId);


    //find a transaction given transactionId
    public Transaction? GetTransactionByTransactionId(int transactionId);
    //return all the transactions in the database
    public List<Transaction> GetAllTransactions();
    //find all the transactions with FromAccount having id fromAccountId
    public List<Transaction> GetTransactionsByFromAccount(int fromAccountId);
    //find all the transactions with ToAccount having id toAccountId
    public List<Transaction> GetTransactionsByToAccountId(int toAccountId);

}