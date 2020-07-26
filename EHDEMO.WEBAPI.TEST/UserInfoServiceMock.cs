using EHDEMO.Domain.Dtos;
using EHDEMO.Domain.Entities;
using EHDEMO.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace EHDEMO.WEBAPI.TEST
{
    public class UserInfoServiceMock : IUserInfoService
    {
        private readonly List<UserInfo> _userInfo;
    
        public UserInfoServiceMock()
        {
            _userInfo = new List<UserInfo>()
            {
                new UserInfo() { Id = 1, FirstName = "John", LastName = "Ray", Email = "John@gmail.com", IsActive = true, PhoneNumber = "9376767677"},
                new UserInfo() { Id = 2, FirstName = "Amar", LastName = "Patil", Email = "Amar@gmail.com", IsActive = true, PhoneNumber = "8376767677"},
                new UserInfo() { Id = 3, FirstName = "Jayant", LastName = "Naralikar", Email = "Jaynat@gmail.com", IsActive = true, PhoneNumber = "7376767677"},
                new UserInfo() { Id = 4, FirstName = "Mahendrasinh", LastName = "Dhoni", Email = "MS@gmail.com", IsActive = true, PhoneNumber = "4776767677"}
            };
        }
        public void DeleteUserInfo(int id)
        {
            var existing = _userInfo.First(a => a.Id == id);
            _userInfo.Remove(existing);
        }

        public List<UserInfo> GetAllUsers()
        {
            return _userInfo.ToList();
        }

        public UserInfo GetUserContact(int id)
        {
            return _userInfo.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<UserContactInfoDTO> GetUserContactInfo()
        {        
            var result = Mapper.Map<List<UserContactInfoDTO>>(_userInfo);
            return result;            
        }

        public UserInfo Post<V>(UserInfo obj) where V : AbstractValidator<UserInfo>
        {
            _userInfo.Add(obj);
            return obj;
        }

        public UserInfo Put<V>(UserInfo obj) where V : AbstractValidator<UserInfo>
        {
            foreach (var item in _userInfo)
            {
                if (obj.Id == item.Id)
                {
                    item.FirstName = obj.FirstName;
                    item.LastName = obj.LastName;
                    item.Email = obj.Email;
                    item.PhoneNumber = obj.PhoneNumber;
                    item.IsActive = obj.IsActive;
                }
            }
          return obj;
        }
    }
}
