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
    public class PaymentManager : IPaymentManager
    {
        IWork work = new MarketWork();

        public bool Transact(PaymentModel model)
        {
            try
            {
                var balance = work.Balances.Find(model.BalanceID);
                balance.Cash -= model.TotalPrice;
                work.Balances.Update(balance);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
