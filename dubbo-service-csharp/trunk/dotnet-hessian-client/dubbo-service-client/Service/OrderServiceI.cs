using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.eqying.pf.service.provider.api
{

    using Order = com.eqying.pf.service.provider.model.Order;

    /// <summary>
    /// TODO(这个类的作用)
    /// 
    /// @auther: renjunjie
    /// @since: 2016/12/12 11:05
    /// </summary>
    public interface OrderServiceI
    {

        Order getOrderById(string id);

    }
}
