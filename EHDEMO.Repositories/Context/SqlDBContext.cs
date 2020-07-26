using EHDEMO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EHDEMO.Repo.Context
{
  public class SqlDBContext : DbContext
    {
        public SqlDBContext()
        {
        }

        public SqlDBContext(DbContextOptions<SqlDBContext> options) : base(options)
        {

        }

        public virtual DbSet<UserInfo> UserInfo { get; set; }
 
    }
}
