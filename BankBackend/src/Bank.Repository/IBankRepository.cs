namespace BankBackend.Repository;

using BankBackend.Models;

/// <summary>
/// 
/// </summary>
public interface IBankRepository
{
    /// <summary>
    /// find a user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>the user with the Id, null if user does not exist</returns>
    public User? GetUserByUserId(int userId);

    /// <summary>
    /// find a user with <c>username</c>
    /// </summary>
    /// <param name="username"></param>
    /// <returns>the user with the username, null if user does not exist</returns>
    public User? GetUserByUsername(string username);

    /// <summary>
    /// find all existing users in the database
    /// </summary>
    /// <returns>a list containing all existing users, empty list if non exists</returns>
    public List<User> GetAllUsers();

    /// <summary>
    /// find all the accounts of the user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>a list containing all the accounts, null if user does not exist</returns>
    public List<Account>? GetAccountsByUserId(int userId);


    /// <summary>
    /// uploads the user to database
    /// </summary>
    /// <param name="user"></param>
    /// <returns>the user that was just created</returns>
    public User CreateUser(User user);

    /// <summary>
    /// updates the password of user with <c>userId</c> to <c>password</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns>the updated user with the new password, null if the user does not exist</returns>
    public User? UpdatePassword(int userId, string password);

    /// <summary>
    /// updates the username of the user with <c>userId</c> to <c>username</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="name"></param>
    /// <returns>the updated user with the new username, null if the user does not exist</returns>
    public User? UpdateUsername(int userId, string username);

    /// <summary>
    /// adds account with <c>accountId</c> to the account list of the user with <c>userId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="userId"></param>
    /// <returns>the updated user with the added account, null if the user does not exist</returns>
    public User? AddAccountToUser(int accountId, int userId);


    /// <summary>
    /// deletes the user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>the user that was just deleted, null if the user does not exist</returns>
    public User? DeleteUserById(int userId);

    /// <summary>
    /// deletes the account with <c>accountId</c> from the user with <c>userId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountId"></param>
    /// <returns>the account that was just deleted, null if user does not exist, or the user does not have an account with <c>accountId</c></returns>
    public Account? DeleteUserAccountByAccountId(int userId, int accountId);



    /// <summary>
    /// find an account with <c>accountId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns>the account with <c>accountId</c>, null if account does not exist</returns>
    public Account? GetAccountByAccountId(int accountId);

    /// <summary>
    /// find all existing accounts in the database
    /// </summary>
    /// <returns>list containing all existing accounts, empty list if non exists</returns>
    public List<Account> GetAllAccounts();

    /// <summary>
    /// find all the users of the account with <c>accountId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns>a list containing all the users, null if account does not exist</returns>
    public List<User>? GetUsersByAccountId(int accountId);

    /// <summary>
    /// find all the accounts that the user with <c>accountId</c> is a primary user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>a list containing all the primary accounts, null if user does not exist</returns>
    public List<Account>? GetPrimaryAccountsByUserId(int userId);

    /// <summary>
    /// uploads the account to database
    /// </summary>
    /// <param name="account"></param>
    /// <returns>the account that was just created</returns>
    public Account CreateAccount(Account account);

    /// <summary>
    ///  updates the balance of the account with <c>accountId</c> to <c>balance</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="balance"></param>
    /// <returns>the updated account with the new balance, null if the account does not exist</returns>
    public Account? UpdateBalance(int accountId, double balance);

    /// <summary>
    /// replaces the primary user of the account with <c>accountId</c> with <c>userId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="userId"></param>
    /// <returns>the updated account with the new primary user, null if account doesnot exist or if user does not exist</returns>
    public Account? UpdatePrimaryUser(int accountId, int userId);

    /// <summary>
    /// adds user with <c>userId</c> to the account list of the account with <c>accountId</c>
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="accountId"></param>
    /// <returns>the updated account with the new user, null if account does not exist or if user does not exist</returns>
    public Account? AddUserToAccount(int userId, int accountId);

    /// <summary>
    /// deletes the account with <c>accountId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns>the account that was just deleted, null if the account does not exist</returns>
    public Account? DeleteAccountById(int accountId);

    /// <summary>
    /// deletes the user with <c>userId</c> from the account with <c>accountId</c>
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="userId"></param>
    /// <returns>the user that was just deleted, null if the account does not exist, or if the account does not have an user with <c>userId</c></returns>
    public User? DeleteAccountUserByUserId(int accountId, int userId);




    /// <summary>
    /// find a transaction with <c>transactionId</c>
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns>the transaction with <c>transactionId</c>, null if transaction does not exist</returns>
    public Transaction? GetTransactionByTransactionId(int transactionId);

    /// <summary>
    /// find all existing transactions in the database
    /// </summary>
    /// <returns>list containing all existing transactions, empty list if non exists</returns>
    public List<Transaction> GetAllTransactions();

    /// <summary>
    /// find all transaction with <c><fromAccountId/c> as the from account
    /// </summary>
    /// <param name="fromAccountId"></param>
    /// <returns>list containing all such transactions</returns>
    public List<Transaction> GetTransactionsByFromAccountId(int fromAccountId);

    /// <summary>
    /// find all transaction with <c><toAccountId/c> as the from account
    /// </summary>
    /// <param name="toAccountId"></param>
    /// <returns>list containing all such transactions</returns>
    public List<Transaction> GetTransactionsByToAccountId(int toAccountId);

    /// <summary>
    /// uploads the transaction to database
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns>the transaction that was just created</returns>
    public Transaction CreateTransaction(Transaction transaction);

    /// <summary> 
    /// deletes the transaction with <c>transactionId</c>
    /// </summary>
    /// <param name="transactionId"></param>
    /// <returns>the transaction that was just deleted, null if the transaction does not exist</returns>
    public Transaction? DeleteTransactionByTransactionId(int transactionId);

}