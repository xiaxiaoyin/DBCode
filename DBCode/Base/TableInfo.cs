using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCode.Base
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo
    {
        public string TableName { get; set; }

        public List<ColumnInfo> ColumnInfos { get; set; }

        public TableInfo()
        {
            if (this.ColumnInfos == null)
            {
                this.ColumnInfos = new List<ColumnInfo>();
            }
        }
    }

    /// <summary>
    /// 列信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public Type ColumnType { get; set; }

        /// <summary>
        /// 列名说明
        /// </summary>
        public string Description { get; set; }
    }
}
