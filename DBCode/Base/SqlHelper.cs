using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCode.Base
{
    public enum DBType
    {
        SqlServer=1,
        Oracle=2,
        MySql=3
    }
    class SqlHelper
    {
        public DBType dBType { get; set; }
        public string ConnectionStr { get; set; }

        public SqlHelper(DBType dBType,string ConnectionStr)
        {
            this.dBType = dBType;
            this.ConnectionStr = ConnectionStr;
        }

        public DbConnection GetConnection()
        {
            if (string.IsNullOrEmpty(this.ConnectionStr))
            {
                throw new Exception("请配置数据库连接信息");
            }
            DbConnection conn = null;
            switch(this.dBType)
            {
                case DBType.SqlServer:
                    conn = new SqlConnection(this.ConnectionStr);
                    break;
                case DBType.Oracle:
                    break;
                case DBType.MySql:
                    break;
            }
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

        public int ExuteNoquery(string sql)
        {
            using (DbConnection conn = GetConnection())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                return cmd.ExecuteNonQuery();
            }
        }

        public System.Data.DataTable Exute(string sql)
        {
            using (DbConnection conn = GetConnection())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                //SqlDataAdapter dap = new SqlDataAdapter(cmd);
                DbDataAdapter dataAdapter = null;
                
                switch (this.dBType)
                {
                    case DBType.SqlServer:
                        dataAdapter = new SqlDataAdapter((SqlCommand)cmd);
                        break;
                    case DBType.Oracle:
                        break;
                    case DBType.MySql:
                        break;
                }
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                dataAdapter.Dispose();
                return dt;
            }
        }

    }

}

