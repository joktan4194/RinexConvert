using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinexConvert
{
    class Javad : IGNSSDC
    {
        private string newWorkingDirect;
        private string teqcPath;
        private string teqcPathDes;
        private string workingDirec, jokta, fileNameToDecompres, desiredNam;
        public void DeleteFile()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            throw new NotImplementedException();
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
                writer.WriteLine("teqc -jav jps {0}", fileNameToDecompress);
                //    
                //    else if (ext.Contains("to00"))
                //    {
                //        writer.WriteLine("teqc -top tps {0}", fileNameToDecompress);
                //    }
                //    else
                //    {
                //        writer.WriteLine("teqc -sep sbf {0}", fileNameToDecompress);
                //    }
            }
            return "From Javad";
        }

        public void teqcFileResolver()
        {
            throw new NotImplementedException();
        }
    }
}
