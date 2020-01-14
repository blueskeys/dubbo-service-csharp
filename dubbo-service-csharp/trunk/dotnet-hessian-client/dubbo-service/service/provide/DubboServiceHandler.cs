using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using com.wyying.service.rpc.hessian;

namespace com.alibaba.dubbo.service
{
    public class DubboServiceHandler : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            IHttpHandler handler = HessianFactory.Instance().GetHttpHandler(url);

            if (handler != null)
            {
                return handler;
            }

            return null;
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            return;
        }
    }
}
