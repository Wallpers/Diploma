using AutoMapper;
using SmartMarket.DAL.Entities;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entity = SmartMarket.DAL.Entities.User;
using UserModel = SmartMarket.BLL.ViewModels.UserModel;

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
                config.CreateMap<UserModel, Entity>().ConvertUsing<UserEntityConverter>();
                config.CreateMap<Entity, UserModel>().ConvertUsing<EntityUserConverter>();
                config.CreateMap<RegistryModel, Entity>().ConvertUsing<RegistryEntityConverter>();
            });
        }

        class RegistryEntityConverter : ITypeConverter<RegistryModel, Entity>
        {
            public Entity Convert(RegistryModel source, Entity destination, ResolutionContext context)
            {
                destination = new Entity();

                destination.Name = source.Name;
                destination.LastName = source.LastName;
                destination.Email = source.Email;

                var passwordHash = SecurityService.GetPasswordHash(source.Password);
                destination.PasswordHash = passwordHash;

                return destination;
            }
        }

        class EntityUserConverter : ITypeConverter<Entity, UserModel>
        {
            public UserModel Convert(Entity source, UserModel destination, ResolutionContext context)
            {
                destination = new UserModel();
                destination.ID = source.ID;
                destination.Name = source.Name;
                destination.LastName = source.LastName;
                destination.Email = source.Email;
                destination.IsEmailConfirmed = source.IsEmailConfirmed;
                destination.PasswordHash = source.PasswordHash;


                if (source.Roles != null)
                {
                    string[] roles = new string[source.Roles.Count];
                    var i = 0;
                    foreach (var role in source.Roles)
                    {
                        roles[i++] = role.Name;
                    }
                    destination.Roles = roles;
                }

                return destination;
            }
        }

        class UserEntityConverter : ITypeConverter<UserModel, Entity>
        {
            public Entity Convert(UserModel source, Entity destination, ResolutionContext context)
            {
                destination = new Entity();
                destination.ID = source.ID;
                destination.Name = source.Name;
                destination.LastName = source.LastName;
                destination.Email = source.Email;
                destination.IsEmailConfirmed = source.IsEmailConfirmed;

                if (source.Password == null && source.PasswordHash == null)
                {
                    throw new InvalidOperationException("Password is null. It is forbidden.");
                }
                else if (source.Password == null)
                {
                    destination.PasswordHash = source.PasswordHash;
                }
                else
                {
                    var passwordHash = SecurityService.GetPasswordHash(source.Password);
                    destination.PasswordHash = passwordHash;
                }

                if (source.Roles != null)
                {
                    List<Role> roles = new List<Role>();
                    foreach (var role in source.Roles)
                    {
                        roles.Add(new Role() { Name = role });
                    }
                    destination.Roles = roles;
                }

                return destination;
            }
        }
    }

    
}
