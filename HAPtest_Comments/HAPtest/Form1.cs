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
            HtmlAgilityPack.HtmlDocument getHtmlWeb = new HtmlAgilityPack.HtmlDocument();
            getHtmlWeb = webPage.Load(textBox1.Text);

            //getHtmlWeb.OptionAutoCloseOnEnd = true;
            //getHtmlWeb.OptionOutputAsXml = true;


            //int numTextBoxes = 2;
            //string textBoxContent;
            // parseURL(textBox1.Text);
            //parseURL(textBox2.Text);

            var textNodes = getHtmlWeb.DocumentNode.SelectNodes("//p");
            foreach (HtmlAgilityPack.HtmlTextNode node in textNodes)
            {
                node.Text = node.Text.Replace("\"", "&quot;");

                richTextBox1.Text += node.InnerText;

                richTextBox1.AppendText("\n\n\n");
            }

            //foreach (HtmlNode node in getHtmlWeb.DocumentNode.SelectNodes("//p"))
            //{
            //    richTextBox1.Text += node.InnerText;

            //    richTextBox1.AppendText("\n\n\n");

            //}

            //foreach (HtmlNode node in getHtmlWeb.DocumentNode.SelectNodes("//p"))
            //{
            //    //pseudocode
            //    //if (node.Contains("&quot"))
            //    //{
            //    //    replace with "\""
            //    //}
                

            //    richTextBox1.Text += node.InnerText;

            //    richTextBox1.AppendText("\n\n\n");
            //}


//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

            HtmlWeb webPage1 = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument getHtmlWeb1 = webPage1.Load(textBox2.Text);
            getHtmlWeb1.OptionAutoCloseOnEnd = true;

            //int numTextBoxes = 2;
            //string textBoxContent;
            // parseURL(textBox1.Text);
            //parseURL(textBox2.Text);

            foreach (HtmlNode node in getHtmlWeb1.DocumentNode.SelectNodes("//p"))
            {
                richTextBox2.Text += node.InnerText;

                richTextBox2.AppendText("\n\n\n");
            }

            //for (int i = 0; i < numTextBoxes; i++)
            //{
            ////IMPORTANT: Iterate through i textboxes
            //textBoxContext = "textBox" + i;
            //parseURL(textBoxContent.Text);
            //}
        }

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        //private void parseURL(string givenURL)
        //{
        //    HtmlWeb webPage = new HtmlWeb();
        //    HtmlAgilityPack.HtmlDocument getHtmlWeb = webPage.Load(givenURL);

        //    foreach (HtmlNode node in getHtmlWeb.DocumentNode.SelectNodes("//p"))
        //    {
        //        richTextBox1.Text += node.InnerHtml;

        //        richTextBox1.AppendText("\n\n\n");
        //    }
        //}
        
    }
}