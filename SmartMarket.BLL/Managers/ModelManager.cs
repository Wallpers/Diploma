using AutoMapper;
using SmartMarket.BLL.Services;
using SmartMarket.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL.Managers
{
    public class ModelManager : IModelManager
    {
        public TModel GetModel<TModel>() where TModel : class
        {
            var user = UserService.CurrentUser;
            return Mapper.Map<User, TModel>(user)
                    ?? Activator.CreateInstance(typeof(TModel)) as TModel;
        }

    }
}
