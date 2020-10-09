using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tl.Extension.Configuration
{
    public class PersonConfig
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Address Address { get; set; }


        public List<Contact> Contacts { get; set; }
    }


    public class Address
    {
        public string Province { get; set; }

        public string City { get; set; }

        public string Area { get; set; }
    }

    public class Contact
    {
        public string name { get; set; }

        public string age { get; set; }
    }

    public class ContactListConfig : List<Contact>
    {
        public Contact this[string name]
            => this.FirstOrDefault(item => item.name == name);
    }
}
