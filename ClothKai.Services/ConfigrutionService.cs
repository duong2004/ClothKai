using ClothKai.Database;
using ClothKai.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothKai.Services
{
    public class ConfigrutionService
    {
        #region Singleton
        public static ConfigrutionService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigrutionService();
                return instance;
            }
        }
        private static ConfigrutionService instance { get; set; }
        private ConfigrutionService()
        {
        }
        #endregion
        public Config GetConfig(string Key)
        {
            using (var context = new CBContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}
