using BDP.Domain.Entities.User;
using BDP.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDP.Application.Constant
{
    public static class SystemConstant
    {
        public static UserEntity SuperAdmin()
        {
            string system = "System";
            return new UserEntity()
            {
                Id = 0,
                RoleId = Roles.SuperAdmin,
                FirstName = system,
                MiddleName = system,
                LastName = system,
                EmailAddress = system,
                CreatedBy = system
            };
        }

    }
}
