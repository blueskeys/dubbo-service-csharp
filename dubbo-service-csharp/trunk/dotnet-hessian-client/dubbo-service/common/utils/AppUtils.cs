using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace com.alibaba.dubbo.common.utils
{
    public class AppUtils
    {
        public static bool IsWebApp()
        {
            bool flag = false;
            if (HttpContext.Current != null)
            {
                flag = true;
            }
            //否则是winform程序  
            return flag;
        }
    }
}
