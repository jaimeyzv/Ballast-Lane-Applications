using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Tech.Interview.Api.Controllers;
using Tech.Interview.Application.Presentation;
using Tech.Interview.Application.Services;
using Tech.Interview.Domain.Entities;

namespace Tech.Interview.UnitTests;

public class Tests
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
    public async Task GetUserByIdAsync_WhenRequestIsInvalidDueUserDoesnotExist_ReturnsNotFound()
    {
        //Arrange
        User user = null;
        this._userServiceMock
            .Setup(x => x.GetUserByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(user);

        _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);

        //Act
        var result = await _userController.GetUserByIdAsync(100);
        var okResult = result as NotFoundResult;

        //Assert
        Assert.That(okResult, Is.TypeOf<NotFoundResult>());
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