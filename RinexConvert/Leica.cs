using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RinexConvert
{
    class Leica : IGNSSDC
    {
        private string newWorkingDirect;
        private string teqcPath;
        private string teqcPathDes;
        private string workingDirec, jokta, fileNameToDecompres, desiredNam;
       
        public void DeleteFile()
        {
            File.Delete(newWorkingDirect);
            File.Delete(teqcPathDes);
        }

        public void Execute()
        {
            try
            {
                ProcessStartInfo processtartinfo = new ProcessStartInfo();
                processtartinfo.WindowStyle = ProcessWindowStyle.Hidden;
                processtartinfo.WorkingDirectory = workingDirec;
                processtartinfo.UseShellExecute = true;
                processtartinfo.WindowStyle = ProcessWindowStyle.Normal;
                processtartinfo.FileName = newWorkingDirect;
                Process.Start(processtartinfo);
                Thread.Sleep(500);
                Thread.Sleep(1200);
                DeleteFile();

            }
            catch (FileNotFoundException ex)
            {
                DeleteFile();
            }
        }

        public string LineWriter(string workingDirect, string joktan, string fileNameToDecompress, string desiredName)
        {
            workingDirec = workingDirect; jokta = joktan; fileNameToDecompres = fileNameToDecompress; desiredNam = desiredName;
            newWorkingDirect = workingDirect;
            newWorkingDirect += "joktan.bat";
            FileStream stream = null;
            stream = new FileStream(newWorkingDirect, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII))
            {
                    writer.WriteLine("teqc -leica mdb {0} > " + "{1}.{2}o", fileNameToDecompress, joktan, desiredName);
                    writer.WriteLine("teqc -leica mdbn {0} > " + "{1}.{2}n", fileNameToDecompress, joktan, desiredName);
            }
            teqcPath += "\\Program Files\\teqc.exe";
            teqcPathDes = workingDirect + "\\teqc.exe";

            teqcFileResolver();
            return "From Lieca";

        }

        public void teqcFileResolver()
        {
            File.Copy(teqcPath, teqcPathDes, true);
            Process.Start(teqcPathDes);
            Execute();
        }
    }
}
