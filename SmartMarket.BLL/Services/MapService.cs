using AutoMapper;
using SmartMarket.DAL.Entities;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;


namespace SmartMarket.BLL.Services
{
    public static class MapService
    {
        public static void Config()
        {
            UserConfigurations();
        }

        private static void UserConfigurations()
        {
            Mapper.Initialize(config => {
                config.CreateMap<RegistryModel, User>().ConvertUsing<RegistryModelUserConverter>();
                config.CreateMap<EditModel, User>().ConvertUsing<EditModelUserConverter>();
                config.CreateMap<OAuthModel, User>().ConvertUsing<OAuthModelUserConverter>();

                config.CreateMap<User, EditModel>().ConvertUsing<UserEditModelConverter>();
                config.CreateMap<User, OAuthModel>().ConvertUsing<UserOAuthModelConverter>();

                config.CreateMap<User, IndexModel>().ConvertUsing<UserIndexModelConverter>();
                config.CreateMap<Balance, DisplayBalanceModel>().ConvertUsing<BalanceDisplayBalanceModelConverter>();

                config.CreateMap<CreateBalanceModel, Balance>().ConvertUsing<CreateBalanceModelBalanceConverter>();

            });
        }

        class RegistryModelUserConverter : ITypeConverter<RegistryModel, User>
        {
            public User Convert(RegistryModel source, User destination, ResolutionContext context)
            {
                if (source == null)
                    return null;

                if (destination == null)
                    destination = new User();

                if(source.Name != null)
                    destination.Name = source.Name;

                if (source.LastName != null)
                    destination.LastName = source.LastName;

                if (source.Email != null)
                    destination.Email = source.Email;

                if (destination.RegistredCredential == null)
                    destination.RegistredCredential = new RegistredCredential();

                if (source.Password != null)
                {
                    var passwordHash = SecurityService.GetPasswordHash(source.Password);
                    destination.RegistredCredential.PasswordHash = passwordHash;
                }
                

                return destination;
            }
        }

        class OAuthModelUserConverter : ITypeConverter<OAuthModel, User>
        {
            public User Convert(OAuthModel source, User destination, ResolutionContext context)
            {
                if (source == null)
                    return null;

                if (destination == null)
                    destination = new User();

                if (source.Name != null)
                    destination.Name = source.Name;

                if (source.LastName != null)
                    destination.LastName = source.LastName;

                if (source.Email != null)
                    destination.Email = source.Email;

                if (destination.OAuthCredential == null)
                    destination.OAuthCredential = new OAuthCredential();

                if (source.Token != null)
                    destination.OAuthCredential.Token = source.Token;

                if (source.Expires != null)
                    destination.OAuthCredential.Expires = source.Expires;

                if (source.Identifier != 0)
                    destination.OAuthCredential.Identifier = source.Identifier;


                return destination;
            }
        }

        class EditModelUserConverter : ITypeConverter<EditModel, User>
        {
            public User Convert(EditModel source, User destination, ResolutionContext context)
            {
                if (source == null)
                    return null;

                if (destination == null)
                    destination = new User();

                if (source.ID != 0)
                    destination.ID = source.ID;

                if (source.Name != null)
                    destination.Name = source.Name;

                if (source.LastName != null)
                    destination.LastName = source.LastName;

                if (source.Email != null)
                    destination.Email = source.Email;

                return destination;
            }
        }

        class UserOAuthModelConverter : ITypeConverter<User, OAuthModel>
        {
            public OAuthModel Convert(User source, OAuthModel destination, ResolutionContext context)
            {
                if (source == null)
                    return null;

                if (destination == null)
                    destination = new OAuthModel();

                if (source.Name != null)
                    destination.Name = source.Name;

                if (source.LastName != null)
                    destination.LastName = source.LastName;

                if (source.Email != null)
                    destination.Email = source.Email;

                if (source.OAuthCredential == null)
                    return destination;

                if (source.OAuthCredential.Token != null)
                    destination.Token = source.OAuthCredential.Token;

                if (source.OAuthCredential.Expires != null)
                    destination.Expires = source.OAuthCredential.Expires;

                if (source.OAuthCredential.Identifier != 0)
                    destination.Identifier = source.OAuthCredential.Identifier;

                return destination;
            }
        }

        class UserEditModelConverter : ITypeConverter<User, EditModel>
        {
            public EditModel Convert(User source, EditModel destination, ResolutionContext context)
            {
                if (source == null)
                    return null;

                if (destination == null)
                    destination = new EditModel();

                if (source.ID != 0)
                    destination.ID = source.ID;

                if (source.Name != null)
                    destination.Name = source.Name;

                if (source.LastName != null)
                    destination.LastName = source.LastName;

                if (source.Email != null)
                    destination.Email = source.Email;


                return destination;
            }
        }

        class BalanceDisplayBalanceModelConverter : ITypeConverter<Balance, DisplayBalanceModel>
        {
            public DisplayBalanceModel Convert(Balance source, DisplayBalanceModel destination, ResolutionContext context)
            {
                if (source == null)
                    return null;

                if (destination == null)
                    destination = new DisplayBalanceModel();

                destination.Cash = source.Cash.ToString("C");
                destination.Currency = source.Currency.ToString().ToLower();

                return destination;
            }
        }

        class UserIndexModelConverter : ITypeConverter<User, IndexModel>
        {
            public IndexModel Convert(User source, IndexModel destination, ResolutionContext context)
            {
                if (source == null)
                    return null;

                if (destination == null)
                    destination = new IndexModel();

                if (source.ID != 0)
                    destination.UserID = source.ID;

                if (source.Name != null)
                    destination.Name = source.Name;

                if (source.LastName != null)
                    destination.LastName = source.LastName;

                if (source.Balances == null)
                    return destination;

                var balanceModels = new List<DisplayBalanceModel>();
                foreach (var balance in source.Balances)
                {
                    balanceModels.Add(Mapper.Map<Balance, DisplayBalanceModel>(balance));
                }
                destination.BalanceModels = balanceModels;

                return destination;
            }
        }

        class CreateBalanceModelBalanceConverter : ITypeConverter<CreateBalanceModel, Balance>
        {
            public Balance Convert(CreateBalanceModel source, Balance destination, ResolutionContext context)
            {
                if (source == null)
                    return null;

                if (destination == null)
                    destination = new Balance();

                if (source.UserID != 0)
                    destination.UserID = source.UserID;

                if (source.MerchantID != 0)
                    destination.MerchantID = source.MerchantID;

                if (source.MerchantPassword != null)
                    destination.MerchantPassword = source.MerchantPassword;

                if (source.CardNumber == null)
                    destination.CardNumber = source.CardNumber;

                switch(source.Country)
                {
                    case "UA":
                        destination.Currency = Currency.UA;
                        break;
                    case "RU":
                        destination.Currency = Currency.RU;
                        break;
                    case "US":
                        destination.Currency = Currency.US;
                        break;
                }

                return destination;
            }
        }
    }

}
