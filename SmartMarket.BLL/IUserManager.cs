using SmartMarket.BLL.Managers.Answers;
using SmartMarket.BLL.ViewModels;

namespace SmartMarket.BLL
{
    public interface IUserManager
    {
        LoginAnswer Login(LoginModel model);
        bool IsEmailExists(string email);
        int Create(RegistryModel model);
        string GenerateToken(string name, string lastName);
        void SendEmail(RegistryModel model, string body);
        bool ConfirmEmail(int id, string token);
        OAuthModel CreateOrUpdate(OAuthModel user);
        void Update(EditModel newModel);
        int GetUserId(string email);
    }
}