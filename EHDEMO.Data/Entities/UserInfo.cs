using System;
using System.Collections.Generic;
using System.Text;

namespace EHDEMO.Domain.Entities
{
    public class UserInfo : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
