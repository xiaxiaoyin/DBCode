using DBCode.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCode.SqlServer
{
    class SqlServerDataBase : IDataBase
    {
        SqlHelper SqlHelper { get; set; }

        public SqlServerDataBase(string ConnectionStr)
        {
            this.SqlHelper = new SqlHelper(DBType.SqlServer, ConnectionStr);
        }

        /// <summary>
        /// 获取数据表名称集合
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableNames()
        {
            List<string> tableNames = new List<string>();
            string sql = "SELECT name FROM SysObjects Where XType='U' ORDER BY Name";
            DataTable dt = this.SqlHelper.Exute(sql);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    tableNames.Add(row["name"].ToString());
                }
            }
            return tableNames;
        }

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public TableInfo GetTableInfo(string tableName)
        {
            TableInfo tableinfo = new TableInfo();
            string queryInfoSql = string.Format(@"SELECT 
                                        表名       = case when a.colorder=1 then d.name else '' end,
                                        表说明     = case when a.colorder=1 then isnull(f.value,'') else '' end,
                                        字段序号   = a.colorder,
                                        字段名     = a.name,
                                        标识       = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,
                                        主键       = case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                                                         SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) then '√' else '' end,
                                        类型       = b.name,
                                        占用字节数 = a.length,
                                        长度       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
                                        小数位数   = isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
                                        允许空     = a.isnullable ,
                                        默认值     = isnull(e.text,''),
                                        字段说明   = isnull(g.[value],'')
                                    FROM 
                                        syscolumns a
                                    left join 
                                        systypes b 
                                    on 
                                        a.xusertype=b.xusertype
                                    inner join 
                                        sysobjects d 
                                    on 
                                        a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
                                    left join 
                                        syscomments e 
                                    on 
                                        a.cdefault=e.id
                                    left join 
                                    sys.extended_properties   g 
                                    on 
                                        a.id=G.major_id and a.colid=g.minor_id  
                                    left join
                                    sys.extended_properties f
                                    on 
                                        d.id=f.major_id and f.minor_id=0
                                    where 
                                        d.name='{0}'", tableName);

            DataTable dt = this.SqlHelper.Exute(queryInfoSql);
            if (dt != null)
            {
                tableinfo.TableName = tableName;
                foreach (DataRow row in dt.Rows)
                {
                    tableinfo.ColumnInfos.Add(new ColumnInfo() { ColumnName = row["字段名"].ToString(), ColumnType = GetColumnType(row["类型"].ToString()), Description = row["字段说明"].ToString() });
                }
            }
            return tableinfo;
        }

        private Type GetColumnType(string dbColumnType)
        {
            Type T = null;
            switch (dbColumnType)
            {
                case "smallint":
                    T = typeof(Int16);
                    break;
                case "int":
                    T = typeof(Int32);
                    break;
                case "bigint":
                    T = typeof(Int64);
                    break;
                case "bit":
                    T = typeof(bool);
                    break;
                case "char":
                case "text":
                case "varchar":
                case "nchar":
                case "ntext":
                case "nvarchar":
                    T = typeof(string);
                    break;
                case "date":
                case "datetime":
                    T = typeof(DateTime);
                    break;
                case "decimal":
                    T = typeof(decimal);
                    break;
                case "float":
                    T = typeof(double);
                    break;
            }
            return T;
        }
    }
}
