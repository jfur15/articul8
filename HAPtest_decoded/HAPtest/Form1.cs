using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
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
        private void URLGet(TextBox tb, RichTextBox rtb)
        {
            HtmlWeb webPage = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load(tb.Text);
            //HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load("https://web.archive.org/web/20160314164825/http://www.nytimes.com/2016/03/15/us/politics/bernie-sanders-amendments.html?partner=rss&emc=rss");
            List<HtmlNode> myNodes = new List<HtmlNode>();
            foreach (HtmlNode node in getHtmlWeb.DocumentNode.SelectNodes("//p"))
            {
                rtb.Text += System.Net.WebUtility.HtmlDecode(node.InnerText);

                rtb.AppendText("\n\n\n");

                myNodes.Add(node);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetText g = new GetText();
            Dictionary<TextBox, RichTextBox> inputs = new Dictionary<TextBox, RichTextBox>();
            inputs.Add(textBox1, richTextBox1);
            inputs.Add(textBox2, richTextBox2);

            foreach (TextBox t in inputs.Keys)
            {
                URLGet(t, inputs[t]);
            }
            

            //g.URLSplit(textBox1.Text, richTextBox1);
        }

    }
}

