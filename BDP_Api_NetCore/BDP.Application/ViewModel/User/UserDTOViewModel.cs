using BDP.Application.ViewModel.Account;
using BDP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Application.ViewModel.User
{
    public class UserDTOViewModel
    {
        public string Role { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public AccountDTOViewModel Account { get; set; }
    }
}
