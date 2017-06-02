using SmartMarket.BLL.Managers;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL
{
    // TODO Make through tokens and id`s.
    public interface IBalanceManager
    {
        List<BalanceTransfer> GetBalances(int id);
    }
}
