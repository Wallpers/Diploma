using AutoMapper;
using SmartMarket.BLL.Services;
using SmartMarket.DAL;
using SmartMarket.DAL.Entities;
using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL.Managers
{
    public class ModelManager : IModelManager
    {
        IWork work = new MarketWork();

        public BalanceModel GetModelForBalance<BalanceModel>(int id) where BalanceModel : class
        {
            var balance = work.Balances.Find(id);
            return Mapper.Map<Balance, BalanceModel>(balance)
                    ?? Activator.CreateInstance(typeof(BalanceModel)) as BalanceModel;
        }

        public UserModel GetModelForUser<UserModel>() where UserModel : class
        {
            var user = UserService.CurrentUser;
            return Mapper.Map<User, UserModel>(user)
                    ?? Activator.CreateInstance(typeof(UserModel)) as UserModel;
        }
    }
}
