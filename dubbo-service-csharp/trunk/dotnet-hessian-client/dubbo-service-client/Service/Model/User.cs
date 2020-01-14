using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.eqying.pf.service.provider.model
{
    public class User
    {
        private String id;
        private String name;
        private DateTime birthDate;
        private long age;
        private List<String> address;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public long Age
        {
            get { return age; }
            set { age = value; }
        }

        public List<string> Address
        {
            get { return address; }
            set { address = value; }
        }

        public override string ToString()
        {
            return $"{nameof(id)}: {id}, {nameof(name)}: {name}, {nameof(birthDate)}: {birthDate}, {nameof(age)}: {age}, {nameof(address)}: {address}, {nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(BirthDate)}: {BirthDate}, {nameof(Age)}: {Age}, {nameof(Address)}: {Address}";
        }
    }
}
