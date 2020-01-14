using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.alibaba.dubbo.service;
using com.eqying.pf.service.provider.api;

namespace dubbo_demo
{
    public partial class Form1 : Form
    {
        public static readonly log4net.ILog logger = log4net.LogManager.GetLogger("Form1");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                UserServiceI userService = (UserServiceI)ServiceConsumerContainer.Instance().GetHessianServices(typeof(UserServiceI).FullName);
                
                Console.WriteLine(userService.getUserInfo("123"));
                logger.Info(userService.getUserInfo("123"));

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }
        }
    }
}
