using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL
{
    public interface IModelManager
    {
        UserModel GetModelForUser<UserModel>() where UserModel : class;
        BalanceModel GetModelForBalance<BalanceModel>(int id) where BalanceModel : class;
    }
}
