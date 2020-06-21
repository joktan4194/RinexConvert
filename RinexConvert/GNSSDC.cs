using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RinexConvert
{
    class GNSSDC
    {
        public GNSSDC()
        {
            
        }
        public bool Make(string ext,string workingDirect, string joktan, string fileNameToDecompress, string year)
        {
            if (ext.Contains("m00")) {
                var writer  = new Leica();
                writer.LineWriter(workingDirect,joktan,fileNameToDecompress,year);
                return true;
        //        System.Windows.Forms.MessageBox.Show("Leica");
            }
            else if (ext.Contains("t00") || ext.Contains("t01") || ext.Contains("t02") || ext.Contains("r00")) {
                var writer = new Trimble();
                writer.LineWriter(workingDirect,joktan,fileNameToDecompress,year);
                return true;
                //System.Windows.Forms.MessageBox.Show("Trimble");
            }
            //else if (ext.Contains("j00")) { 
            //    System.Windows.Forms.MessageBox.Show("Javad");
            //}
            //else if (ext.Contains("O")|| ext.Contains("o")) { 
            //    System.Windows.Forms.MessageBox.Show("Nothing");
            //}           
            else if (ext.Contains("tps"))
            {
                var writer = new Topcon();
                writer.LineWriter(workingDirect, joktan, fileNameToDecompress, year);
                return true;
            }
            else
            { 
                
                return false;
            }
        }
        public void DeleteFiles(string ext) { }
        public void FileResolver(string ext) { }

    }
}
