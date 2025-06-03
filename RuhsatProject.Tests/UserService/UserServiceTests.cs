using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.DataAccess.Contexts;

namespace RuhsatProject.Tests
{
    public class UserServiceTests
    {
        private readonly RuhsatDbContext _context;

        public UserServiceTests()
        {
            // In-memory veritabanı kurulumu
            var serviceProvider = new ServiceCollection()
                .AddDbContext<RuhsatDbContext>(options =>
                    options.UseInMemoryDatabase("TestDatabase")) // In-memory database ismi
                .BuildServiceProvider();

            _context = serviceProvider.GetRequiredService<RuhsatDbContext>(); // Context'i al
        }

        [Fact]
        public async Task Should_Add_User_To_Database()
        {
            // Arrange
            var user = new User
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Picture = "default_picture.jpg"
            };

            // Act
            await _context.Users.AddAsync(user); // Veriyi ekle
            await _context.SaveChangesAsync(); // Veriyi kaydet

            // Assert
            var savedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "testuser");
            savedUser.Should().NotBeNull(); // User'ın veritabanında var olduğundan emin ol
            savedUser.Email.Should().Be("testuser@example.com"); // Email'in doğru olduğunu kontrol et
        }
    }
}
