using SmartMarket.BLL.Resources;
using SmartMarket.BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNames), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "InvalidEmail")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNames), Name = "Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof(DisplayNames), Name = "RememberMe")]
        public bool RememberMe { get; set; }

        public string StatusMessage { get; set; }

        public string LoginTextButton
            => ResourceService.GetString(typeof(DisplayNames), "LoginTextButton");
    }
}
