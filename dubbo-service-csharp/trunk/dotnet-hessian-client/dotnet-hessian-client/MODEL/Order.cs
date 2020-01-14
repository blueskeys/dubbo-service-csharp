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
    /// @since: 2016/12/12 11:08
    /// </summary>
    [Serializable]
    public class Order
    {

        private string id;
        private string orderName;
        private User user;

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


        public virtual string OrderName
        {
            get
            {
                return orderName;
            }
            set
            {
                this.orderName = value;
            }
        }


        public virtual User User
        {
            get
            {
                return user;
            }
            set
            {
                this.user = value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(id)}: {id}, {nameof(orderName)}: {orderName}, {nameof(user)}: {user}, {nameof(Id)}: {Id}, {nameof(OrderName)}: {OrderName}, {nameof(User)}: {User}";
        }
    }
}
