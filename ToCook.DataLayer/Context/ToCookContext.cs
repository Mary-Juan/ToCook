using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToCook.DataLayer.Entities.User;

namespace ToCook.DataLayer.Context
{
    public class ToCookContext : DbContext
    {
        public ToCookContext(DbContextOptions<ToCookContext> options) :base(options)
        {
                
        }

        #region User
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UsersRole> UseresRoles { get; set; }
        #endregion 
    }
}
