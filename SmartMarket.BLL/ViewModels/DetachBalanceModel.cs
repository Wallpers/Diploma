using SmartMarket.BLL.Resources;
using SmartMarket.BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL.ViewModels
{
    public class DetachBalanceModel
    {
        public int BalanceID { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "CardNumber")]
        public string CardNumber { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "Balance")]
        public string Balance { get; set; }

        public string DetachConfirm
            => ResourceService.GetString(typeof(Strings), "DetachConfirm");

        public string DetachTextButton
            => ResourceService.GetString(typeof(Strings), "Yes");
    }
}
