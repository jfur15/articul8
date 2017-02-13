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

        // Primary function: click button to process input
        private void button1_Click(object sender, EventArgs e)
        {
            // Create a list to house all articles inputted
            List<Article> allArticles = new List<Article>();

            // Remove tabs from any previous button clicks
            for (int i = tabControl1.TabPages.Count; i > 1; i--) { tabControl1.TabPages.Remove(tabControl1.TabPages[i-1]); }

            //  Extract URLs from TextBoxes, error check, and create article for each
            foreach (TextBox T in Articles.Controls)
            {
                T.BackColor = Color.White;
                // Is anything in the textbox?
                if (!string.IsNullOrEmpty(T.Text))
                {
                    // Is it actually a URL?
                    if (Uri.IsWellFormedUriString(T.Text, UriKind.Absolute))
                    {
                        // Create new article object and add it to list of all articles
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
            // Error Checking: User must enter two URLs miniumum
            if (allArticles.Count() < 2) { MessageBox.Show("Invalid Input: Please enter URL into at least 2 fields"); }


            // Use URLGet function to retrieve URLs from textbox input and proceed to extract website text and assign it to
            // each Article object in primary list in paragraph form
            for (int i = 0; i < allArticles.Count; i++) { URLGet(allArticles[i]); }


            // Format output to GUI. Create/Label a tab for each article and output text in paragraph form to RichTextBox in each.
            // Also sentence tabs with ordered scores and shit
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
                tempTab.Text = "Article " + idx + ": Paragraphs";
                tempTab.Controls.Add(tempRtb);
                tempRtb.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tempTab);

                tempTab = new TabPage();
                ListView tempView = new ListView();
                tempView.View = View.Details;
                tempView.GridLines = true;
                tempView.Columns.Add(new ColumnHeader().Name = "Text");

                tempView.Columns.Add(new ColumnHeader().Name = "Score");
                foreach (Paragraph p in A.paragraphs)
                {
                    foreach (Sentence s in p.sentences)
                    {
                        richTextBox1.AppendText(s.Text + "\n\n\n");
                        tempView.Items.Add(new ListViewItem(new string[] {s.Grade.ToString(), s.Text }));
                        //tempRtb.AppendText(s.Text + "|" + s.Grade + "\n\n\n");

                    }
                   
                }
                tempView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                tempView.Text = "Article " + idx + ": Sentences";
                tempTab.Controls.Add(tempView);
                tempView.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(tempTab);
                idx++;
            }
        }

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        // Retrieve text from webpage in paragraph form based on URL and assign to passed in Article object
        private void URLGet(Article anArticle)
        {
            // Create HTMLWeb object, enable cookies, and use Article object's URL string to load online webpage
            // into HTMLDocument object
            HtmlWeb webPage = new HtmlWeb();
            webPage.UseCookies = true;
            HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load(anArticle.URL);
            
            // For each node in loaded webpage that is in paragraph tags (<p> </p>) add contained text to Article 
            // object's list of paragraphs
            foreach (HtmlNode node in getHtmlWeb.DocumentNode.SelectNodes("//p"))
            {
                anArticle.AddParagraph(System.Net.WebUtility.HtmlDecode(node.InnerText));
            }
        }
    }
}

