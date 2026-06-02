using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NextHouse.Application.Contracts.Repositories;
using NextHouse.Application.UseCases.Roles.Commands.UpdateRolePermissions;
using NextHouse.Application.UseCases.Security.Roles.Commands.UpdateRolePermissions;
using NextHouse.Domain.Entities.Account;
using System.Collections.Generic;
using System.Linq;

namespace NextHouse.Tests.UnitTests.Application.Roles
{
    [TestClass]
    public class UpdateRolePermissionsUseCaseTests
    {
        private Mock<IRoleRepository> _roleRepository = null!;
        private UpdateRolePermissionsUseCase _useCase = null!;

        [TestInitialize]
        public void Setup()
        {
            _roleRepository = new Mock<IRoleRepository>();

            _useCase = new UpdateRolePermissionsUseCase(
                _roleRepository.Object);
        }

        [TestMethod]
        public async Task Handle_WithPermissions_ReplacesPermissions()
        {
            Guid roleId = Guid.NewGuid();

            UpdateRolePermissionsCommand command =
                new UpdateRolePermissionsCommand
                {
                    RoleId = roleId.ToString(),
                    PermissionIds = new List<string>
                    {
                        Guid.NewGuid().ToString(),
                        Guid.NewGuid().ToString()
                    }
                };

            await _useCase.Handle(command);

            _roleRepository.Verify(
                 x => x.DeleteRolePermissions(It.IsAny<List<RolePermission>>()),
                 Times.Once);

            _roleRepository.Verify(
                x => x.AddRolePermissions(
                It.Is<List<RolePermission>>(p => p.Count == 2)),
                Times.Once);
        }

        [TestMethod]
        public async Task Handle_WithEmptyPermissions_RemovesAllPermissions()
        {
            Guid roleId = Guid.NewGuid();

            UpdateRolePermissionsCommand command =
                new UpdateRolePermissionsCommand
                {
                    RoleId = roleId.ToString(),
                    PermissionIds = new List<string>()
                };

            await _useCase.Handle(command);

            _roleRepository.Verify(
                x => x.DeleteRolePermissions(It.IsAny<List<RolePermission>>()),
                Times.Once);

            _roleRepository.Verify(
                x => x.AddRolePermissions(
                    It.IsAny<List<RolePermission>>()),
                Times.Never);
        }

        [TestMethod]
        public async Task Handle_WithDuplicatedPermissions_AddsDistinctPermissions()
        {
            Guid roleId = Guid.NewGuid();
            Guid permissionId = Guid.NewGuid();

            UpdateRolePermissionsCommand command =
                new UpdateRolePermissionsCommand
                {
                    RoleId = roleId.ToString(),
                    PermissionIds = new List<string>
                    {
                        permissionId.ToString(),
                        permissionId.ToString(),
                        permissionId.ToString()
                    }
                };

            await _useCase.Handle(command);

            _roleRepository.Verify(
                x => x.AddRolePermissions(
                    It.Is<List<RolePermission>>(p =>
                        p.Count == 1)),
                Times.Once);
        }

        [TestMethod]
        public async Task Handle_WhenDeleteRolePermissionsThrowsException_RethrowsException()
        {
            _roleRepository
                .Setup(x =>
                    x.DeleteRolePermissions(
                        It.IsAny<List<RolePermission>>()))
                .ThrowsAsync(
                    new InvalidOperationException());

            UpdateRolePermissionsCommand command =
                new UpdateRolePermissionsCommand
                {
                    RoleId = Guid.NewGuid().ToString(),
                    PermissionIds = new List<string>
                    {
                        Guid.NewGuid().ToString()
                    }
                };

            bool seLanzoExcepcion = false;
            try
            {
                await _useCase.Handle(command);
            }
            catch (InvalidOperationException)
            {
                seLanzoExcepcion = true;
            }

            Assert.IsTrue(seLanzoExcepcion, "Se esperaba InvalidOperationException.");
        }

        [TestMethod]
        public async Task Handle_WhenAddRolePermissionsThrowsException_RethrowsException()
        {
            _roleRepository
                .Setup(x =>
                    x.AddRolePermissions(
                        It.IsAny<List<RolePermission>>()))
                .ThrowsAsync(
                    new InvalidOperationException());

            UpdateRolePermissionsCommand command =
                new UpdateRolePermissionsCommand
                {
                    RoleId = Guid.NewGuid().ToString(),
                    PermissionIds = new List<string>
                    {
                        Guid.NewGuid().ToString()
                    }
                };

            bool seLanzoExcepcion = false;
            try
            {
              
                await _useCase.Handle(command);
            }
            catch (InvalidOperationException)
            {
                seLanzoExcepcion = true;
            }

            Assert.IsTrue(seLanzoExcepcion, "Se esperaba InvalidOperationException.");
        }

        [TestMethod]
        public async Task Handle_WithSinglePermission_AddsOnePermission()
        {
            Guid roleId = Guid.NewGuid();
            Guid permissionId = Guid.NewGuid();

            UpdateRolePermissionsCommand command =
                new UpdateRolePermissionsCommand
                {
                    RoleId = roleId.ToString(),
                    PermissionIds = new List<string>
                    {
                        permissionId.ToString()
                    }
                };

            await _useCase.Handle(command);

            _roleRepository.Verify(
                x => x.AddRolePermissions(
                    It.Is<List<RolePermission>>(p =>
                        p.Count == 1)),
                Times.Once);
        }
    }
}