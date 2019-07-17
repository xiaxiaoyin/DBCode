using DBCode.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxDBType.Text))
            {
                MessageBox.Show("请选择数据库类型");
                return;
            }
            if (string.IsNullOrEmpty(txtConnection.Text))
            {
                MessageBox.Show("请填写数据库连接信息");
                return;
            }
            //if (string.IsNullOrEmpty(txtNameSpace.Text))
            //{
            //    MessageBox.Show("请填写类命名空间");
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtLocation.Text))
            //{
            //    MessageBox.Show("请填写类文件存放路径");
            //    return;
            //}  
            DBType dBType = 0;
            switch (cbxDBType.Text)
            {
                case "Sql Server":
                    dBType = DBType.SqlServer;
                    break;
                case "Oracle":
                    dBType = DBType.Oracle;
                    break;
                case "MySql":
                    dBType = DBType.Oracle;
                    break;
            }
            SelectTable selectTable = new SelectTable(dBType, txtConnection.Text);
            selectTable.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxDBType.Text))
            {
                MessageBox.Show("请选择数据库类型");
                return;
            }
            if (string.IsNullOrEmpty(txtConnection.Text))
            {
                MessageBox.Show("请填写数据库连接信息");
                return;
            }
            DBType dBType = 0;
            switch (cbxDBType.Text)
            {
                case "Sql Server":
                    dBType = DBType.SqlServer;
                    break;
                case "Oracle":
                    dBType = DBType.Oracle;
                    break;
                case "MySql":
                    dBType = DBType.Oracle;
                    break;
            }
            SqlHelper sqlHelper = new SqlHelper(dBType, txtConnection.Text);
            try
            {
                var conn = sqlHelper.GetConnection();
                conn.Dispose();
                MessageBox.Show("连接成功");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
