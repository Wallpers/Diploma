using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMarket.DAL.Entities;

namespace SmartMarket.DAL
{
    public class MarketWork : IWork
    {
        private readonly MarketContext db = new MarketContext();

        private IRepository<User> userRepository;

        private IRepository<Role> roleRepository;

        // Lazy loadings for repositories/
        // Also we can use Lazy class.
        public IRepository<User> Users
            => userRepository ?? (userRepository = new Repository<User>());

        public IRepository<Role> Roles
            => roleRepository ?? (roleRepository = new Repository<Role>());
    }
}
