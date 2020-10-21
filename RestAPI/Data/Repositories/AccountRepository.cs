using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RestAPI.Data.Interfaces;
using RestAPI.Models;
//using Web.Domain.Interfaces.Repositories;

namespace RestAPI.Data.Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(WebContext context) : base(context)
        {

        }

        public int? Login(string email, string password)
        {
            password = Encrypt(password);
            return _dbSet.SingleOrDefault(u => u.EMail == email && u.Password == password)?.ID;
        }

        public override void Add(Account obj)
        {
            obj.Password = Encrypt(obj.Password);
            base.Add(obj);
        }

        private string Encrypt(string text)
        {
            using(SHA256Managed encryptor = new SHA256Managed())
            {
                UTF8Encoding utf = new UTF8Encoding();
                return Convert.ToBase64String(encryptor.ComputeHash(utf.GetBytes(text)));
            }
        }
    }
}
