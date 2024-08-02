public interface IBankRepository
{
    //find a user given userId
    public User? GetUserByUserId(int userId);
    //find all the users in the database
    public List<User> GetAllUsers();
    //find all the users of the account with accountId
    public List<User> GetUsersByAccountId(int accountId);
    //create an new user
    public User CreateUser(User user);
    //add an account to the user with userId
    public User AddAccountToUser(Account account, int userId);
    //delete a user with userId
    public User DeleteUserById(int userId);
    //delete an account with accountId from user with userId
    public int DeleteUserAccountByAccountId(int userId, int accountId);


    //find an account given accountId
    public Account? GetAccountByAccountId(int accountId);
    //find all the accounts in the database
    public List<Account> GetAllAccounts();
    //find all the accounts of user with userId
    public List<Account> GetAccountsByUserId(int userId);
    //find all the primary accounts of user with userId
    public List<Account> GetPrimaryAccountsByUserId(int userId);
    //create a new account
    public Account CreateAccount(Account account);
    //add a user to the account with accountId
    public Account AddUserToAccount(User user, int accountId);
    //delete an account with accountId
    public Account DeleteAccountById(int accountId);
    //delete an user with userId from account with accountId
    public int DeleteAccountUserByUserId(int accountId, int userId);


    //find a transaction given transactionId
    public Transaction? GetTransactionByTransactionId(int transactionId);
    //find all the transactions in the database
    public List<Transaction> GetAllTransactions();
    //find all the transactions with FromAccount having id fromAccountId
    public List<Transaction> GetTransactionsByFromAccount(int fromAccountId);
    //find all the transactions with ToAccount having id toAccountId
    public List<Transaction> GetTransactionsByToAccountId(int toAccountId);
    //create a transaction
    public Transaction CreateTransaction(Transaction transaction);
    //delete a transaction with transactionId
    public Transaction DeleteTransactionByTransactionId(int transactionId);

}