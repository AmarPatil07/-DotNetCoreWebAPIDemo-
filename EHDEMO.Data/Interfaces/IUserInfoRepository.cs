using EHDEMO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHDEMO.Domain.Interfaces
{
  public interface IUserInfoRepository
    {
        void Insert(UserInfo obj);

        void Update(UserInfo obj);

        void DeleteUserContact(int id);

        UserInfo GetUser(int id);

        List<UserInfo> GetAllUsers();
    }
}
