using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tl.Extension.Configuration.CustomConfiguation
{
    public class CustomConfigProvider : ConfigurationProvider
    {

        public override void Load()
        {
            var configData = GetDataFromDb();
            this.Data= ConvertToDictionary(configData);
        }


        private CustomConfigModel GetDataFromDb()
        {
            return new CustomConfigModel
            {
                CurrentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                Name = "Custom Config Name",
                Age = 111,
                Address = new Address
                {
                    Province = "custom jiang su",
                    City = "custom su zhou",
                    Area = "custom yuan qu"
                }

            };
        }

        private Dictionary<string, string> ConvertToDictionary(CustomConfigModel data)
        {
            var dic = new Dictionary<string, string>();
            data.GetType().GetProperties();
            dic["name"] = data.Name;
            dic["age"] = data.Age.ToString();
            return dic;
        }

    }
}
