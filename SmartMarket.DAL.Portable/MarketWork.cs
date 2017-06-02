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
        private IRepository<User> userRepository;

        private IRepository<Balance> balanceRepository;

        private IRepository<RegistredCredential> registredCredentialRepository;

        private IRepository<OAuthCredential> oAuthCredentialRepository;

        private IRepository<Role> roleRepository;

        // Lazy loadings for repositories/
        // Also we can use Lazy class.
        public IRepository<User> Users
           => userRepository ?? 
            (userRepository = new Repository<User>());

        public IRepository<Balance> Balances
            => balanceRepository ?? 
            (balanceRepository = new Repository<Balance>());

        public IRepository<RegistredCredential> RegistredCredentials
            => registredCredentialRepository ?? 
            (registredCredentialRepository = new Repository<RegistredCredential>());

        public IRepository<OAuthCredential> OAuthCredentials
            => oAuthCredentialRepository ?? 
            (oAuthCredentialRepository = new Repository<OAuthCredential>());

        public IRepository<Role> Roles
            => roleRepository ?? 
            (roleRepository = new Repository<Role>());
    }
}
