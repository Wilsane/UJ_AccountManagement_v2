using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UJ_AccountManagement.Domain.Entities;
using FluentAssertions;


namespace UJ_AccountManagement._Api.Tests.Controllers.TransactionsControllerTests
{
    public class GetTransactionById : TransactionsControllerTests
    {

        [Fact]
        public async Task ReturnOkWithTransaction_WhenValidInput()
        {
            // Arrange
            var transactionMock = _fixture.Create<Transaction>();
            var id = _fixture.Create<int>();
            _transactionRepositoryMock.Setup(x => x.GetTransactionById(id)).ReturnsAsync(transactionMock);

            // Act
            var result = await _transactionsController.GetTransactionById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Transaction>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();

            var okResult = result.Result.As<OkObjectResult>();
            okResult.Value.Should().NotBeNull().And.BeOfType<Transaction>();

            _transactionRepositoryMock.Verify(x => x.GetTransactionById(id), Times.Once());
        }


        [Fact]
        public async Task ReturnOkWithTransactions_WhenDataFound()
        {
            // Arrange
            var transactionMock = _fixture.Create<Transaction>();
            var id = transactionMock.TransactionId;
            _transactionRepositoryMock.Setup(x => x.GetTransactionById(id)).ReturnsAsync(transactionMock);

            // Act
            var result = await _transactionsController.GetTransactionById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Transaction>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();

            var okResult = result.Result.As<OkObjectResult>();
            okResult.Value.Should().NotBeNull().And.BeOfType<Transaction>();

            _transactionRepositoryMock.Verify(x => x.GetTransactionById(id), Times.Once());
        }

        [Fact]
        public async Task ReturnBadRequest_WhenInputIsZeroOrNegative()
        {
            // Arrange
            var response = _fixture.Create<Transaction>();
            int id = 0;
            _transactionRepositoryMock.Setup(x => x.GetTransactionById(id)).ReturnsAsync(response);

            // Act
            var result = await _transactionsController.GetTransactionById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>(); 

            var badRequestResult = result.Result.As<BadRequestObjectResult>();
            badRequestResult.Value.Should().NotBeNull()
                .And.BeEquivalentTo(new { message = "Invalid transaction ID!", status = "error" });

            _transactionRepositoryMock.Verify(x => x.GetTransactionById(id), Times.Never());
        }

        [Fact]
        public async Task ReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _transactionRepositoryMock.Setup(x => x.GetTransactionById(id)).ReturnsAsync((Transaction)null);

            // Act
            var result = await _transactionsController.GetTransactionById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();

            var notFoundResult = result.Result.As<NotFoundObjectResult>();
            notFoundResult.Value.Should().NotBeNull()
                .And.BeEquivalentTo(new { message = "Transaction does not exist! \t :(", status = "error" });

            _transactionRepositoryMock.Verify(x => x.GetTransactionById(id), Times.Once());
        }
    }
}
