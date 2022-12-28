using BDP.Domain.Enum;
using BDP_Api_NetCore.ViewModel.Account;

namespace BDP_Api_NetCore.ViewModel.User
{
    public class UserViewModel
    {
        public string Role { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public AccountViewModel Account { get; set; }
    }
}
