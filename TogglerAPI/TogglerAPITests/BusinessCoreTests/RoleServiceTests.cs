using Moq;
using System.Collections.Generic;
using TogglerAPI.BusinessCore;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.RequestModels;
using Xunit;

namespace TogglerAPITests.BusinessCoreTests
{
    public class RoleServiceTests
    {
        private readonly RoleModel RoleModel = new RoleModel()
        {
            RoleId = 100,
            Name = "RoleModel",
            Description = "DescriptionRoleModel",
        };

        private readonly RoleRequestModel RoleRequestModel = new RoleRequestModel()
        {
            RoleId = 200,
            Name = "RoleRequestModel",
            Description = "DescriptionRoleRequestModel",
        };

        [Fact]
        public void CreateRole_ReturnsOne_WhenPassingCorrectObject()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(r => r.CreateRole(It.IsAny<RoleModel>())).Returns(1);

            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            int result = roleService.CreateRole(RoleRequestModel);

            Assert.Equal(1, result);
        }

        [Fact]
        public void CreateRole_ReturnsNegativeNumber_WhenPassingNullObject()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            int result = roleService.CreateRole(null);

            Assert.Equal(-1, result);
        }

        [Fact]
        public void CreateRole_ReturnsNegativeNumber_WhenPassingEmptyStringsInTheObject()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            int result = roleService.CreateRole(new RoleRequestModel());

            Assert.Equal(-1, result);
        }

        [Fact]
        public void DeleteRole_ReturnsTrue()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(r => r.DeleteRole(It.IsAny<int>())).Returns(true);

            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);

            Assert.True(roleService.DeleteRole(100));
        }

        [Fact]
        public void GetRole_ReturnsObject_When100IsPassed()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(r => r.GetRole(It.IsAny<int>())).Returns(RoleModel);

            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            var result = roleService.GetRole(100);

            Assert.Equal(result.RoleId, RoleModel.RoleId);
            Assert.Equal(result.Name, RoleModel.Name);
            Assert.Equal(result.Description, RoleModel.Description);
        }

        [Fact]
        public void GetRole_ReturnsNull_WhenRoleDoesntExist()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(r => r.GetRole(It.IsAny<int>())).Returns((RoleModel)null);

            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            var result = roleService.GetRole(100);

            Assert.Null(result);
        }

        [Fact]
        public void GetRoleList_ReturnsList()
        {
            List<RoleModel> roleModelList = new List<RoleModel>
            {
                RoleModel
            };

            var roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(r => r.GetRoleList()).Returns(roleModelList);

            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            var result = roleService.GetRoleList();

            Assert.Equal(result[0].RoleId, RoleModel.RoleId);
            Assert.Equal(result[0].Name, RoleModel.Name);
            Assert.Equal(result[0].Description, RoleModel.Description);
        }

        [Fact]
        public void UpdateRole_ReturnsTrue_WhenRoleRequestModelIsValid()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(r => r.UpdateRole(It.IsAny<RoleModel>())).Returns(true);

            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            var result = roleService.UpdateRole(RoleRequestModel);

            Assert.True(result);
        }

        [Fact]
        public void UpdateRole_ReturnsFalse_WhenRoleRequestModelIsNull()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            var result = roleService.UpdateRole(null);

            Assert.False(result);
        }

        [Fact]
        public void UpdateRole_ReturnsFalse_WhenRoleRequestModelHasNullRoleId()
        {
            var roleRepositoryMock = new Mock<IRoleRepository>();
            var loggerMock = new Mock<ILogger>();

            RoleService roleService = new RoleService(roleRepositoryMock.Object, loggerMock.Object);
            var result = roleService.UpdateRole(new RoleRequestModel());

            Assert.False(result);
        }
    }
}
