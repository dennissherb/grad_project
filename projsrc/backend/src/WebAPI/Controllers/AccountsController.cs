using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Datalayer.Models;
using Datalayer.Repositories;
using System.Security.Cryptography;
using System.Text;
using DataObjects;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repository;

        public AccountsController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _repository.GetAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _repository.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] Account account)
        {
            var existingAccount = await _repository.GetAccountByEmailAsync(account.Email);

            if (existingAccount != null)
            {
                return BadRequest("Account already exists");
            }

            account.Salt = HelperFuncs.GenerateSalt(20);
            account.Password = HelperFuncs.CreateHash(account.Password, account.Salt);
            await _repository.CreateAccountAsync(account);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            await _repository.UpdateAccountAsync(account);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _repository.DeleteAccountAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login([FromBody] Account account)
        {
            var existingAccount = await _repository.GetAccountByEmailAsync(account.Email);

            if (existingAccount == null)
            {
                return NotFound("Account not found");
            }

            if (!HelperFuncs.VerifyPassword(account.Password, existingAccount.Password, existingAccount.Salt))
            {
                return BadRequest("Invalid password");
            }

            return Ok(existingAccount);
        }
    }
    public class HelperFuncs
    {
        static public bool VerifyPassword(string enteredPassword, string passwordHash, string salt)
        {
            // Convert the entered password and salt to bytes
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

            // Prepend the salt to the entered password
            byte[] saltedPasswordBytes = new byte[saltBytes.Length + enteredPasswordBytes.Length];
            Array.Copy(saltBytes, 0, saltedPasswordBytes, 0, saltBytes.Length);
            Array.Copy(enteredPasswordBytes, 0, saltedPasswordBytes, saltBytes.Length, enteredPasswordBytes.Length);

            // Compute the SHA256 hash of the salted password
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);

                // Convert the hashed bytes to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                string hashedPassword = builder.ToString();

                // Compare the hashed password with the stored password hash
                return hashedPassword == passwordHash;
            }
        }
        static public string CreateHash(string enteredPassword, string salt)
        {
            // Convert the entered password and salt to bytes
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);

            // Prepend the salt to the entered password
            byte[] saltedPasswordBytes = new byte[saltBytes.Length + enteredPasswordBytes.Length];
            Array.Copy(saltBytes, 0, saltedPasswordBytes, 0, saltBytes.Length);
            Array.Copy(enteredPasswordBytes, 0, saltedPasswordBytes, saltBytes.Length, enteredPasswordBytes.Length);

            // Compute the SHA256 hash of the salted password
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(saltedPasswordBytes);

                // Convert the hashed bytes to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                string hashedPassword = builder.ToString();

                // Compare the hashed password with the stored password hash
                return hashedPassword;
            }
        }
        static public string GenerateSalt(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder sb = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    sb.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }
            return sb.ToString();
        }
    }
}
