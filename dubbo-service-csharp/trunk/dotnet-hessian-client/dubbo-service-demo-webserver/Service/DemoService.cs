using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using com.alibaba.dubbo.common.attributes;
using com.alibaba.dubbo.service;
using hessiancsharp.server;

namespace com.eqying.pf.service.provider.api
{

    [Service(interfaceClass = typeof(IService), path = "hessiantest.hessian")]
    public class DemoService : CHessianHandler, IService
    {
        #region IDubboService 成员
        public string Hello(string name)
        {
            return "Hello " + name;
        }

        #endregion
    }
}