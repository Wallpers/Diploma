using SmartMarket.BLL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.BLL.Services
{
    public static class ResourceService
    {
        private static ResourceManager resourceManager;

        public static string GetString(Type type, string name)
        {
            if (resourceManager == null || resourceManager.ResourceSetType != type)
            {
                resourceManager = new ResourceManager(type);
            }

            return resourceManager.GetString(name);
        }
    }
}
