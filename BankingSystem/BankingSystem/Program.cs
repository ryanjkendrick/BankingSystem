using BankingSystem.Common;

namespace BankingSystem;

class Program
{
    private static int _accountNumber;
    private static readonly Bank _bank = new Bank();
    
    static void Main(string[] args)
    {
        bool exit = false;

        Console.WriteLine("Welcome to the Banking System!");

        while (!exit)
        {
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit Money");
            Console.WriteLine("3. Withdraw Money");
            Console.WriteLine("4. Check Balance");
            Console.WriteLine("5. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _accountNumber = _bank.CreateAccount();
                    
                    Console.WriteLine($"Account successfully created! Account number is {_accountNumber} and balance is $0.");
                    
                    break;

                case "2":
                    // Deposit money
                    DepositMoney();
                    break;

                case "3":
                    // Withdraw money
                    WithdrawMoney();
                    break;

                case "4":
                    // Check balance
                    CheckBalance();
                    break;

                case "5":
                    // Exit the application
                    exit = true;
                    Console.WriteLine("Thank you for using the Banking System. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
    
    static void DepositMoney()
    {
        Console.WriteLine("Enter the amount you want to deposit: ");
        string input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal depositAmount))
        {
            try
            {
                var currentBalance = _bank.DepositMoney(_accountNumber, depositAmount);
                
                Console.WriteLine($"You have successfully deposited ${depositAmount}. Your new balance is ${currentBalance}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    static void WithdrawMoney()
    {
        Console.WriteLine("Enter the amount you want to withdraw: ");
        string input = Console.ReadLine();
        
        if (decimal.TryParse(input, out decimal withdrawAmount))
        {
            try
            {
                var currentBalance = _bank.WithdrawMoney(_accountNumber, withdrawAmount);

                Console.WriteLine(
                    $"You have successfully withdrawn ${withdrawAmount}. Your new balance is ${currentBalance}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Invalid amount. Please try again.");
        }
    }

    static void CheckBalance()
    {
        var currentBalance = _bank.CheckBalance(_accountNumber);
        
        Console.WriteLine($"Your current balance is ${currentBalance}.");
    }
}