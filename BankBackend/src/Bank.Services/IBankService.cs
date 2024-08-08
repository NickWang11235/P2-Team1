namespace BankBackend.Service;

using BankBackend.Models;

/// <summary>
/// </summary>
public interface IBankService
{
    // Validate the login credentials for a user.
    public string ValidateLogin(string username, string password);
    // Get a user by their username.
    public User GetUserByUsername(string username);
    // Get a user by userId
    public User GetUserByUserId(int userId);
    // Get a account by account
    public Account GetAccountByAccountId(int accountId);
    // Get all accounts associated with a user by their userId.
    public List<Account> GetAccountsByUserId(int userId);
    // Get all transactions associated with a user by their userId.
    public List<Transaction> GetTransactionsByUserId(int userId);
    // Get all transactions associated with an accountId 
    public List<Transaction> GetTransactionsByAccountId(int accountId);
    // Withdraw an amount from an account.
    public string Withdraw(int accountId, double amount);
    // Deposit an amount into an account.
    public string Deposit(int accountId, double amount);
    // Add an account to a family.
    public string AddAccountToFamily(int userId, int accountId);
    // Remove an account from a family.
    public string RemoveAccountFromFamily(int userId, int accountId);
    // Update the profile of a user.
    public string UpdateUserProfile(int userId, string newUsername, string newPassword);
    // Create a new user
    public User CreateUser(User user);
    // Createa a new account
    public Account CreateAccount(Account account);
}
