using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCode.Base
{
    public class CodeUtil
    {
        /// <summary>
        /// 创建类代码文件
        /// </summary>
        /// <param name="tableInfo"></param>
        /// <param name="filePath">类代码文件存放路径</param>
        /// <param name="codeClassName">类名</param>
        /// <param name="codeNamespace">类的命名空间</param>
        public static void CreateCodeFile(TableInfo tableInfo, string filePath, string codeClassName, string codeNamespace)
        {
            StringBuilder content = new StringBuilder();
            content.AppendLine("using System;").AppendLine("");
            content.AppendLine(string.Format("namespace {0}", codeNamespace));
            content.AppendLine("{");
            content.AppendLine(string.Format("\tpublic class {0}", codeClassName));
            content.AppendLine("\t{");
            string pro = CreatePropertyStr(tableInfo);
            content.AppendLine(pro);
            content.AppendLine("\t}");
            content.AppendLine("}");
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(filePath, content.ToString());
        }

        private static string CreatePropertyStr(TableInfo tableInfo)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ColumnInfo col in tableInfo.ColumnInfos)
            {
                sb.AppendLine();
                if (!string.IsNullOrEmpty(col.Description))
                {
                    sb.AppendLine("\t\t///<summary>");
                    sb.AppendLine("\t\t///" + col.Description);
                    sb.AppendLine("\t\t///</summary>");
                }
                string typeName = ""; 
                switch (col.ColumnType.Name)
                {
                    case "Int16":
                        typeName = "Int16";
                        break;
                    case "Int32":
                        typeName = "int";
                        break;
                    case "String":
                        typeName = "string";
                        break;
                    case "Int64":
                        typeName = "long";
                        break;
                    case "Decimal":
                        typeName = "decimal";
                        break;
                    case "DateTime":
                        typeName = "DateTime";
                        break;
                    case "Boolean":
                        typeName = "bool";
                        break;
                    case "Single":
                        typeName = "float";
                        break;
                    case "Double":
                        typeName = "double";
                        break;
                }
                sb.AppendLine(string.Format("\t\tpublic {0} {1} {{ get; set; }}", typeName, col.ColumnName));
            }
            return sb.ToString();
        }
    }
}
