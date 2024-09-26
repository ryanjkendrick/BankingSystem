using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankingSystem.Common.Tests;

[TestClass]
    public class BankTests
    {
        private Bank _bank;

        [TestInitialize]
        public void Setup()
        {
            _bank = new Bank();
        }

        [TestMethod]
        public void CreateAccount_ShouldReturnValidAccountNumber()
        {
            // Act
            int accountNumber = _bank.CreateAccount();

            // Assert
            Assert.IsTrue(accountNumber >= 10000000 && accountNumber <= 99999999, "Account number is not valid.");
        }

        [TestMethod]
        public void DepositMoney_ShouldIncreaseBalance()
        {
            // Arrange
            int accountNumber = _bank.CreateAccount();
            decimal depositAmount = 500m;

            // Act
            decimal balance = _bank.DepositMoney(accountNumber, depositAmount);

            // Assert
            Assert.AreEqual(depositAmount, balance, "Balance should be equal to the deposit amount.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid amount. Please try again.")]
        public void DepositMoney_ShouldThrowException_WhenDepositAmountIsNegative()
        {
            // Arrange
            int accountNumber = _bank.CreateAccount();

            // Act
            _bank.DepositMoney(accountNumber, -100m);
        }

        [TestMethod]
        public void WithdrawMoney_ShouldDecreaseBalance_WhenSufficientFunds()
        {
            // Arrange
            int accountNumber = _bank.CreateAccount();
            _bank.DepositMoney(accountNumber, 500m);
            decimal withdrawAmount = 200m;

            // Act
            decimal balance = _bank.WithdrawMoney(accountNumber, withdrawAmount);

            // Assert
            Assert.AreEqual(300m, balance, "Balance should be 300 after withdrawal.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Insufficient funds. Please try again.")]
        public void WithdrawMoney_ShouldThrowException_WhenInsufficientFunds()
        {
            // Arrange
            int accountNumber = _bank.CreateAccount();
            _bank.DepositMoney(accountNumber, 100m);

            // Act
            _bank.WithdrawMoney(accountNumber, 200m); // Trying to withdraw more than balance
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid amount. Please try again.")]
        public void WithdrawMoney_ShouldThrowException_WhenWithdrawAmountIsNegative()
        {
            // Arrange
            int accountNumber = _bank.CreateAccount();

            // Act
            _bank.WithdrawMoney(accountNumber, -100m);
        }

        [TestMethod]
        public void CheckBalance_ShouldReturnCorrectBalance()
        {
            // Arrange
            int accountNumber = _bank.CreateAccount();
            _bank.DepositMoney(accountNumber, 300m);
            _bank.WithdrawMoney(accountNumber, 100m);

            // Act
            decimal balance = _bank.CheckBalance(accountNumber);

            // Assert
            Assert.AreEqual(200m, balance, "Balance should be 200 after deposit and withdrawal.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Account not found.")]
        public void GetAccount_ShouldThrowException_WhenAccountDoesNotExist()
        {
            // Act
            _bank.CheckBalance(99999999); // Non-existent account
        }
    }