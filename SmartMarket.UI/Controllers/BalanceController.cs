using SmartMarket.BLL.Managers;
using SmartMarket.BLL.Managers.Answers;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartMarket.UI.Controllers
{
    // TODO Horrible must be refactored.
    public class BalancesController : ApiController
    {
        public List<BalanceTransfer> Get(int id)
        {
            return (new BalanceManager()).GetBalances(id);
        }
    }

}
