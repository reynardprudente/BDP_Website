using BDP.Domain.Entities.Account;
using BDP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Domain.Entities.User
{
    public class UserEntity : BaseEntity
    {
        public Roles RoleId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public AccountEntity Account { get; set; }
    }
}
