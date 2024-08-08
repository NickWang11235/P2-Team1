namespace BankBackend.Service;

using BankBackend.Models;

/// <summary>
/// </summary>
public interface IBankService
{
    // Validate the login credentials for a user.
    public User ValidateLogin(string username, string password);
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
    public Account Withdraw(int accountId, double amount);
    // Deposit an amount into an account.
    public Account Deposit(int accountId, double amount);
    public List<Transaction> GetTransactionsByAccountId(int accountId);
    // Withdraw an amount from an account.
    // Add an account to a family.
    public User AddAccountUser(int userId, int accountId);
    // Remove an account from a family.
    public User RemoveAccountUser(int userId, int accountId);
    // Update the profile of a user.
    public User UpdateUserProfile(int userId, string newUsername, string newPassword);
    // Create a new user
    public User CreateUser(User user);
    // Createa a new account
    public Account CreateAccount(Account account);
}
