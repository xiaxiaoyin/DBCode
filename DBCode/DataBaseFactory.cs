using DBCode.Base;
using DBCode.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCode
{

    public class DataBaseFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="ConnectionStr">数据库连接字符串</param>
        /// <returns></returns>
        public static IDataBase GetDataBase(DBType databaseType, string ConnectionStr)
        {
            IDataBase database = null;
            switch (databaseType)
            {
                case DBType.SqlServer:
                    database = new SqlServerDataBase(ConnectionStr);
                    break;
                case DBType.Oracle:
                    break;
                case DBType.MySql:
                    break;
            }
            return database;
        }
    }
}
