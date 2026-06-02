using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextHouse.Domain.Entities.Account;
using NextHouse.Domain.Exceptions;

namespace NextHouse.Tests.UnitTests.Domain.Account
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void Reconstitute_WithValidData_ReturnsUser()
        {
            Guid roleId = Guid.NewGuid();

            User user =
                User.Reconstitute(
                    "1",
                    "Juan",
                    "Penagos",
                    "juanp",
                    "juan@test.com",
                    true,
                    "3001234567",
                    roleId);

            Assert.IsNotNull(user);
            Assert.AreEqual("Juan", user.FisrtName);
            Assert.AreEqual("Penagos", user.LastName);
            Assert.AreEqual(roleId, user.RoleId);
        }

        [TestMethod]
        public void Reconstitute_WithEmptyId_ThrowsBussinesRuleException()
        {
            bool exceptionThrown = false;

            try
            {
                User.Reconstitute(
                    "",
                    "Juan",
                    "Penagos",
                    "juanp",
                    "juan@test.com",
                    true,
                    null,
                    Guid.NewGuid());
            }
            catch (BussinesRuleException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void Reconstitute_WithEmptyRoleId_ThrowsBussinesRuleException()
        {
            bool exceptionThrown = false;

            try
            {
                User.Reconstitute(
                    "1",
                    "Juan",
                    "Penagos",
                    "juanp",
                    "juan@test.com",
                    true,
                    null,
                    Guid.Empty);
            }
            catch (BussinesRuleException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }
    }
}