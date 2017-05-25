using System;
using System.ComponentModel.DataAnnotations;

namespace SmartMarket.BLL.ViewModels
{
    public class OAuthModel
    {
        public string Email { get; set; }
        public DateTime Expires { get; set; }
        public string Token { get; set; }
        public int Identifier { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}