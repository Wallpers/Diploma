using SmartMarket.BLL.ViewModels;
using SmartMarket.DAL;
using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL.Managers
{
    //TODO horrible must be refactored.
    public class BalanceManager : IBalanceManager
    {
        IWork work = new MarketWork();

        public List<BalanceTransfer> GetBalances(int id)
        {
            var user = work.Users.Find(id, entity => entity.Balances);
            return user.Balances.Select(balance => new BalanceTransfer { ID = balance.ID, Cash = balance.Cash}).ToList();
        }
    }
}
