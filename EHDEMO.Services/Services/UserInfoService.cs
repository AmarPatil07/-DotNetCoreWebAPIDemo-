using AutoMapper;
using EHDEMO.Domain.Dtos;
using EHDEMO.Domain.Entities;
using EHDEMO.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace EHDEMO.Service.Services
{
    public class UserInfoService : IUserInfoService
    {

        private readonly IUserInfoRepository iUserInfoRepository;
        private readonly IMapper mapper;

        public UserInfoService(IUserInfoRepository iUserInfoRepository, IMapper mapper)
        {
            this.iUserInfoRepository = iUserInfoRepository;
            this.mapper = mapper;
        }

        public void DeleteUserInfo(int id)
        {
            if (id == 0)
                throw new ArgumentException("The User id can't be zero.");

            iUserInfoRepository.DeleteUserContact(id);
        }      

        public UserInfo GetUserContact(int id)
        {
            if (id == 0)
                throw new ArgumentException("The User id can't be zero.");

            return iUserInfoRepository.GetUser(id);
        }
        public List<UserInfo> GetAllUsers()
        {
            return iUserInfoRepository.GetAllUsers();
        }

        /// <summary>
        /// To Get User Contact Info Only
        /// We used AutoMapper to map entity to DTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserContactInfoDTO> GetUserContactInfo()
        {
            List<UserInfo> model = new List<UserInfo>();
            model = iUserInfoRepository.GetAllUsers();
            var result = mapper.Map<List<UserContactInfoDTO>>(model); 
            return result;
        }

        public UserInfo Post<V>(UserInfo userInfo) where V : AbstractValidator<UserInfo>
        {
            Validate(userInfo, Activator.CreateInstance<V>());

            iUserInfoRepository.Insert(userInfo);
            return userInfo;
        }

        public UserInfo Put<V>(UserInfo obj) where V : AbstractValidator<UserInfo>
        {
            throw new NotImplementedException();
        }

        private void Validate(UserInfo obj, AbstractValidator<UserInfo> validator)
        {
            if (obj == null)
                throw new Exception("records not found, Please provide valide input.");

            validator.ValidateAndThrow(obj);
        }

        
    }
}
