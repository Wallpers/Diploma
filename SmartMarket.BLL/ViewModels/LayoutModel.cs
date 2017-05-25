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
    public class LayoutModel
    {
        public int UserID { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "Name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "LastName")]
        public string LastName { get; set; }

        public IEnumerable<DisplayBalanceModel> BalanceModels { get; set; }

        public string CreateBalance
            => ResourceService.GetString(typeof(Strings), "CreateBalance");

        public string StatusMessage { get; set; }
    }
}
