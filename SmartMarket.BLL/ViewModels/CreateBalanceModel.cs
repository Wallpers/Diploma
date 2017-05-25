using SmartMarket.BLL.Bank;
using SmartMarket.BLL.Resources;
using SmartMarket.BLL.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SmartMarket.BLL.ViewModels;
using System;

namespace SmartMarket.BLL.ViewModels
{
    public class CreateBalanceModel
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        [Display(ResourceType = typeof(DisplayNames), Name = "CardNumber")]
        public string CardNumber { get; set; }

        [Required]
        [Display(ResourceType = typeof(DisplayNames), Name = "Country")]
        public string Country { get; set; }

        public SelectList Countries 
            => new SelectList(Enum.GetValues(typeof(Country)));

        [Display(ResourceType = typeof(DisplayNames), Name = "MerchantID")]
        public int MerchantID
            => Private24.MerchantID;

        [Display(ResourceType = typeof(DisplayNames), Name = "MerchantPassword")]
        public string MerchantPassword
            => Private24.MerchantPassword;

        public string SubmitTextButton
            => ResourceService.GetString(typeof(Strings), "AttachCard");
    }
}
