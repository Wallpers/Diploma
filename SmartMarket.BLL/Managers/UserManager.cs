using AutoMapper;
using SmartMarket.BLL.Managers.Answers;
using SmartMarket.BLL.Resources;
using SmartMarket.BLL.Services;
using SmartMarket.BLL.ViewModels;
using SmartMarket.DAL;
using SmartMarket.DAL.Entities;
using SmartMarket.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace SmartMarket.BLL.Managers
{
    public class UserManager : IUserManager
    {
        IWork work = new MarketWork();

        public LoginAnswer Login(LoginModel model)
        {
            var passwordHash = SecurityService.GetPasswordHash(model.Password);

            var user = work.Users.FirstOrDefault(
                entity => entity.Email == model.Email, 
                entity => entity.RegistredCredential, 
                entity => entity.OAuthCredential,
                entity => entity.Balances);

            if (user == null)
            {
                return LoginAnswer.EmailNotFound;
            }            

            var credentials = user.RegistredCredential;
            if (credentials == null)
            {
                return LoginAnswer.UserIsOAuth;
            }

            if (!credentials.IsEmailConfirmed)
            {
                return LoginAnswer.EmailNotConfirmed;
            }
            if (credentials.PasswordHash != passwordHash)
            {
                return LoginAnswer.PasswordWrong;
            }

            UserService.Refresh(user);
            return LoginAnswer.Access;
        }

        public int Create(RegistryModel model)
        {
            // TODO MAke right map.
            var user = Mapper.Map<RegistryModel, User>(model);
            user = work.Users.Create(user);
            return user.ID;
        }

        public bool IsEmailExists(string email)
        {
            var entity = work.Users.FirstOrDefault(user => user.Email == email);
            return entity != null;
        }

        public string GenerateToken(string name, string lastName)
        {
            SecurityService.HashMode = HashMode.MD5;
            return SecurityService.GetHash($"{name} {lastName}");
        }

        public void SendEmail(RegistryModel model, string body)
        {
            var tittle = ResourceService.GetString(typeof(Strings), "EmailConfirmHeader");
            EmailService.Send(model.Email, model.Name, tittle, body);
        }

        public bool ConfirmEmail(int id, string token)
        {
            var user = work.Users.Find(id, x => x.RegistredCredential);

            if (user != null)
            {
                if (GenerateToken(user.Name, user.LastName) == token)
                {

                    user.RegistredCredential.IsEmailConfirmed = true;
                    work.Users.Update(user);
                    return true;
                }
            }

            return false;
        }

        public OAuthModel CreateOrUpdate(OAuthModel user)
        {
            throw new NotImplementedException();
        }

        public void Update(EditModel model)
        {
            var user = Mapper.Map<EditModel, User>(model);
            work.Users.Update(user);
            UserService.Refresh(user);
        }

        internal User FirstOrDefault(Func<User, bool> predicate)
        {
            return work.Users.FirstOrDefault(predicate, x => x.Balances);
        }

        public int GetUserId(string email)
        {
            var user = work.Users.FirstOrDefault(entity => entity.Email == email);
            return user.ID;
        }
    }
}
