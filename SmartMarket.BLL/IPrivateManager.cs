using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL
{
    public interface IPrivateManager
    {
        int CreateBalance(CreateBalanceModel model, decimal cash);
    }
}
