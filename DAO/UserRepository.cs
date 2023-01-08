using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DAO
{
    public class UserRepository
    {
        private readonly cargageEntities db;
        public UserRepository()
        {
            db = new cargageEntities();
        }

        public List<User> Get()
        {
            return db.User.ToList();
        }
        public User Get(int id)
        {
            return db.User.FirstOrDefault(x => x.id == id);
        }

        public User Get(string login)
        {
            return db.User.FirstOrDefault(x => x.login == login);
        }

        public User Get(string login, string password)
        {
            var user = Get(login);
            if (user?.password == CreateMD5Hash(password))
                return user;
            return null;
        }

        public User Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var u = Get(user.login);
            if (u != null)
                throw new DuplicateWaitObjectException($"login {user.login} already exist !");

            user.password = CreateMD5Hash(user.password);

            user = db.User.Add(user);
            db.SaveChanges();

            return user;
        }

        public User Set(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var currentDb = new cargageEntities();
            var oldUser = currentDb.User.Find(user.id);

            if (oldUser == null)
                throw new KeyNotFoundException($"User not found !");

            var u = currentDb.User.FirstOrDefault(x => x.login == user.login);
            if (u != null && u.id != oldUser.id)
                throw new DuplicateWaitObjectException($"login {user.login} already exist !");

            user.password = !string.IsNullOrEmpty(user.password) && oldUser.password != user.password ? CreateMD5Hash(user.password) : oldUser.password;

            db.Entry(user).State = System.Data.EntityState.Modified;
            db.SaveChanges();

            return user;
        }

        private string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes =Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
