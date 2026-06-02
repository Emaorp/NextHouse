using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Account.Commands.Loging;


namespace NextHouse.Tests.UnitTests.Application.Account
{
    [TestClass]
    public class LoginUseCaseTests
    {
        private Mock<IAccountRepository> _repository = null!;
        private LoginUseCase _useCase = null!;

        [TestInitialize]
        public void Setup()
        {
            _repository = new Mock<IAccountRepository>();

            _useCase = new LoginUseCase(
                _repository.Object);
        }

        [TestMethod]
        public async Task Handle_WithValidCredentials_ReturnsRepositoryResult()
        {
            AccountSignInResult expected =
                new AccountSignInResult
                {
                    Succeeded = true,
                    IsLockedOut = false
                };
            _repository
                .Setup(x =>
                    x.SignInAsync(
                        "admin",
                        "123",
                        true))
                .ReturnsAsync(expected);

            LoginCommand command = new LoginCommand
            {
                UserName = "admin",
                Password = "123",
                RememberMe = true
            };

            AccountSignInResult result =
                await _useCase.Handle(command);

            Assert.AreSame(expected, result);
        }

        [TestMethod]
        public async Task Handle_WithValidCredentials_CallsRepositoryOnce()
        {
            _repository
                .Setup(x =>
                    x.SignInAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>()))
                .ReturnsAsync(
    new AccountSignInResult
    {
        Succeeded = true,
        IsLockedOut = false
    });

            LoginCommand command = new LoginCommand
            {
                UserName = "admin",
                Password = "123"
            };

            await _useCase.Handle(command);

            _repository.Verify(
                x => x.SignInAsync(
                    command.UserName,
                    command.Password,
                    command.RememberMe),
                Times.Once);
        }

        [TestMethod]
        public async Task Handle_WhenRepositoryThrowsException_RethrowsException()
        {
            _repository
                .Setup(x =>
                    x.SignInAsync(
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<bool>()))
                .ThrowsAsync(
                    new InvalidOperationException());

            LoginCommand command = new LoginCommand
            {
                UserName = "admin",
                Password = "123"
            };

            bool exceptionThrown = false;

            try
            {
                await _useCase.Handle(command);
            }
            catch (InvalidOperationException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }
    }
}