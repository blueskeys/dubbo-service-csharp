using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.eqying.pf.service.provider.model.DAL;
using dotnet_hessian_client.zookeeper;

namespace com.eqying.pf.service.provider.model
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpInvoke.Instance().GetUerInfo(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ZookeeperDemo zkDemo = new ZookeeperDemo();
            zkDemo.Init();
        }
    }
}
