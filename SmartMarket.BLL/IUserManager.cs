using SmartMarket.BLL.Managers.Answers;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL
{
    public interface IUserManager
    {
        UserModel Create(RegistryModel model);
        UserModel Get(int id);
        void Update(UserModel userVM);
        void Remove(UserModel userVM);
        LoginAnswer Check(LoginModel model);
        void SendEmail(UserModel user, string body);
        string GenerateToken(UserModel user);
        UserModel FirstOrDefault(Func<UserModel, bool> predicate);
        bool IsEmailValid(string email);
    }
}
