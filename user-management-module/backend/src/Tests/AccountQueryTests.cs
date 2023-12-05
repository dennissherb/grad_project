using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryClasses;
using System;
using System.Threading.Tasks;

namespace AccountQueryTests
{
    [TestClass]
    public class AccountQueryTests
    {
        [TestMethod]
        public async Task Test1TryLogin_InvalidCredentials()
        {
            // Assuming you have invalid test credentials for login
            string invalidEmail = "invalid@example.com";
            string invalidPassword = "invalidPassword";

            bool loginResult = await AccountQuery.TryLogin(invalidEmail, invalidPassword);

            Assert.IsFalse(loginResult, "Login with invalid credentials should fail.");
        }

        [TestMethod]
        public async Task Test2CreateAccount()
        {
            // Assuming you have test data for account creation
            string testEmail = "newuser@example.com";
            string testUserName = "NewUser";
            DateTime testDateOfBirth = DateTime.Parse("1990-01-01");
            string testPassword = "newPassword123";

            bool creationResult = await AccountQuery.CreateAccount(testEmail, testUserName, testDateOfBirth, testPassword);

            Assert.IsTrue(creationResult, "Account creation should succeed.");
        }

        [TestMethod]
        public async Task Test3ReadAccountByEmail()
        {
            // Assuming you have a test email for reading an existing account
            string existingEmail = "newuser@example.com";

            var accountDetails = await AccountQuery.ReadAccountByEmail(existingEmail);

            Assert.IsNotNull(accountDetails, "Reading an existing account should return details.");
        }

        [TestMethod]
        public async Task Test4UpdateAccount()
        {
            // Assuming you have a test email and updated details for an account
            string testEmail = "newuser@example.com";
            string updatedUserName = "UpdatedUserName";
            DateTime updatedDateOfBirth = DateTime.Parse("1985-05-05");
            string updatedPassword = "updatedPassword123";

            bool updateResult = await AccountQuery.UpdateAccount(testEmail, updatedUserName, updatedDateOfBirth, updatedPassword);

            Assert.IsTrue(updateResult, "Account update should succeed.");
        }

        [TestMethod]
        public async Task Test5TryLogin_ValidCredentials()
        {
            // Assuming you have valid test credentials for login
            string validEmail = "newuser@example.com";
            string validPassword = "updatedPassword123";

            bool loginResult = await AccountQuery.TryLogin(validEmail, validPassword);

            Assert.IsTrue(loginResult, "Login with valid credentials should succeed.");
        }

        [TestMethod]
        public async Task Test6DeleteAccount()
        {

            string testEmailToDelete = "newuser@example.com";

            bool deleteResult = await AccountQuery.DeleteAccount(testEmailToDelete);

            Assert.IsTrue(deleteResult, "Account deletion should succeed.");
        }

    }
}
