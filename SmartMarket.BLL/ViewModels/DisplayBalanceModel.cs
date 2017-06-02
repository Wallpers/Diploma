using SmartMarket.BLL.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL.ViewModels
{
    public class DisplayBalanceModel
    {
        public int ID { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "Cash")]
        public string Cash { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "Currency")]
        public string Currency { get; set; }
    }
}
