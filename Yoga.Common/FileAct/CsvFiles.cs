using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yoga.Common.FileAct
{
    public class CsvFiles
    {
        /// <summary>
        /// 将ListBox中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fileName">CSV的文件路径</param>
        /// 
        public static void SaveTXT(ListBox liststr, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.ASCII);

            for (int i = 0; i < liststr.Items.Count; i++)
            {
                sw.WriteLine(liststr.Items[i].ToString());                 //写出各行数据
            }

            sw.Close();
            fs.Close();
        }

        public void SaveCSV(ListView lv, string fullPath)
        {
            try
            {
                FileInfo fi = new FileInfo(fullPath);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.ASCII);
                string wstr;
                wstr = "";
                if (lv.Columns.Count > 12)
                {
                    wstr = lv.Columns[0].Text.Trim().ToString();
                    for (int k = 1; k < 13; k++)     //lv.Columns.Count
                    {
                        wstr = wstr + "," + lv.Columns[k].Text.Trim().ToString();
                    }
                    sw.WriteLine(wstr);


                    for (int i = 0; i < lv.Items.Count; i++)
                    {
                        wstr = lv.Items[i].SubItems[0].Text.Trim();
                        for (int j = 1; j < 13; j++)  //lv.Columns.Count
                        {
                            wstr = wstr + "," + lv.Items[i].SubItems[j].Text.Trim();
                        }
                        sw.WriteLine(wstr);
                    }
                }
                else
                {
                    Util.Notify("文件保存异常,数据格式错误");
                }
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Util.WriteLog(this.GetType(), ex);
                Util.Notify("文件保存异常");
            }

        }

        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="fileName">CSV文件路径</param>
        /// <returns>返回读取了CSV数据的DataTable</returns>
        /// 
        public static DataTable OpenTestCSV(string filePath)
        {
            try
            {
                String strLine;
                String[] split = null;
                DataTable table = new DataTable();
                DataRow row = null;
                StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);         //创建与数据源对应的数据列 
                strLine = sr.ReadLine();
                split = strLine.Split(',');

                int i = 0;
                foreach (String colname in split)
                {

                    table.Columns.Add(colname, System.Type.GetType("System.String"));
                    i++;
                    if (i > 15) break;
                }

                //将数据填入数据表 
                int j = 0;
                while ((strLine = sr.ReadLine()) != null)
                {
                    j = 0;
                    row = table.NewRow();
                    split = strLine.Split(',');
                    foreach (String colname in split)
                    {
                        row[j] = colname;
                        j++;
                        if (j > 15) break;
                    }
                    table.Rows.Add(row);
                }
                sr.Close();
                return table;
            }
            catch (Exception ex)
            {
                Util.WriteLog(typeof(CsvFiles), ex);
                Util.Notify("文件打开异常");
            }
            return null;
        }

        /// <summary>
        /// 将CSV文件的数据读取到DataTable中
        /// </summary>
        /// <param name="pCsvpath"></param>
        /// <param name="pCsvname"></param>
        /// <returns></returns>
        public DataTable GetCsvData(string pCsvpath, string pCsvname)
        {
            OleDbConnection OleCon = new OleDbConnection();
            try
            {
                DataSet dsCsvData = new DataSet();


                OleDbCommand OleCmd = new OleDbCommand();
                OleDbDataAdapter OleDa = new OleDbDataAdapter();

                OleCon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pCsvpath + ";Extended Properties='Text;FMT=Delimited(,);HDR=YES;IMEX=1';";
                OleCon.Open();
                DataTable dts1 = OleCon.GetSchema("Tables");
                DataTable dts2 = OleCon.GetSchema("Columns");
                OleCmd.Connection = OleCon;
                OleCmd.CommandText = "select * from [" + pCsvname + "] where 1=1";
                OleDa.SelectCommand = OleCmd;
                OleDa.Fill(dsCsvData, "Csv");
                OleCon.Close();

                return dsCsvData.Tables[0];
            }
            catch
            {
                return null;
            }
            finally
            {
                if (OleCon.State == System.Data.ConnectionState.Open)
                    OleCon.Close();
            }
        }

        /// <summary>
        /// 将DataTable中数据写入到CSV文件中
        /// </summary>
        /// <param name="dt">提供保存数据的DataTable</param>
        /// <param name="fileName">CSV的文件路径</param>
        public void SaveCSV(DataTable dt, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            string data = "";
            //读取列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            //列名称写入
            sw.WriteLine(data);
            //读取行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                    if (str.Contains(',') || str.Contains('"')
                        || str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);//写入行数据
            }
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 将DataTable中数据追加写入到CSV文件中(包含表头)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fullPath"></param>
        public static void AppendWriteCSV(DataTable dt, string fullPath)//table数据写入csv
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);
            bool onNewFile = false;
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
                onNewFile = true;
            }
            if (!fi.Exists)
            {
                onNewFile = true;
            }
            using (FileStream fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                {
                    string data = "";
                    if (onNewFile)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)//写入列名
                        {
                            data += dt.Columns[i].ColumnName.ToString();
                            if (i < dt.Columns.Count - 1)
                            {
                                data += ",";
                            }
                        }
                        sw.WriteLine(data);
                    }


                    for (int i = 0; i < dt.Rows.Count; i++) //写入各行数据
                    {
                        data = "";
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            string str = dt.Rows[i][j].ToString();
                            str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                            if (str.Contains(',') || str.Contains('"')
                                || str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中
                            {
                                str = string.Format("\"{0}\"", str);
                            }

                            data += str;
                            if (j < dt.Columns.Count - 1)
                            {
                                data += ",";
                            }
                        }
                        sw.WriteLine(data);
                    }
                }
            }
        }

    }
}
