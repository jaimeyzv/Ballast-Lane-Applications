using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Tech.Interview.Api.Controllers;
using Tech.Interview.Application.Presentation;
using Tech.Interview.Application.Services;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.UnitTests;

public class UserControllerTests
{
    private Mock<IUserService> _userServiceMock;
    private Mock<IMapper> _mapperMock;
    private UserController _userController;

    [SetUp]
    public void Setup()
    {
        _userServiceMock = new Mock<IUserService>();
        _mapperMock = new Mock<IMapper>();
    }

    [Test]
    public async Task GetAllUsersAsync_WhenRequestIsValid_ReturnsOkObjectResult()
    {
        //Arrange
        this._userServiceMock
            .Setup(x => x.GetAllUsersAsync())
            .ReturnsAsync(GetFakeUsers());

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.GetAllUsersAsync();
        var okResult = result as ObjectResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<OkObjectResult>());
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public async Task GetAllUsersAsync_WhenRequestIsValid_ReturnsListOfUsers()
    {
        //Arrange
        this._userServiceMock
            .Setup(x => x.GetAllUsersAsync())
            .ReturnsAsync(GetFakeUsers());

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.GetAllUsersAsync();
        var okResult = result as ObjectResult;

        //Assert
        Assert.That(okResult.Value, Is.TypeOf<List<GetAllUsersViewModel>>());
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public async Task GetUserByIdAsync_WhenRequestIsValid_ReturnsOkObjectResultWithUser()
    {
        //Arrange
        var user = GetFakeUsers().First();
        this._userServiceMock
            .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(user);
        this._mapperMock
            .Setup(x => x.Map<GetUserByIdViewModel>(It.IsAny<User>()))
            .Returns(new GetUserByIdViewModel());

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.GetUserByIdAsync(user.UserId);
        var okResult = result as ObjectResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<OkObjectResult>());
        Assert.That(okResult.Value, Is.TypeOf<GetUserByIdViewModel>());
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public async Task GetUserByIdAsync_WhenRequestIsInvalidDueUserDoesnotExist_ReturnsNotFoundObjectResult()
    {
        //Arrange
        User user = null;
        this._userServiceMock
            .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(user);

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.GetUserByIdAsync(100);
        var okResult = result as NotFoundObjectResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<NotFoundObjectResult>());
        Assert.That(okResult.StatusCode, Is.EqualTo(404));
    }

    [Test]
    public async Task CreateUserAsync_WhenRequestIsValid_ReturnsCreatedResult()
    {
        //Arrange
        var user = GetFakeUsers().First();       
        this._mapperMock
            .Setup(x => x.Map<User>(It.IsAny<CreateUserViewModel>()))
            .Returns(new User());

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.CreateUserAsync(new CreateUserViewModel());
        var okResult = result as ObjectResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<CreatedResult>());
        Assert.That(okResult.StatusCode, Is.EqualTo(201));
    }

    [Test]
    public async Task CreateUserAsync_WhenInternalErrorOccurs_ThrowsException()
    {
        //Arrange
        var expectedException = new Exception("Internal error on creating");
        this._mapperMock
            .Setup(x => x.Map<User>(It.IsAny<CreateUserViewModel>()))
            .Returns(new User());
        this._userServiceMock
            .Setup(x => x.CreateUserAsync(It.IsAny<User>()))
            .ThrowsAsync(expectedException);

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var error = Assert.ThrowsAsync<Exception>(() => _userController.CreateUserAsync(new CreateUserViewModel()));

        //Assert
        Assert.That(error, Is.TypeOf<Exception>());
        Assert.That(error.Message, Is.Not.Empty);
    }

    [Test]
    public async Task UpdateUserAsync_WhenRequestIsValid_ReturnsOkResult()
    {
        //Arrange
        var user = GetFakeUsers().First();
        this._userServiceMock
            .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(user);
        this._mapperMock
            .Setup(x => x.Map<User>(It.IsAny<UpdateUserViewModel>()))
            .Returns(new User());

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.UpdateUserAsync(It.IsAny<int>(), new UpdateUserViewModel());
        var okResult = result as OkResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<OkResult>());
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public async Task UpdateUserAsync_WhenUserIdDoesNotExist_ReturnsBadRequestObjectResult()
    {
        //Arrange
        User user = null;
        this._userServiceMock
            .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(user);

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.UpdateUserAsync(It.IsAny<int>(), new UpdateUserViewModel());
        var okResult = result as BadRequestObjectResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(okResult.StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task UpdateUserAsync_WhenInternalErrorOccurs_ThrowsException()
    {
        //Arrange
        var expectedException = new Exception("Internal error on updating");
        this._userServiceMock
           .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
           .ReturnsAsync(new User());
        this._mapperMock
            .Setup(x => x.Map<User>(It.IsAny<UpdateUserViewModel>()))
            .Returns(new User());
        this._userServiceMock
            .Setup(x => x.UpdateUserAsync(It.IsAny<User>()))
            .ThrowsAsync(expectedException);

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var error = Assert.ThrowsAsync<Exception>(() => _userController.UpdateUserAsync(It.IsAny<int>(), new UpdateUserViewModel()));

        //Assert
        Assert.That(error, Is.TypeOf<Exception>());
        Assert.That(error.Message, Is.Not.Empty);
    }

    [Test]
    public async Task DeleteUserAsync_WhenRequestIsValid_ReturnsOkResult()
    {
        //Arrange
        var user = GetFakeUsers().First();
        this._userServiceMock
            .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(user);

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.DeleteUserAsync(It.IsAny<int>());
        var okResult = result as OkResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<OkResult>());
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public async Task DeleteUserAsync_WhenUserIdDoesNotExist_ReturnsBadRequestObjectResult()
    {
        //Arrange
        User user = null;
        this._userServiceMock
            .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(user);

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.DeleteUserAsync(It.IsAny<int>());
        var okResult = result as BadRequestObjectResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<BadRequestObjectResult>());
        Assert.That(okResult.StatusCode, Is.EqualTo(400));
    }

    [Test]
    public async Task DeleteUserAsync_WhenInternalErrorOccurs_ThrowsException()
    {
        //Arrange
        var expectedException = new Exception("Internal error on deleting");
        this._userServiceMock
           .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
           .ReturnsAsync(new User());
        this._userServiceMock
            .Setup(x => x.DeleteUserAsync(It.IsAny<int>()))
            .ThrowsAsync(expectedException);

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var error = Assert.ThrowsAsync<Exception>(() => _userController.DeleteUserAsync(It.IsAny<int>()));

        //Assert
        Assert.That(error, Is.TypeOf<Exception>());
        Assert.That(error.Message, Is.Not.Empty);
    }

    #region Private

    private IEnumerable<User> GetFakeUsers()
    {
        return new List<User>
        {
            new User { UserId = 1, Name = "Josue", LastName = "Sanchez", Age = 20 },
            new User { UserId = 2, Name = "Andrea", LastName = "Guillen", Age = 31 },
            new User { UserId = 3, Name = "Perla", LastName = "Rosillo", Age = 15 }
        };
    }

    #endregion
}