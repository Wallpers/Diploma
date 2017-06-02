using SmartMarket.BLL;
using SmartMarket.BLL.Managers;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartMarket.UI.Controllers
{
    public class PaymentController : ApiController
    {
        IPaymentManager paymentManager = new PaymentManager();

        [HttpPost]
        public bool Transaction(PaymentModel model)
        {
            return paymentManager.Transact(model);
        }
    }
}
