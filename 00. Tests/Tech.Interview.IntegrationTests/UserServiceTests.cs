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
        public async Task CreateUserAsync_WhenEntryIsCorrect_DbRecordsIncrease()
        {
            //Arrange
            _unitOfWorkMock
                .SetupSequence(x => x.Create())
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"));
            _userService = new UserService(_unitOfWorkMock.Object);
            var newUser = new User { Name = "John Test 001", LastName = "Connor Test 001", Age = 35 };

            //Act
            var allUserCounts = await this._userService.GetAllUsersAsync();
            await this._userService.CreateUserAsync(newUser);
            var newAllUserCounts = await this._userService.GetAllUsersAsync();

            //Assert
            Assert.That(newAllUserCounts.Count(), Is.EqualTo(allUserCounts.Count() + 1));
        }

        [Test]
        public async Task DeleteUserAsync_WhenEntryIsCorrect_DbRecordsDecrease()
        {
            //Arrange
            _unitOfWorkMock
                .SetupSequence(x => x.Create())
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"));
            _userService = new UserService(_unitOfWorkMock.Object);

            //Act
            var allUserCounts = await this._userService.GetAllUsersAsync();
            await this._userService.DeleteUserAsync(allUserCounts.First().UserId);
            var newAllUserCounts = await this._userService.GetAllUsersAsync();

            //Assert
            Assert.That(newAllUserCounts.Count(), Is.EqualTo(allUserCounts.Count() - 1));
        }

        [Test]
        public async Task UpdateUserAsync_WhenEntryIsCorrect_EntityIsUpdate()
        {
            //Arrange
            _unitOfWorkMock
                .SetupSequence(x => x.Create())
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"))
                .Returns(new UnitOfWorkAdapter("Data Source=.\\SQLEXPRESS;Initial Catalog=InterviewDB;Integrated Security=True"));
            _userService = new UserService(_unitOfWorkMock.Object);

            //Act
            var newUser = new User { Name = $"Lionel {DateTime.Now.ToString("yyyyMMddHHmmss")}", LastName = "Messi", Age = 37 };
            await this._userService.CreateUserAsync(newUser);
            var allUserCounts = await this._userService.GetAllUsersAsync();
            var userMessi = allUserCounts.First(x => x.Name == newUser.Name);
            userMessi.Name = $"Cristiano {DateTime.Now.ToString("yyyyMMddHHmmss")}";
            userMessi.LastName = "Dos Santos Aveiro";
            userMessi.Age = 39;
            await this._userService.UpdateUserAsync(userMessi);
            var newAllUserCounts = await this._userService.GetAllUsersAsync();

            //Assert
            Assert.That(newUser.Name, Is.Not.EqualTo(userMessi.Name));
            Assert.That(newUser.LastName, Is.Not.EqualTo(userMessi.LastName));
            Assert.That(newUser.Age, Is.Not.EqualTo(userMessi.Age));
            Assert.That(newAllUserCounts.Count(), Is.EqualTo(allUserCounts.Count()));
        }
    }
}
