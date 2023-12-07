using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datalayer.Queries;
using System;
using System.Threading.Tasks;

namespace AccountQueryTests
{
    [TestClass]
    public class AccountQueryTests
    {
        [TestMethod]
        public void Test1TryLogin_InvalidCredentials()
        {
            // Assuming you have invalid test credentials for login
            string invalidEmail = "invalid@example.com";
            string invalidPassword = "invalidPassword";

            bool loginResult = AccountQuery.TryLogin(invalidEmail, invalidPassword).Result;

            Assert.IsFalse(loginResult, "Login with invalid credentials should fail.");
        }

        [TestMethod]
        public void  Test2CreateAccount()
        {
            // Assuming you have test data for account creation
            string testEmail = "newuser@example.com";
            string testUserName = "NewUser";
            DateTime testDateOfBirth = DateTime.Parse("1990-01-01");
            string testPassword = "newPassword123";

            bool creationResult = AccountQuery.CreateAccount(testEmail, testUserName, testDateOfBirth, testPassword).Result;

            Assert.IsTrue(creationResult, "Account creation should succeed.");
        }

        [TestMethod]
        public void  Test3ReadAccountByEmail()
        {
            // Assuming you have a test email for reading an existing account
            string existingEmail = "newuser@example.com";

            var accountDetails = AccountQuery.ReadAccountByEmail(existingEmail).Result;

            Assert.IsNotNull(accountDetails, "Reading an existing account should return details.");
        }

        [TestMethod]
        public void  Test4UpdateAccount()
        {
            // Assuming you have a test email and updated details for an account
            string testEmail = "newuser@example.com";
            string updatedUserName = "UpdatedUserName";
            DateTime updatedDateOfBirth = DateTime.Parse("1985-05-05");
            string updatedPassword = "updatedPassword123";

            bool updateResult = AccountQuery.UpdateAccount(testEmail, updatedUserName, updatedDateOfBirth, updatedPassword).Result;

            Assert.IsTrue(updateResult, "Account update should succeed.");
        }

        [TestMethod]
        public void  Test5TryLogin_ValidCredentials()
        {
            // Assuming you have valid test credentials for login
            string validEmail = "newuser@example.com";
            string validPassword = "updatedPassword123";

            bool loginResult = AccountQuery.TryLogin(validEmail, validPassword).Result;

            Assert.IsTrue(loginResult, "Login with valid credentials should succeed.");
        }

        [TestMethod]
        public void  Test6DeleteAccount()
        {
            string testEmailToDelete = "newuser@example.com";

            bool deleteResult = AccountQuery.DeleteAccount(testEmailToDelete).Result;

            Assert.IsTrue(deleteResult, "Account deletion should succeed.");
        }

    }
}
