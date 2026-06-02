using Microsoft.VisualStudio.TestTools.UnitTesting;
using NextHouse.Domain.Entities.Account;
using NextHouse.Domain.Exceptions;

namespace NextHouse.Tests.UnitTests.Domain.Account
{
    [TestClass]
    public class RoleTests
    {
        [TestMethod]
        public void Constructor_WithValidName_CreatesRole()
        {
            Role role = new Role("Administrador");

            Assert.IsNotNull(role);
            Assert.AreEqual("Administrador", role.Name);
            Assert.AreNotEqual(Guid.Empty, role.Id);
        }

        [TestMethod]
        public void Constructor_WithNullName_ThrowsBussinesRuleException()
        {
            bool exceptionThrown = false;

            try
            {
                _ = new Role(null!);
            }
            catch (BussinesRuleException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void Constructor_WithEmptyName_ThrowsBussinesRuleException()
        {
            bool exceptionThrown = false;

            try
            {
                _ = new Role(string.Empty);
            }
            catch (BussinesRuleException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod]
        public void Constructor_WithWhitespaceName_ThrowsBussinesRuleException()
        {
            bool exceptionThrown = false;

            try
            {
                _ = new Role("   ");
            }
            catch (BussinesRuleException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown);
        }
    }
}