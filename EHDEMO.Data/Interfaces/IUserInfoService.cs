using EHDEMO.Domain.Dtos;
using EHDEMO.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EHDEMO.Domain.Interfaces
{
  public interface IUserInfoService
    {
        UserInfo Post<V>(UserInfo obj) where V : AbstractValidator<UserInfo>;

        UserInfo Put<V>(UserInfo obj) where V : AbstractValidator<UserInfo>;
        IEnumerable<UserContactInfoDTO> GetUserContactInfo();

        void DeleteUserInfo(int id);

        UserInfo GetUserContact(int id);

        List<UserInfo> GetAllUsers();

    }
}
