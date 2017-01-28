using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace HAPtest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HtmlWeb webPage = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load(textBox1.Text);


            foreach (HtmlNode node in getHtmlWeb.DocumentNode.SelectNodes("//p"))
            {
                richTextBox1.Text += node.InnerHtml;

                richTextBox1.AppendText("\n\n\n");
            }

        }


        
    }
}