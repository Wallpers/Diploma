using SmartMarket.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DAL.Interfaces
{
    public interface IWork
    {
        IRepository<User> Users { get; }
    }
}
