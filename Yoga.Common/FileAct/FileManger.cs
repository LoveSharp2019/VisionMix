using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Yoga.Common.FileAct
{
    class FileComparer : IComparer
    {
        int IComparer.Compare(Object o1, Object o2)
        {
            FileInfo fi1 = o1 as FileInfo;
            FileInfo fi2 = o2 as FileInfo;
            return fi1.CreationTime.CompareTo(fi2.CreationTime);
        }
    }

  public  class FileManger
    {
        //文件操作必须是原子的如果正在操作就直接放弃
        private static int InOverflow=0;
        public static  void  DeleteOverflowFile(string path,int maxFileNumber)
        {
            if (Interlocked.CompareExchange(ref InOverflow, 1, 0) == 0)
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    FileInfo[] files = di.GetFiles();
                    if (files.Length < maxFileNumber)
                    {
                        return;
                    }
                    FileComparer fc = new FileComparer();
                    Array.Sort(files, fc);
                    int number = files.Length / 2;
                    for (int i = 0; i < number; i++)
                    {
                        File.Delete(files[i].FullName);
                    }
                    File.Delete(files[0].FullName);
                    Util.Notify("多余图像被删除");


                }
                catch (Exception ex)
                {
                    Util.WriteLog(typeof(FileManger), ex);
                    Util.Notify("图像文件删除失败");
                }
                finally
                {
                    Interlocked.Exchange(ref InOverflow, 0);
                }
            }
        }
    }
}
