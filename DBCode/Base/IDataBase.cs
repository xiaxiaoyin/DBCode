using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCode.Base
{
    public interface IDataBase
    {
        /// <summary>
        /// 获取数据库中的表名
        /// </summary>
        List<string> GetTableNames();

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        TableInfo GetTableInfo(string tableName);

    }
}
