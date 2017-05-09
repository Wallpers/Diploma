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
    public class RegistryModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNames), Name = "Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNames), Name = "LastName")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNames), Name = "Email")]
        [EmailAddress(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "InvalidEmail")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNames), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "RequiredTemplate")]
        [Display(ResourceType = typeof(DisplayNames), Name = "PasswordConfirm")]
        [Compare("Password", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "PasswordMatch" )]
        public string PasswordConfirm { get; set; }

        public string StatusMessage { get; set; }

        public string RegistryTextButton
            => ResourceService.GetString(typeof(DisplayNames), "RegistryTextButton");
    }
}
