using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.eqying.pf.service.provider.api
{


    using Book = com.eqying.pf.service.provider.model.Book;

    /// <summary>
    /// TODO(这个类的作用)
    /// 
    /// @auther: renjunjie
    /// @since: 2016/12/12 14:07
    /// </summary>
    public interface BookServiceI
    {

        Book getBookById(string id);

    }
}
