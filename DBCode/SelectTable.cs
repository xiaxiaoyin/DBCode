using DBCode.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBCode
{
    public partial class SelectTable : Form
    {
        public IDataBase dataBase { get; set; }

        public SelectTable()
        {
            InitializeComponent();
        }

        public SelectTable(DBType dbType, string ConnectionStr) : this()
        {
            dataBase = DataBaseFactory.GetDataBase(dbType, ConnectionStr);
        }

        private void SelectTable_Load(object sender, EventArgs e)
        {
            List<string> tableNames = dataBase.GetTableNames();
            foreach (string name in tableNames)
            {
                clbTables.Items.Add(name);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNameSpace.Text))
            {
                MessageBox.Show("请填写类命名空间");
                return;
            }
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                MessageBox.Show("请填写类文件存放路径");
                return;
            }
            if (clbTables.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要生成代码的表");
                return;
            }
            Regex reg = new Regex("{\\}|{/}$");
            for (int i = 0; i < clbTables.Items.Count; i++)
            {
                if (clbTables.GetItemChecked(i))
                {
                    string tableName = clbTables.Items[i].ToString();
                    TableInfo tableInfo = dataBase.GetTableInfo(tableName);
                    string filePath = reg.Replace(txtLocation.Text, "").Replace("/", "\\");

                    if (tableInfo != null)
                    {
                        CodeUtil.CreateCodeFile(tableInfo, string.Format("{0}.cs", filePath + "\\" + tableName), tableName, txtNameSpace.Text);
                    }
                }
            }
            MessageBox.Show("执行完毕");
        }
    }
}
