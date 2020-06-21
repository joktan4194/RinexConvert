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
    class Topcon : IGNSSDC
    {
        private string workingDirec;
        private string jokta;
        private string fileNameToDecompres;
        private string desiredNam;
        private string newWorkingDirect;

        private string teqcPath;
        private string teqcPathDes;
 


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

        public string LineWriter(string workingDirect, string joktan, string fileNameToDecompress, string year)
        {
            workingDirec = workingDirect; jokta = joktan; fileNameToDecompres = fileNameToDecompress; desiredNam = year;
            newWorkingDirect = workingDirect;
            newWorkingDirect += "joktan.bat";

            FileStream stream = null;
            stream = new FileStream(newWorkingDirect, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII))
            {
                writer.WriteLine("teqc -top tps {0}", fileNameToDecompress);
            }
            teqcPath += "\\Program Files\\teqc.exe";
            teqcPathDes = workingDirect + "\\teqc.exe";

            teqcFileResolver();
            return "From Topcon";
        }

        public void teqcFileResolver()
        {
            File.Copy(teqcPath, teqcPathDes, true);
            Process.Start(teqcPathDes);
            Execute();
        }
    }
}
