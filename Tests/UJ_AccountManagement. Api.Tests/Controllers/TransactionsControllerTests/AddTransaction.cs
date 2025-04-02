using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UJ_AccountManagement.Domain.Entities;
using FluentAssertions;





namespace UJ_AccountManagement._Api.Tests.Controllers.TransactionsControllerTests
{
    public class AddTransaction : TransactionsControllerTests
    {
        [Fact]
        public async Task ReturnsCreatedResult_WhenTransactionIsValid()
        {
            // Arrange
            var transactionMock = _fixture.Create<Transaction>();
            _transactionRepositoryMock.Setup(x => x.AddTransaction(transactionMock)).Returns(Task.CompletedTask);

            // Act
            var result = await _transactionsController.AddTransaction(transactionMock).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<CreatedResult>();

            var createdResult = result.As<CreatedResult>();
            createdResult.Value.Should().NotBeNull()
                .And.BeEquivalentTo(new { message = "Transaction added successfully! 😊", status = "success", transaction = transactionMock });

            _transactionRepositoryMock.Verify(x => x.AddTransaction(transactionMock), Times.Once());
        }

        [Fact]
        public async Task ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var transactionMock = _fixture.Create<Transaction>();
            _transactionsController.ModelState.AddModelError("Error", "Invalid model!");

            // Act
            var result = await _transactionsController.AddTransaction(transactionMock).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var badRequestResult = result.As<BadRequestObjectResult>();
            badRequestResult.Value.Should().Be("Invalid model! 😢");

            _transactionRepositoryMock.Verify(x => x.AddTransaction(It.IsAny<Transaction>()), Times.Never());
        }

        [Fact]
        public async Task ReturnsBadRequest_WhenTransactionIsNull()
        {
            // Arrange
            Transaction transactionMock = null;

            // Act
            var result = await _transactionsController.AddTransaction(transactionMock).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var badRequestResult = result.As<BadRequestObjectResult>();
            badRequestResult.Value.Should().Be("Invalid model! 😢");

            _transactionRepositoryMock.Verify(x => x.AddTransaction(It.IsAny<Transaction>()), Times.Never());
        }
    }
}
