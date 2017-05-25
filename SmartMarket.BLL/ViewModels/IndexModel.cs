using SmartMarket.BLL.Resources;
using SmartMarket.BLL.Services;


namespace SmartMarket.BLL.ViewModels
{
    public class IndexModel : LayoutModel
    { 
        public string PleaseAutorize
            => ResourceService.GetString(typeof(Strings), "PleaseAuthorize");

        public string Authorized
            => ResourceService.GetString(typeof(Strings), "Authorized");

        public string NureHeader
            => ResourceService.GetString(typeof(Strings), "NureHeader");

        public string NureDescription
            => ResourceService.GetString(typeof(Strings), "NureDescription");

        public string TechnologiesTittle
            => ResourceService.GetString(typeof(Strings), "TechnologiesTittle");

        public string NureEnding
            => ResourceService.GetString(typeof(Strings), "NureEnding");

        public LoginModel LoginModel { get; set; }

        public RegistryModel RegistryModel { get; set; }
    }
}
