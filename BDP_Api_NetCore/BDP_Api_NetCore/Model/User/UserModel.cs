using BDP.Domain.Enum;

namespace BDP_Api_NetCore.Model.User
{
    public class UserModel
    {
        public Roles Role { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
