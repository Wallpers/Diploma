using SmartMarket.DAL.Entities;
using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL
{
    public class MarketContext : DbContext
    {
        public MarketContext() : base(Strings.Connection)
        { }

        public new DbSet<T> Set<T>() where T : class, IEntity
        {
            return base.Set<T>();
        }

        public DbSet<Balance> Balances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OAuthCredential> OAuthCredentials { get; set; }
        public DbSet<RegistredCredential> RegistredCredentials { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
