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

            var transcoder = new NReadability.NReadabilityWebTranscoder();
            bool success;
            string page = transcoder.Transcode(url, out success);

            if (success)
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);

                var title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                //var imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
                var mainText = doc.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerText;
                richTextBox1.Text = title + "\n\n" + mainText;
            }
        }
    }
}