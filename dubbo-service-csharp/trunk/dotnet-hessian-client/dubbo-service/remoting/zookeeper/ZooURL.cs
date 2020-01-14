using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.alibaba.dubbo.common;

namespace com.alibaba.dubbo.remoting.zookeeper
{
    public class ZooURL
    {
        private string url;
        private int sessionTimeout;

        public ZooURL(string url)
        {
            this.url = url;
        }

        public ZooURL(string url, int sessionTimeout)
        {
            this.url = url;
            this.sessionTimeout = sessionTimeout;
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public int SessionTimeout
        {
            get
            {
                if (sessionTimeout == 0)
                {
                    sessionTimeout = Constants.DEFAULT_SESSION_TIMEOUT;
                }
                return sessionTimeout;
            }
            set { sessionTimeout = value; }
        }
    }
}
