using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.eqying.pf.service.provider.model;
using dotnet_hessian_client.DAL;

namespace com.eqying.pf.service.provider.api
{
    public interface UserServiceI
    {
        User getUserInfo(String userId);
    }
}
