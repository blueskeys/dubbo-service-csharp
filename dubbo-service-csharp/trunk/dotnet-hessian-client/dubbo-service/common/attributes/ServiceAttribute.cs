using System;

namespace com.alibaba.dubbo.common.attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : Attribute
    {
        public Type interfaceClass;

        public string interfaceName  = "";

        public string version  = "";

        public string group  = "";

        public string path  = "";

        public bool export  = false;

        public string token  = "";

        public bool deprecated  = false;

        public bool dynamic  = false;

        public string accesslog  = "";

        public int executes  = 0;

        public bool register  = false;

        public int weight  = 0;

        public string document  = "";

        public int delay  = 0;

        public string local  = "";

        public string stub  = "";

        public string cluster  = "";

        public string proxy  = "";

        public int connections  = 100;

        public int callbacks  = 0;

        public string onconnect  = "";

        public string ondisconnect  = "";

        public string owner  = "";

        public string layer  = "";

        public int retries  = 2;

        public string loadbalance  = "";

        public bool async  = false;

        public int actives  = 0;

        public bool sent  = false;

        public string mock  = "";

        public string validation  = "";

        public int timeout  = 1000;

        public string cache  = "";

        public string[] filter  = new string[0];

        public string[] listener  = new string[0];

        public string[] parameters  = new string[0];

        public string application  = "";

        public string module  = "";

        public string provider  = "";

        public string[] protocol  = new string[0];

        public string monitor  = "";

        public string[] registry  = new string[0];



    }
}