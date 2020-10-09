using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tl.Extension.Configuration.Services
{
    public class PersonService : IPersonService
    {
        private readonly IOptionsSnapshot<PersonConfig> _personOption;
        public PersonService(IOptionsSnapshot<PersonConfig> personOption) 
        {
            _personOption = personOption;
        }

        public string GetPersonConfig()
        {
            var val = _personOption.Value;
            return JsonConvert.SerializeObject(val);
        }
    }
}
