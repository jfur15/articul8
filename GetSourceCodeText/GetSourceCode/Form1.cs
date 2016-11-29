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
            //string url = URLTextBox1.Text;
            //Hard-coded Article 1 in
            URLTextBox1.Text = "Hardcoded for example";
            string url1 = "https://web.archive.org/web/20160314164825/http://www.nytimes.com/2016/03/15/us/politics/bernie-sanders-amendments.html?partner=rss&emc=rss";
            //string url2 = URLTextBox2.Text;
            //Hard-coded Article 2 in
            URLTextBox2.Text = "Hardcoded for example";
            string url2 = "http://www.nytimes.com/2016/03/15/us/politics/bernie-sanders-amendments.html";
            var transcoder = new NReadability.NReadabilityWebTranscoder();
            bool success;
            string page1 = transcoder.Transcode(url1, out success);
            //Transcode for url2
            string page2 = transcoder.Transcode(url2, out success);

            if (success)
            {
                HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                doc1.LoadHtml(page1);

                var title1 = doc1.DocumentNode.SelectSingleNode("//title").InnerText;
                //var imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
                var mainText1 = doc1.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerText;
                richTextBox1.Text = title1 + "\n\n" + mainText1;

                HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
                doc2.LoadHtml(page2);

                var title2 = doc2.DocumentNode.SelectSingleNode("//title").InnerText;
                //var imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
                var mainText2 = doc2.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerText;
                richTextBox2.Text = title2 + "\n\n" + mainText2;
            }
        }
    }
}