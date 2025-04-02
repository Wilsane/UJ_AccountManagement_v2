using Moq;
using AutoFixture;
using UJ_AccountManagement.API.Controllers;
using UJ_AccountManagement.Domain.Interfaces;

namespace UJ_AccountManagement._Api.Tests.Controllers.TransactionsControllerTests
{
    // Base class for all tests
    public class TransactionsControllerTests
    {
        protected readonly IFixture _fixture;
        protected readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        protected readonly TransactionsController _transactionsController;

        public TransactionsControllerTests()
        {
            _fixture = new Fixture();
            _transactionRepositoryMock = _fixture.Freeze<Mock<ITransactionRepository>>();
            _transactionsController = new TransactionsController(_transactionRepositoryMock.Object);
        }
    }
}