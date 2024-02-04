using Moq;
using NUnit.Framework;
using Tech.Interview.Application.Persistence.UoW;
using Tech.Interview.Application.Services;
using Tech.Interview.Domain.Entities;
using Tech.Interview.Persistence.UoW;
using Tech.Interview.Service;

namespace Tech.Interview.UnitTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IUserService _userService;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Test]
        public async Task CreateUserAsync_WhenEntryIsCorrect_Success()
        {
            //Arrange
            _unitOfWorkMock
                .SetupSequence(x => x.Create())
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"));
            _userService = new UserService(_unitOfWorkMock.Object);
            var newUser = new User { Name = "John Test 002", LastName = "Connor  Test 002", Age = 32 };

            //Act
            await this._userService.CreateUserAsync(newUser);
            var exisingUsers = await this._userService.GetAllUsersAsync();
            var newUserCreated = exisingUsers
                .Single(x => x.Name == newUser.Name && x.LastName == newUser.LastName && x.Age == newUser.Age);

            //Assert
            Assert.That(newUserCreated, Is.Not.Null);

            await this._userService.DeleteUserAsync(newUserCreated.UserId);
        }
    }
}