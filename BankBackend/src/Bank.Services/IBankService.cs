namespace BankBackend.Service;

using BankBackend.Models;

/// <summary>
/// </summary>
public interface IBankService
{
    // Validate the login credentials for a user.
    public User ValidateLogin(string username, string password);
    // Get all users
    public List<User> GetAllUsers();
    // Get a user by their username.
    public User GetUserByUsername(string username);
    // Get a user by userId
    public User GetUserByUserId(int userId);
    // Get a account by account
    public Account GetAccountByAccountId(int accountId);
    // Get all accounts associated with a user by their userId.
    public List<Account> GetAccountsByUserId(int userId);
    // Withdraw an amount from an account.
    public Transaction Withdraw(int accountId, double amount);
    // Deposit an amount into an account.
    public Transaction Deposit(int accountId, double amount);
    // Transfer amount 
    public Transaction Transfer(int fromAccountId, int toAccountId, double amount);
    // Get all transactions associated with a user by their userId.
    public List<Transaction> GetTransactionsByUserId(int userId);
    // Get all transactions associated with an accountId 
    public List<Transaction> GetTransactionsByAccountId(int accountId);
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
