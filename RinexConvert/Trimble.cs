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
    class Trimble : IGNSSDC
    {
        private string newWorkingDirect;
        private string teqcPathDes;
        private string runkPathDes;
        private string runkPath;
        private string teqcPath;
        private string workingDirec, jokta, fileNameToDecompres, desiredNam;
        private string newWorkingDirect1;

        public void DeleteFile()
        {
            File.Delete(newWorkingDirect);
            Thread.Sleep(200);
            File.Delete(teqcPathDes);
            Thread.Sleep(200);
            File.Delete(runkPathDes);
            Thread.Sleep(200);
            File.Delete(newWorkingDirect1);
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
            newWorkingDirect1 = workingDirect;
            newWorkingDirect += "joktan.bat";
            newWorkingDirect1 += "joktan1.bat";
            var fileNameToDecompre = fileNameToDecompress.Split('.');
            var fileName= fileNameToDecompre[0];
            FileStream stream = null;
            stream = new FileStream(newWorkingDirect, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII))
            {
                writer.WriteLine("teqc -tr d +nav {1}.{2}N {0}.tgd > {1}.{2}O", fileName,joktan,year);

            }
            FileStream stream1 = null;
            stream1 = new FileStream(newWorkingDirect1, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(stream1, Encoding.ASCII))
            {
                writer.WriteLine("runpkr00 -g -d {0}", fileNameToDecompress);
                //writer.WriteLine("pause");

            }
            teqcPath += "\\Program Files\\teqc.exe";
            runkPath += "\\Program Files\\runpkr00.exe";
            teqcPathDes = workingDirect + "\\teqc.exe";
            runkPathDes = workingDirect + "\\runpkr00.exe";
            teqcFileResolver();
            return "From Trimble";
        }

        public void teqcFileResolver()
        {
            File.Copy(teqcPath, teqcPathDes, true);
            Thread.Sleep(500);
            File.Copy(runkPath, runkPathDes, true);
            Thread.Sleep(500);
            Process.Start(runkPathDes);
            tgdExtractor();
            
        }
        public void tgdExtractor() {
            ProcessStartInfo processtartinfo = new ProcessStartInfo();
            processtartinfo.WindowStyle = ProcessWindowStyle.Normal;
            processtartinfo.WorkingDirectory = workingDirec;
            processtartinfo.UseShellExecute = true;
            processtartinfo.WindowStyle = ProcessWindowStyle.Normal;
            processtartinfo.FileName = newWorkingDirect1;
            Process.Start(processtartinfo);
            Thread.Sleep(1200);
            Process.Start(teqcPathDes);
            Execute();
        }
    }
}
