using SmartMarket.BLL.ViewModels;
using SmartMarket.DAL.Entities;
using AutoMapper;
using SmartMarket.DAL;
using SmartMarket.DAL.Interfaces;
using SmartMarket.BLL.Services;

namespace SmartMarket.BLL.Managers
{
    public class PrivateManager : IPrivateManager
    {
        IWork work = new MarketWork();
        public int CreateBalance(CreateBalanceModel model, decimal cash)
        {
            var balance = Mapper.Map<CreateBalanceModel, Balance>(model);
            balance.Cash = cash;
            balance = work.Balances.Create(balance);
            UserService.Refresh();
            return balance.ID;
        }
    }
}
