using Xunit;
using Moq;
using Sensoring_API.Controllers;
using Sensoring_API.Repositories;
using Sensoring_API.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Sensoring_API.Models;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

public class LitterControllerTests
{
    private readonly Mock<ILitterRepository> _mockRepo;
    private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
    private readonly LitterController _controller;

    public LitterControllerTests()
    {
        _mockRepo = new Mock<ILitterRepository>();

        var userStoreMock = new Mock<IUserStore<IdentityUser>>();
        _mockUserManager = new Mock<UserManager<IdentityUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null
        );

        _controller = new LitterController(_mockRepo.Object);

        // gemockde gebruiker toevoegen aan de controller context
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "testuser")
        }, "mock"));
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task Create_ValidDto_ReturnsOk()
    {
        // Arrange
        var dto = new LitterCreateDto
        {
            TypeOfTrash = "Plastic",
            Location = "Park",
            Coordinates = new float[] { 1.0f, 2.0f },
            Time = DateTime.UtcNow
        };

        _mockRepo.Setup(r => r.Create(dto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Contains("created successfully", okResult.Value.ToString());
    }

    [Fact]
    public async Task Create_ThrowsException_Returns500()
    {
        var dto = new LitterCreateDto
        {
            TypeOfTrash = "Plastic",
            Location = "Park",
            Coordinates = new float[] { 1.0f, 2.0f },
            Time = DateTime.UtcNow
        };

        _mockRepo.Setup(r => r.Create(dto)).Throws(new Exception("DB error"));

        var result = await _controller.Create(dto);

        var status = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, status.StatusCode);
        Assert.Contains("DB error", status.Value.ToString());
    }

    [Fact]
    public async Task Read_WithNoFilters_ReturnsOkWithData()
    {
        // Arrange

        var litter = new Litter
        {
            Id = 1,
            TypeOfTrash = "Plastic",
            Location = "Park",
            Coordinates = [1.0f, 2.0f],
            Time = DateTime.Now

        };

        var readdto = new LitterReadDto(litter);


        var readDtos = new List<LitterReadDto>
    {
            readdto

    };

        _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(new IdentityUser());

        _mockUserManager.Setup(m => m.GetRolesAsync(It.IsAny<IdentityUser>()))
            .ReturnsAsync(new List<string> { "User" });

        _mockRepo.Setup(r => r.Read()).ReturnsAsync(readDtos);

        // Act
        var result = await _controller.Read(null, null, null, null);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result); // hier Result gebruiken!
        var returnData = Assert.IsAssignableFrom<IEnumerable<LitterReadDto>>(okResult.Value);
        Assert.Single(returnData);
    }


    [Fact]
    public async Task Read_NoData_ReturnsNotFound()
    {
        _mockUserManager.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(new IdentityUser());

        _mockUserManager.Setup(m => m.GetRolesAsync(It.IsAny<IdentityUser>()))
            .ReturnsAsync(new List<string> { "User" });

        _mockRepo.Setup(r => r.Read()).ReturnsAsync((List<LitterReadDto>?)null);

        var result = await _controller.Read(null, null, null, "nonexistend");

        var notFound = Assert.IsType<ActionResult<LitterReadDto>>(result); // mischien is NotFoundObjectResult beter
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsOk()
    {
        int id = 5;

        _mockRepo.Setup(r => r.Delete(id)).Returns(Task.CompletedTask);

        var result = await _controller.Delete(id);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Contains("deleted successfully", okResult.Value.ToString());
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsBadRequest()
    {
        int id = -1;

        var result = await _controller.Delete(id);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Contains("Invalid litter ID", badRequest.Value.ToString());
    }

    [Fact]
    public async Task Delete_NonExistingId_ReturnsNotFound()
    {
        int id = 999;

        _mockRepo.Setup(r => r.Delete(id)).Throws(new KeyNotFoundException("Not found"));

        var result = await _controller.Delete(id);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Contains("Not found", notFound.Value.ToString());
    }

    [Fact]
    public async Task Delete_UnexpectedError_Returns500()
    {
        int id = 1;

        _mockRepo.Setup(r => r.Delete(id)).Throws(new Exception("Unexpected error"));

        var result = await _controller.Delete(id);

        var objResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, objResult.StatusCode);
        Assert.Contains("Unexpected error", objResult.Value.ToString());
    }
}
