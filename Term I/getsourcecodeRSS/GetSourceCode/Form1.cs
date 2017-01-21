using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;

namespace GetSourceCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = URLTextBox.Text;
            XmlReader myXml = XmlReader.Create(url);
            SyndicationFeed syn = SyndicationFeed.Load(myXml);
            myXml.Close();
            foreach (SyndicationItem item in syn.Items)
            {
                richTextBox1.AppendText(item.Title.Text);

                richTextBox1.AppendText("\n\n");
            }    
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void URLTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
