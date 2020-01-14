using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.alibaba.dubbo.service;
using com.eqying.pf.service.provider.api;

namespace dubbo_service_client
{
    class DubboServiceClient
    {
        public void invoke()
        {
            try
            {
                UserServiceI userservice = (UserServiceI)ServiceConsumerContainer.Instance().GetHessianServices(typeof(UserServiceI).FullName);
                Console.WriteLine(userservice.getUserInfo("UserServiceI"));

                //IService iService = (IService)ServiceConsumerContainer.Instance().GetHessianServices(typeof(IService).FullName);
                //Console.WriteLine(iService.Hello("任俊杰"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            
        }
    }
}
