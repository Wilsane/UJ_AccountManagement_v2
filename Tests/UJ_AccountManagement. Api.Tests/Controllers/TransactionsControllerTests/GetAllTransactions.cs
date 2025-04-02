using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UJ_AccountManagement.Domain.Entities;
using FluentAssertions;


namespace UJ_AccountManagement._Api.Tests.Controllers.TransactionsControllerTests
{
    public class GetAllTransactions : TransactionsControllerTests
    {
        [Fact]
        public async Task ReturnsOkWithTransactions_WhenDataFound()
        {
            // Arrangef
            var transactionsMock = _fixture.Create<List<Transaction>>();
            _transactionRepositoryMock
                .Setup(x => x.GetAllTransactions())
                .ReturnsAsync(transactionsMock);

            // Act
            var result = await _transactionsController.GetAllTransactions().ConfigureAwait(true);

            // Assert
            result.Should().NotBeNull();

            result.Should().BeAssignableTo<ActionResult<List<Transaction>>>();

            result.Result.Should().BeAssignableTo<OkObjectResult>();

            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(transactionsMock.GetType());

            _transactionRepositoryMock.Verify(x => x.GetAllTransactions(), Times.Once());
        }

        [Fact]
        public async Task ReturnsNotFound_WhenDataNotFound()
        {
            //Arrange
            List<Transaction> response = null;
            _transactionRepositoryMock.Setup(x => x.GetAllTransactions()).ReturnsAsync(response);

            //Act
            var result = await _transactionsController.GetAllTransactions().ConfigureAwait(true);

            //Asset
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NotFoundResult>();
            _transactionRepositoryMock.Verify(x => x.GetAllTransactions(), Times.Once());
        }
    }
}
