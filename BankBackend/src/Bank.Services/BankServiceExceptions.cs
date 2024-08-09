namespace BankBackend.Service
{
    public class BankExceptions : Exception
    {
        public BankExceptions() : base() { }
        public BankExceptions(string message) : base(message) { }
    }

    public class InvalidPasswordException : BankExceptions
    {
        public InvalidPasswordException() : base() { }
        public InvalidPasswordException(string message) : base(message) { }
    }

    public class UsernameNotFoundException : BankExceptions
    {
        public UsernameNotFoundException() : base() { }
        public UsernameNotFoundException(string message) : base(message) { }
    }

    public class UsernameAlreadyExistsException : BankExceptions
    {
        public UsernameAlreadyExistsException() : base() { }
        public UsernameAlreadyExistsException(string message) : base(message) { }
    }

    public class UserIdNotFoundException : BankExceptions
    {
        public UserIdNotFoundException() : base() { }
        public UserIdNotFoundException(string message) : base(message) { }
    }

    public class UserNotAuthorizedException : BankExceptions
    {
        public UserNotAuthorizedException() : base() { }
        public UserNotAuthorizedException(string message) : base(message) { }
    }

    public class AccountIdNotFoundException : BankExceptions
    {
        public AccountIdNotFoundException() : base() { }
        public AccountIdNotFoundException(string message) : base(message) { }
    }

    public class InsufficientFundsException : BankExceptions
    {
        public InsufficientFundsException() : base() { }
        public InsufficientFundsException(string message) : base(message) { }
    }

    public class RepositoryException : BankExceptions
    {
        public RepositoryException() : base() { }
        public RepositoryException(string message) : base() { }
    }
}