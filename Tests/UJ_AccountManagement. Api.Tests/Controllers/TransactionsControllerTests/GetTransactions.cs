using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UJ_AccountManagement.Domain.Entities;
using FluentAssertions;


namespace UJ_AccountManagement._Api.Tests.Controllers.TransactionsControllerTests
{
    public class GetTransactions : TransactionsControllerTests
    {
        [Fact]
        public async Task ReturnsOkResultWithTransactions_WhenDataFound()
        {
            // Arrange
            var searchTerm = _fixture.Create<string>();
            var transactionsMock = _fixture.Create<List<Transaction>>();
            _transactionRepositoryMock.Setup(x => x.GetTransactions(searchTerm)).ReturnsAsync(transactionsMock);

            // Act
            var result = await _transactionsController.GetTransactions(searchTerm).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<List<Transaction>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();

            var okResult = result.Result.As<OkObjectResult>();
            okResult.Value.Should().NotBeNull().And.BeOfType<List<Transaction>>();

            _transactionRepositoryMock.Verify(x => x.GetTransactions(searchTerm), Times.Once());
        }

        [Fact]
        public async Task ReturnsNotFound_WhenDataNotFound()
        {
            // Arrange 
            var searchTerm = _fixture.Create<string>();
            List<Transaction> response = null;
            _transactionRepositoryMock.Setup(x => x.GetTransactions(searchTerm)).ReturnsAsync(response);

            // Act
            var result = await _transactionsController.GetTransactions(searchTerm).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundObjectResult>();

            var notFoundResult = result.Result.As<NotFoundObjectResult>();
            notFoundResult.Value.Should().NotBeNull()
                .And.BeEquivalentTo(new { message = "No transactions found! 😢", status = "error" });

            _transactionRepositoryMock.Verify(x => x.GetTransactions(searchTerm), Times.Once());
        }
    }
}
