using Microsoft.AspNetCore.Mvc;
using Moq;
using UJ_AccountManagement.API.Controllers;
using UJ_AccountManagement.Domain.Entities;
using UJ_AccountManagement.Domain.Interfaces;

namespace UJ_AccountManagement.Test;

[TestClass]
public class TransactionRepositoryTests
{
    private Mock<ITransactionRepository> _mockRepository;
    private TransactionsController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<ITransactionRepository>();
        _controller = new TransactionsController(_mockRepository.Object);
    }

    [TestMethod]
    public async Task GetAllTransactions_ReturnsListOfTransactions()
    {
        // Arrange
        var transactions = new List<Transaction>
        {
            new Transaction { TransactionId = 1, Amount = 100, TransactionDate = DateTime.Now, 
                AccountId=2038, ReferenceId=38, TransactionType="Withdrawal"},
            new Transaction { TransactionId = 2, Amount = 500, TransactionDate = DateTime.Now, 
                AccountId=2058, ReferenceId=75, TransactionType="Withdrawal" }
        };
        _mockRepository.Setup(repo => repo.GetAllTransactions()).ReturnsAsync(transactions);
        // Act
        var result = await _controller.GetAllTransactions();
        // Assert
        var okResult = result.Result as OkObjectResult;
        var model = okResult.Value as List<Transaction>;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(2, model.Count);
    }

    [TestMethod]
    public async Task GetTransactionById_ExistingId_ReturnsTransaction()
    {
        // Arrange
        var transaction = new Transaction
        {
            TransactionId = 1,
            AccountId = 1001,
            TransactionType = "Deposit",
            ReferenceId = 0,
            Amount = 1500,
            TransactionDate = DateTime.UtcNow
        };

        _mockRepository
            .Setup(repo => repo.GetTransactionById(1))
            .ReturnsAsync(transaction);

        // Act
        var result = await _controller.GetTransactionById(1);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        var returnedTransaction = okResult.Value as Transaction;
        Assert.IsNotNull(returnedTransaction);
        Assert.AreEqual(1, returnedTransaction.TransactionId);
    }

    [TestMethod]
    public async Task GetTransactionById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockRepository
            .Setup(repo => repo.GetTransactionById(999)) // Transaction doesn't exist
            .ReturnsAsync((Transaction)null);

        // Act
        var result = await _controller.GetTransactionById(999);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
        var response = notFoundResult.Value as dynamic;
        Console.WriteLine(result.Result);
        
    }
}
