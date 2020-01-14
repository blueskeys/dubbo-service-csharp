using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace com.eqying.pf.service.provider.model
{

    /// <summary>
    /// TODO(这个类的作用)
    /// 
    /// @auther: renjunjie
    /// @since: 2016/12/12 14:07
    /// </summary>
    [Serializable]
    public class Book
    {

        private string id;
        private string name;

        public virtual string Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }


        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(id)}: {id}, {nameof(name)}: {name}, {nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
        }
    }
}

