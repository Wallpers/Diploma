using SmartMarket.BLL.ViewModels;
using SmartMarket.DAL;
using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserModel = SmartMarket.BLL.ViewModels.UserModel;
using Entity = SmartMarket.DAL.Entities.User;

using AutoMapper;
using System.Security.Policy;
using SmartMarket.BLL.Managers.Answers;
using SmartMarket.BLL.Services;
using SmartMarket.BLL.Resources;

namespace SmartMarket.BLL.Managers
{
    public class UserManager : IUserManager
    {
        IWork work = new MarketWork();

        public UserModel Create(RegistryModel model)
        {
            var entity = Mapper.Map<RegistryModel, Entity>(model);
            entity = work.Users.Create(entity);
            return Mapper.Map<Entity, UserModel>(entity);
        }

        public UserModel Get(int id)
        {
            var user = work.Users.Find(id);
            return Mapper.Map<Entity, UserModel>(user);
        }

        public void Update(UserModel user)
        {
            var entity = Mapper.Map<UserModel, Entity>(user);
            work.Users.Update(entity);
            UserService.Update(user);
        }

        public void Remove(UserModel user)
        {
            var entity = Mapper.Map<UserModel, Entity>(user);
            work.Users.Remove(entity);
        }

        public LoginAnswer Check(LoginModel model)
        {
            var passwordHash = SecurityService.GetPasswordHash(model.Password);
            var entity = work.Users.FirstOrDefault(user => user.Email == model.Email);

            return  (entity == null) ? LoginAnswer.EmailNotFound :
                    (!entity.IsEmailConfirmed) ? LoginAnswer.EmailNotConfirmed :
                    (entity.PasswordHash != passwordHash) ? LoginAnswer.PasswordWrong :
                                                        LoginAnswer.Access;


        }

        public void SendEmail(UserModel user, string body)
        {
            var tittle = ResourceService.GetString(typeof(Strings), "EmailConfirmHeader");
            EmailService.Send(user, tittle, body);
        }
        

        public string GenerateToken(UserModel user)
        {
            SecurityService.HashMode = HashMode.MD5;
            return SecurityService.GetHash(user.FullName);
        }

        public UserModel FirstOrDefault(Func<UserModel, bool> predicate)
        {
            var entities = work.Users.ToList();

            UserModel user;
            foreach (var entity in entities)
            {
                user = Mapper.Map<Entity, UserModel>(entity);
                if (predicate(user))
                {
                    return user;
                }
            }
            return new UserModel();
        }

        public bool IsEmailValid(string email)
        {
            var entity = work.Users.FirstOrDefault(user => user.Email == email);
            return entity == null;
        }
    }
}
