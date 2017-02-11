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
            List<Article> allArticles = new List<Article>();

            for (int i = tabControl1.TabPages.Count; i > 1; i--)
            {
                tabControl1.TabPages.Remove(tabControl1.TabPages[i-1]);
            }

            foreach (TextBox T in Articles.Controls)
            {
                T.BackColor = Color.White;
                if (!string.IsNullOrEmpty(T.Text))
                {
                   
                    if (Uri.IsWellFormedUriString(T.Text, UriKind.Absolute))
                    {
                        Article newArticle = new Article(T.Text);
                        allArticles.Add(newArticle);
                    }
                    else
                    {
                        T.BackColor = Color.LightPink;
                        MessageBox.Show("Invalid Input: " + T.Text + " is not a valid URL");
                    }
                }
            }
            if (allArticles.Count() < 2)
            {
                MessageBox.Show("Invalid Input: Please enter URL into at least 2 fields");
            }

            for (int i = 0; i < allArticles.Count; i++)
            {
                URLGet(allArticles[i]);
            }

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            ////code for splitting up paragraphs into sentences
            //foreach (Article L in allArticles)
            //{

            //    List<List<string[]>> allArticlesSentences = new List<List<string[]>>();
            //    allArticlesSentences.Add(splitPargraphs(L));

            //    List<List<List<string>>> AllArticlesSentencesWords = new List<List<List<string>>>();
            //}
               


                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                int idx = 1;
            foreach (Article A in allArticles)
            {
                TabPage tempTab = new TabPage();
                RichTextBox tempRtb = new RichTextBox();
                
                foreach (Paragraph p in A.paragraphs)
                {
                    richTextBox1.AppendText(p.Text + "\n\n\n");
                    tempRtb.AppendText(p.Text + "\n\n\n");
                }
                tempTab.Text = "Article " + idx;
                tempTab.Controls.Add(tempRtb);
                tempRtb.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tempTab);
                idx++;
            }
        
        }


        //public void AddArticle(List<List<string>> listString)
        //{
        //    Paragraphs ArticleContent = new Paragraphs();
        //    listString.Add(UrlContent);
        //}

        private void URLGet(Article anArticle)
        {
            HtmlWeb webPage = new HtmlWeb();
            webPage.UseCookies = true;
            //Try webPage.load(tb.text)
            HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load(anArticle.URL);
            //HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load("https://web.archive.org/web/20160314164825/http://www.nytimes.com/2016/03/15/us/politics/bernie-sanders-amendments.html?partner=rss&emc=rss");
            List<HtmlNode> myNodes = new List<HtmlNode>();
            foreach (HtmlNode node in getHtmlWeb.DocumentNode.SelectNodes("//p"))
            {
                anArticle.AddParagraph(System.Net.WebUtility.HtmlDecode(node.InnerText));
            }
        }
  
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // Split list of paragraphs into list of sentences
        private List<string[]> splitPargraphs(List<string> listParagraphs)
        {
            List<string[]> listSentences = new List<string[]>();

            // Use space as delimiter to split string into individual words.
            char[] delimiters = new char[] { '.' };

            // Split list of paragraphs into list of sentences
            foreach (string paragraph in listParagraphs)
            {
                // Create sentences based on periods
                listSentences.Add(paragraph.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));
            }

            //listString.Add(UrlContent);

            return listSentences;
        }

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //// Split list of sentences into list of words
        //private List<string> splitSentences(List<string> listSentences)
        //{
        //    List<string> listWords = new List<string>();
        //    listString.Add(UrlContent);


        //    return listWords;
        //}

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    }
}

