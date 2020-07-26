using System;
using System.Collections.Generic;
using System.Text;

namespace EHDEMO.Domain.Dtos
{
  public  class UserContactInfoDTO
    {

        public string UserFullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
