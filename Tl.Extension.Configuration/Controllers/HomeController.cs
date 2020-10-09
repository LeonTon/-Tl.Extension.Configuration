using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Tl.Extension.Configuration.Services;

namespace Tl.Extension.Configuration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IOptionsSnapshot<PersonConfig> _personOptionSnapshotConfig;
        private readonly IOptionsSnapshot<ContactListConfig> _contactOptionConfig;
        private readonly IOptionsSnapshot<Dictionary<string, string>> _dic;
        private readonly IOptions<PersonConfig> _personOptionConfig;
        private readonly IOptionsMonitor<PersonConfig> _personOptionMonitorConfig;
        private readonly IPersonService _personService;

        public HomeController(IConfiguration configuration,
             IOptionsSnapshot<PersonConfig> personOptionSnapshotConfig,
             IOptionsSnapshot<ContactListConfig> contactOptionConfig,
             IOptionsSnapshot<Dictionary<string, string>> dic,
             IOptions<PersonConfig> personOptionConfig,
             IOptionsMonitor<PersonConfig> personOptionMonitorConfig,
             IPersonService personService)
        {
            _configuration = configuration;
            _personOptionSnapshotConfig = personOptionSnapshotConfig;
            _contactOptionConfig = contactOptionConfig;
            _dic = dic;
            _personOptionConfig = personOptionConfig;
            _personOptionMonitorConfig = personOptionMonitorConfig;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _configuration["Key1"];
            var value = _configuration["person:age"];
            var valueList = _configuration.GetSection("Contacts").Get<IEnumerable<Contact>>();
            var dic = _configuration.GetSection("dic").Get<Dictionary<string, string>>();

            var key1Value = _configuration["Key1"];
            var b = _configuration.GetValue<string>("Key1");
            var c = _configuration["key1"];
           


            //从json 文件获取配置
            var jsonKeyValue = _configuration["JsonKey"];
            Console.WriteLine($"JsonKey读取配置值为：{jsonKeyValue}" + Environment.NewLine);

            var jsonArray = _configuration.GetSection("JsonArray").Get<List<Contact>>();
            Console.WriteLine($"JsonArray读取配置值为：{JsonConvert.SerializeObject(jsonArray)}" + Environment.NewLine);

            var jsonObject = _configuration.GetSection("JsonObject").Get<PersonConfig>();
            Console.WriteLine($"JsonArray读取配置值为：{JsonConvert.SerializeObject(jsonObject)}" + Environment.NewLine);

            var jsonPersonName = _configuration["JsonObject:name"];
            Console.WriteLine($"jsonPersonName：{JsonConvert.SerializeObject(jsonPersonName)}" + Environment.NewLine);


            //从xml 文件获取配置
            var xmlObject = _configuration.GetSection("XmlPerson").Get<PersonConfig>();
            Console.WriteLine($"xmlObject读取配置值为：{JsonConvert.SerializeObject(xmlObject)}" + Environment.NewLine);

            var xmlPersonName = _configuration["XmlPerson:Name"];
            Console.WriteLine($"xmlPersonName读取配置值为：{JsonConvert.SerializeObject(xmlPersonName)}" + Environment.NewLine);

            //从ini获取配置
            var iniObject = _configuration.GetSection("ini").Get<PersonConfig>();
            Console.WriteLine($"iniObject读取配置值为：{JsonConvert.SerializeObject(iniObject)}" + Environment.NewLine);

            var iniPersonName = _configuration["ini:Name"];
            Console.WriteLine($"iniPersonName读取配置值为：{JsonConvert.SerializeObject(iniPersonName)}" + Environment.NewLine);

            //test reloadOnChange
            //Thread.Sleep(3000);
            //var iniPersonNameAfterChange = _configuration["ini:Name"];
            //Console.WriteLine($"iniPersonNameAfterChange读取配置值为：{JsonConvert.SerializeObject(iniPersonNameAfterChange)}" + Environment.NewLine);

            //test changeToken
            var changeToken = _configuration.GetReloadToken();
            changeToken.RegisterChangeCallback(obj =>
            {
                ChangeCallBack();
            }, _configuration);

            //test custom conifg
            var customPersonName = _configuration["name"];
            Console.WriteLine($"customPersonName读取配置值为：{customPersonName}" + Environment.NewLine);


            //test option
            var personOptionSnapshotValue = _personOptionSnapshotConfig.Value;
            Console.WriteLine($"personOptionSnapshotValue：{JsonConvert.SerializeObject(personOptionSnapshotValue)}" + Environment.NewLine);

            var personOptionValue = _personOptionConfig.Value;
            Console.WriteLine($"personOptionValue：{JsonConvert.SerializeObject(personOptionValue)}" + Environment.NewLine);

            var personOptionMonitorValue = _personOptionMonitorConfig.CurrentValue;
            Console.WriteLine($"personOptionMonitorValue：{JsonConvert.SerializeObject(personOptionMonitorValue)}" + Environment.NewLine);

            var contactOptionValue = _contactOptionConfig.Value;
            Console.WriteLine($"contactOptionValue读取配置值为：{JsonConvert.SerializeObject(contactOptionValue)}" + Environment.NewLine);

            var dicValue = _dic.Value;
            Console.WriteLine($"dicValue读取配置值为：{JsonConvert.SerializeObject(dicValue)}" + Environment.NewLine);

            var personServiceConfig = _personService.GetPersonConfig();
            Console.WriteLine($"personServiceConfig读取配置值为：{personServiceConfig}" + Environment.NewLine);

            return new ContentResult();
        }


        public void ChangeCallBack()
        {
            Console.WriteLine("配置发生变化");
            Console.WriteLine($"最新的配置为：{JsonConvert.SerializeObject(_configuration.GetSection("ini").Get<PersonConfig>())}");

            //var changeToken = _configuration.GetReloadToken();
            //changeToken.RegisterChangeCallback(obj =>
            //{
            //    ChangeCallBack();
            //}, _configuration);
        }


    }
}
