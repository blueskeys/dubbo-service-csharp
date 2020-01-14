using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dubbo_service.rpc.hessian
{
    class ServiceProvider
    {
        private string serviceName;
        private string path;
        private Type serviceType;
        private Attribute serviceAttribute;

        public string ServiceName
        {
            get { return serviceName; }
            set { serviceName = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public Type ServiceType
        {
            get { return serviceType; }
            set { serviceType = value; }
        }

        public Attribute ServiceAttribute
        {
            get { return serviceAttribute; }
            set { serviceAttribute = value; }
        }
    }
}
