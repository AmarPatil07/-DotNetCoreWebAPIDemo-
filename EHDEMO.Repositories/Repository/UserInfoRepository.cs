using EHDEMO.Domain.Entities;
using EHDEMO.Domain.Interfaces;
using EHDEMO.Repo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHDEMO.Repo.Repository
{
    public class UserInfoRepository : IUserInfoRepository
    {
        private SqlDBContext context;

        public UserInfoRepository(SqlDBContext context)
        {
            this.context = context;
        }
        public void DeleteUserContact(int id)
        {
            context.Set<UserInfo>().Remove(GetUser(id));
            context.SaveChanges();
        }

        public List<UserInfo> GetAllUsers()
        {
            return context.UserInfo.ToList();       
        }

        public UserInfo GetUser(int id)
        {
            return context.Set<UserInfo>().Find(id);
        }

        public void Insert(UserInfo obj)
        {
            context.Set<UserInfo>().Add(obj);
            context.SaveChanges();
        }

        public void Update(UserInfo obj)
        {
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }     
        

    }
}
