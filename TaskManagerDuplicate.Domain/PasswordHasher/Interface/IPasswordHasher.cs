using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDuplicate.Domain.PasswordHasher.Interface
{
    public interface IPasswordHasher
    {
        public interface IPasswordHasher
        {
            public string Encrypt(string password);
            public string Decrypt(string passwordHash);
        }
    }
}
