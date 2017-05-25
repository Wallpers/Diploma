using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL
{
    public interface IModelManager
    {
        TModel GetModel<TModel>() where TModel : class;
    }
}
