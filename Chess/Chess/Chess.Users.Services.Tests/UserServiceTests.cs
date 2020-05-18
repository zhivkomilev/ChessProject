using AutoMapper;
using Chess.Core.DataAccess;
using Chess.Core.Domain.Interfaces;
using Chess.Users.DataAccess.Entities;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Chess.Users.Services.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetByEmailAsync_PassNullForEmail_ShouldThrowArgumentNullException()
        {
            var mockRepository = new Mock<IRepository<User>>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(s => s.GetRepository<User>())
                .Returns(mockRepository.Object);

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var date = new DateTime(2020, 4, 20);
            dateTimeProvider.Setup(s => s.Now).Returns(date);

            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(unitOfWorkMock.Object, 
                                        dateTimeProvider.Object, 
                                        mapperMock.Object);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.GetByEmailAsync(null));

            Assert.Equal("email", ex.ParamName);
            Assert.IsType<ArgumentNullException>(ex);
        }

        [Fact]
        public async Task GetByEmailAsync_PassNonExistingEmail_ShouldReturnDefaultValueForUserModel()
        {
            var mockRepository = new Mock<IRepository<User>>();
            mockRepository.Setup(s => s.Get(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(Task.FromResult(default(User)));
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(s => s.GetRepository<User>())
                .Returns(mockRepository.Object);

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var date = new DateTime(2020, 4, 20);
            dateTimeProvider.Setup(s => s.Now).Returns(date);

            var mapperMock = new Mock<IMapper>();

            var userService = new UserService(unitOfWorkMock.Object,
                                        dateTimeProvider.Object,
                                        mapperMock.Object);

            var result = await userService.GetByEmailAsync("NonExistingEmail@Non.Existing");

            Assert.Equal(default, result);
        }
    }
}
