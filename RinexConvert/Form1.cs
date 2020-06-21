using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RinexConvert
{
    public partial class Form1 : Form
    {
        //variable declarations
        private string file;
        private string fileName;
        private string ext;
        private string workingDirect;
        private string destinationPath;
        private string[] fullname;
        private string teqcPath, teqcPathDes, runkPathDes, runkPath;
        private string fileNameToDecompress;
        private string newWorkingDirect;
        private GNSSDC GNSSDCFactory;
        bool executed=true;
        public Form1()
        {
            InitializeComponent();
            GNSSDCFactory = new GNSSDC();
        }

        //ExecuteFile() 
        //function to run the cmd
        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;

            if (textBox1.Text.Trim().Equals(""))
            {
                MessageBox.Show("Please Select Leica/Trimble/Javad/Ashtech file to extract","Error");
            }
            else{
                var len = textBox3.Text.Length;
                var empt = textBox3.Text.Trim().Equals("");
                if (empt)
                {
                    MessageBox.Show("Year of observation must not be empty", "Error");
                }
                else if(len<2) {
                    MessageBox.Show("Year of observation must be two digits", "Error");
                }
                else
                {
                    try
                    {
                        LineWriter();
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        if(executed!=false) {
                            MessageBox.Show("Your Navigation and Observation files have been extracted", "Information");
                            this.button1.Enabled = true;
                            this.button2.Enabled = true;
                            this.button3.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Please Select an Appropriate File","Error");
                            this.button1.Enabled = true;
                            this.button2.Enabled = true;
                            this.button3.Enabled = true;

                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        File.Delete(newWorkingDirect);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        MessageBox.Show(ex.Message.ToString(),"Error");
                        this.button1.Enabled = true;
                        this.button2.Enabled = true;
                        this.button3.Enabled = true;

                    }
                }
            }
            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.button3.Enabled = true;
        }
        //Exit() 
        // close the app
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //GET File Name
        //generate a path file
        private void concatinator(string[] m)
        {
            destinationPath="";
            for(int i=0;i<m.Length-1;i++)
            {
                destinationPath += m[i]+@"\";
                teqcPath = m[0];
            }
            workingDirect = destinationPath;
           // destinationPath += BathName;
        }
        //BatFileWriter() 
        //create a new bat file with the commands
        private void LineWriter()
        {
            string joktan;
            string a = DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
            string y = textBox3.Text;//DateTime.Now.Year.ToString().Substring(2);
            if (textBox2.Text.Trim().Equals(""))
            {
                joktan = "CGG-SGS-2019-" + a;
            }
            else
            {
                joktan = textBox2.Text.Trim();
            }

            if (!GNSSDCFactory.Make(ext, workingDirect, joktan, fileNameToDecompress, y)) { executed = false; } else { executed = true; };
            //try
            //{
            //code to write the code for a particle

            //    stream =new FileStream(newWorkingDirect, FileMode.CreateNew);
            //    using (StreamWriter writer = new StreamWriter(stream,Encoding.ASCII))
            //    {
            //        if (ext == ".m00")
            //        {
            //            writer.WriteLine("teqc -leica mdb {0} > " + "{1}.{2}o", fileNameToDecompress, joktan, y);
            //            writer.WriteLine("teqc -leica mdbn {0} > " + "{1}.{2}n", fileNameToDecompress, joktan, y);
            //        }
            //        else if(ext.Contains("t00") || ext.Contains("t01") || ext.Contains("t02") || ext.Contains("r00"))
            //        {
            //            writer.WriteLine("runpkr00 d {0}", fileNameToDecompress);
            //            writer.WriteLine("teqc -tr d {0}", fileNameToDecompress);
            //        }
            //        else if (ext.Contains("j00"))
            //        {
            //            writer.WriteLine("teqc -jav jps {0}", fileNameToDecompress);
            //        }
            //        else if (ext.Contains("to00"))
            //        {
            //            writer.WriteLine("teqc -top tps {0}", fileNameToDecompress);
            //        }
            //        else
            //        {
            //            writer.WriteLine("teqc -sep sbf {0}", fileNameToDecompress);
            //        }
            //    }
            //    teqcPath += "\\Program Files\\teqc.exe";
            //    runkPath += "\\Program Files\\runpk00.exe";
            //    teqcPathDes = workingDirect + "\\teqc.exe";
            //    runkPathDes = workingDirect + "\\runpk00.exe";

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message.ToString());
            //    File.Delete(newWorkingDirect);
            //    textBox1.Text = "";
            //}
        }
        //GetTheFile() 
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            OpenFileDialog fileDialog1 = new OpenFileDialog();
            fileDialog1.DefaultExt = "*";
            //fileDialog1.Filter = "*.m00|*.r00|*.t00|*.t01|*.t02|*.*o";
            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = fileDialog1.FileName;
                var dir=fileDialog1.InitialDirectory;
                textBox1.Text=fileName;
                var a = fileName.Split('\\');
                fullname = a;
                file=(a[a.Length - 1]);
                var k = file.Split('.');
                ext = (k[k.Length - 1]);
                concatinator(fullname);
                fileNameToDecompress = file;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Process.Start(@"C:\\Program Files\\teqc.exe");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (ext.Contains("O") || ext.Contains("o"))
                {
                    if (textBox2.Text != "") {
                        qualityChecker();
                    }
                    else { 
                        MessageBox.Show("Please Specify a quality check file name", "Error");
                    }
                }
                else { 
                    MessageBox.Show("Please Select and Observation File", "Error");
                }
            }
            else
            {
                MessageBox.Show("Please Select an Observation File to Perform Qulaity Check on", "Error");
            }

            //Form2 fm = new Form2();
            //fm.Show();
            //this.Close();
            //this.Hide();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //GetTECQFile() 

        private void qualityChecker() {
            FileStream stream = null;
            string joktan = textBox2.Text.Trim();
            var direc = workingDirect + "jok.bat";
            stream = new FileStream(direc, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII))
            {
                writer.WriteLine("teqc +qc {0} > {1}", fileNameToDecompress,workingDirect+"\\"+joktan+".txt");
                //writer.WriteLine("pause");
            }
            teqcPath += "\\Program Files\\teqc.exe";
            teqcPathDes = workingDirect + "\\teqc.exe";
            File.Copy(teqcPath, teqcPathDes, true);
            Process.Start(teqcPathDes);
            ProcessStartInfo processtartinfo = new ProcessStartInfo();
            processtartinfo.WindowStyle = ProcessWindowStyle.Normal;
            processtartinfo.WorkingDirectory = workingDirect;
            processtartinfo.UseShellExecute = true;
            processtartinfo.WindowStyle = ProcessWindowStyle.Normal;
            processtartinfo.FileName = direc;
            Process.Start(processtartinfo);
            Thread.Sleep(1200);
            File.Delete(direc);
            Thread.Sleep(1200);
            File.Delete(teqcPathDes);
            textBox1.Text = "";
            textBox2.Text = "";
            MessageBox.Show("Your Quality Check file has been successfully extracted","Information");
        }
    }
}