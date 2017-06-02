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
        public int CreateBalance(AttachBalanceModel model)
        {
            var balance = Mapper.Map<AttachBalanceModel, Balance>(model);
            balance = work.Balances.Create(balance);
            UserService.Refresh();
            return balance.ID;
        }
    }
}
