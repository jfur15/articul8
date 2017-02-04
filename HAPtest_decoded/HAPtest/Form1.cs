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
using System.Net;

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
            List<List<string>> allArticles = new List<List<string>>();
            List<string> TextBoxUrlList = new List<string>();

            foreach (TextBox T in Articles.Controls)
            {
                if (!string.IsNullOrEmpty(T.Text))
                {
                    TextBoxUrlList.Add(T.Text);
                    AddArticle(allArticles);
                }
            }
            if (TextBoxUrlList.Count() < 2)
            {
                MessageBox.Show("Invalid Input: Please enter URL into at least 2 fields");
            }

            for (int i = 0; i < TextBoxUrlList.Count; i++)
            {
                URLGet(TextBoxUrlList[i], allArticles[i]);
            }

            richTextBox1.AppendText("Hello!");
        }

        private void AddArticle(List<List<string>> listString)
        {
            List<string> UrlContent = new List<string>();
            listString.Add(UrlContent);
        }

        private void URLGet(string tb, List<string> stringList)
        {
            HtmlWeb webPage = new HtmlWeb();
            webPage.UseCookies = true;
            //Try webPage.load(tb.text)
            HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load(tb);
            //HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load("https://web.archive.org/web/20160314164825/http://www.nytimes.com/2016/03/15/us/politics/bernie-sanders-amendments.html?partner=rss&emc=rss");
            List<HtmlNode> myNodes = new List<HtmlNode>();
            foreach (HtmlNode node in getHtmlWeb.DocumentNode.SelectNodes("//p"))
            {
                stringList.Add(System.Net.WebUtility.HtmlDecode(node.InnerText));
            }
        }
    }
}

