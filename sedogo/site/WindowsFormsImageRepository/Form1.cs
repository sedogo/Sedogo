using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsImageRepository
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {folderBrowserDialog1.ShowDialog();
         DeleteFiles(folderBrowserDialog1.SelectedPath,false);  
            }
        void DeleteFiles(string path, bool deleteFiles)
        {
            foreach (string dpath in Directory.GetDirectories(path)) DeleteFiles(dpath,true);
            if (!deleteFiles) return;
            foreach (string fpath in Directory.GetFiles(path))
            {
                File.Delete(fpath);                
            }

        }
    }
}
