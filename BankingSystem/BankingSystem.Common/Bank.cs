using BankingSystem.Common.Models;

namespace BankingSystem.Common;

public class Bank
{
    private List<Account> Accounts = new List<Account>();
    
    public int CreateAccount()
    {
        var account = new Account
        {
            AccountNumber = GenerateAccountNumber(),
            Balance = 0,
        };
        
        Accounts.Add(account);

        return account.AccountNumber;
    }
    
    public decimal DepositMoney(int accountNumber, decimal depositAmount)
    {
        var account = GetAccount(accountNumber);
        
        if (depositAmount > 0)
        {
            account.Balance += depositAmount;
        }
        else
        {
            throw new Exception("Invalid amount. Please try again.");
        }
        
        return account.Balance;
    }
    
    public decimal WithdrawMoney(int accountNumber, decimal withdrawAmount)
    {
        var account = GetAccount(accountNumber);
        
        if (withdrawAmount > 0)
        {
            if (withdrawAmount <= account.Balance)
            {
                account.Balance -= withdrawAmount;
            }
            else
            {
                throw new Exception("Insufficient funds. Please try again.");
            }
        }
        else
        {
            throw new Exception("Invalid amount. Please try again.");
        }
        
        return account.Balance;
    }

    public decimal CheckBalance(int accountNumber)
    {
        var account = GetAccount(accountNumber);
        
        return account.Balance;
    }

    private Account GetAccount(int accountNumber)
    {
        var account = Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

        if (account == null)
            throw new Exception("Account not found.");
        
        return account;
    }

    private int GenerateAccountNumber()
    {
        const int min = 10000000; // Smallest 8-digit number
        const int max = 99999999; // Largest 8-digit number
        var random = new Random();
        
        return random.Next(min, max + 1);
    }
}