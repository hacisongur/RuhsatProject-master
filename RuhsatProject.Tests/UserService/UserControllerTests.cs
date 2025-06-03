using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RuhsaProject.Business.IServices;
using RuhsaProject.DTOs.RoleDtos;
using RuhsaProject.DTOs.UserDtos;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.WebUI.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Umbraco.Core.Services;
using Xunit;

namespace RuhsatProject.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IWebHostEnvironment> _mockEnvironment;
        private readonly Mock<SignInManager<User>> _mockSignInManager;
        private readonly Mock<RoleManager<Role>> _mockRoleManager;
        private readonly Mock<ILogService> _mockLogService;
        private readonly Mock<ILogger<UserController>> _mockLogger;

        private readonly UserController _controller;

        public UserControllerTests()
        {
            // Mocking dependencies
            _mockUserService = new Mock<IUserService>();
            _mockEnvironment = new Mock<IWebHostEnvironment>();
            _mockSignInManager = new Mock<SignInManager<User>>(Mock.Of<IUserStore<User>>());
            _mockRoleManager = new Mock<RoleManager<Role>>(Mock.Of<IRoleStore<Role>>());
            _mockLogService = new Mock<ILogService>();
            _mockLogger = new Mock<ILogger<UserController>>();

            // Controller'ı mocklarla başlatıyoruz
            _controller = new UserController(
                _mockUserService.Object,
                _mockEnvironment.Object,
                _mockSignInManager.Object,
                _mockRoleManager.Object,
                _mockLogService.Object
            );
        }

        // Test 1: Index view'ını doğrulama (kullanıcı listesiyle)
        [Fact]
        public async Task Should_Return_Index_View_With_Users()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetUsers()).ReturnsAsync(new List<User>
            {
                new User { UserName = "testuser1", Email = "test1@example.com" },
                new User { UserName = "testuser2", Email = "test2@example.com" }
            });

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
            var model = viewResult.Model as UserListDto;
            model.Should().NotBeNull();
            model.Users.Should().HaveCount(2);
        }

        // Test 2: Yeni Kullanıcı Oluşturma
        [Fact]
        public async Task Should_Create_New_User()
        {
            // Arrange
            var userAddDto = new UserAddDto
            {
                UserName = "newuser",
                Email = "newuser@example.com",
                Password = "Password123"
            };

            // Act
            var result = await _controller.Create(userAddDto);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            redirectResult.Should().NotBeNull();
            redirectResult.ActionName.Should().Be("Index");
        }

        // Test 3: Login işlemi doğrulama
        [Fact]
        public async Task Should_Login_User_Successfully()
        {
            // Arrange
            var userLoginDto = new UserLoginDto
            {
                Email = "testuser@example.com",
                Password = "password123"
            };

            var user = new User { UserName = "testuser", Email = "testuser@example.com" };
            _mockSignInManager.Setup(sm => sm.PasswordSignInAsync(user, userLoginDto.Password, true, false))
                .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _controller.Login(userLoginDto);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            redirectResult.Should().NotBeNull();
            redirectResult.ActionName.Should().Be("Index");
        }

        // Test 4: Logout işlemi doğrulama
        [Fact]
        public async Task Should_Logout_User_Successfully()
        {
            // Arrange
            var userId = "testuser123";

            // Act
            var result = await _controller.Logout();

            // Assert
            var redirectResult = result as RedirectToActionResult;
            redirectResult.Should().NotBeNull();
            redirectResult.ActionName.Should().Be("Login");
        }

        // Test 5: Kullanıcıya Rol Atama
        [Fact]
        public async Task Should_Assign_Role_To_User()
        {
            // Arrange
            var roleAssignDto = new UserRoleAssignDto
            {
                UserId = "testuser123",
                RoleAssignDtos = new List<RoleAssignDto>
                {
                    new RoleAssignDto { RoleName = "Admin", HasRole = false }
                }
            };

            _mockRoleManager.Setup(rm => rm.AddToRoleAsync(It.IsAny<User>(), "Admin"))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Assign(roleAssignDto);

            // Assert
            var redirectResult = result as RedirectToActionResult;
            redirectResult.Should().NotBeNull();
            redirectResult.ActionName.Should().Be("Index");
        }
    }
}
