using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using HtmlAgilityPack;
using System.Security;
using System.Xml;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.ie.crf;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.tagger.maxent;
using edu.stanford.nlp.coref;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.simple;
using edu.stanford.nlp.time;
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
            var x = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory("../../../../..");
            x = Directory.GetCurrentDirectory();
            HAPtest.NLPObjs.cfier = CRFClassifier.getClassifierNoExceptions("edu\\stanford\\nlp\\models\\ner\\english.muc.7class.nodistsim.crf.ser.gz");
            x = Directory.GetCurrentDirectory();
            HAPtest.NLPObjs.tizer = PTBTokenizer.factory(new CoreLabelTokenFactory(), "asciiQuotes");
        }

        // Primary function: click button to process input
        private void button1_Click(object sender, EventArgs e)
        {
            string[] urls = new string[] { "https://www.nytimes.com/2017/02/24/us/politics/white-house-sean-spicer-briefing.html",

            "http://thedailybanter.com/2017/02/trump-administration-bans-cnn/",


       " http://www.politicususa.com/2017/02/24/trump-hide-truth-banning-media-write-scandal-press-briefing.html",


       // "http://www.zerohedge.com/news/2017-02-24/white-house-bans-cnn-nyt-participating-media-briefing",


       " http://www.politico.com/story/2017/02/reporters-blocked-white-house-gaggle-235360",


       " https://www.washingtonpost.com/lifestyle/style/cnn-new-york-times-other-media-barred-from-white-house-briefing/2017/02/24/4c22f542-fad5-11e6-be05-1a3817ac21a5_story.html",


      "  http://www.foxnews.com/politics/2017/02/24/media-outlets-accuse-white-house-blocking-certain-press-from-covering-event.html",


       " http://www.vox.com/policy-and-politics/2017/2/24/14729078/white-house-banned-media-outlets-press-briefing",


      "  https://www.conservativereview.com/commentary/2017/02/media-freak-out-when-white-house-blocks-cnn-and-others-from-press-gaggle",


      "  https://www.theguardian.com/us-news/2017/feb/24/media-blocked-white-house-briefing-sean-spicer",


       //" http://www.mediaite.com/online/cnn-and-other-media-outlets-blocked-from-white-house-gaggle/",



       " http://www.huffingtonpost.com/entry/white-house-bars-news-organizations_us_58b08a76e4b0a8a9b78213ae"

};
            List<ComparisonPool> pooledParagraphs = new List<ComparisonPool>();

            // Create a list to house all articles inputted
            List<Article> allArticles = new List<Article>();

            // Remove tabs from any previous button clicks
            for (int i = tabControl1.TabPages.Count; i > 1; i--) { tabControl1.TabPages.Remove(tabControl1.TabPages[i - 1]); }

            //  Extract URLs from TextBoxes, error check, and create article for each
            if (checkBox1.Checked)
            {
                foreach (string s in urls)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        // Is it actually a URL?
                        if (Uri.IsWellFormedUriString(s, UriKind.Absolute))
                        {
                            // Create new article object and add it to list of all articles
                            Article newArticle = new Article(s);
                            allArticles.Add(newArticle);
                        }
                    }
                }
            }
            else
            {
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
            }

            // Error Checking: User must enter two URLs miniumum
            if (allArticles.Count() < 2) { MessageBox.Show("Invalid Input: Please enter URL into at least 2 fields"); }


            // Use URLGet function to retrieve URLs from textbox input and proceed to extract website text and assign it to
            // each Article object in primary list in paragraph form
            for (int i = 0; i < allArticles.Count; i++) { URLGet(allArticles[i]); }




            tempSentenceProcess(allArticles);




            // Format output to GUI. Create/Label a tab for each article and output text in paragraph form to RichTextBox in each.
            // Also sentence tabs with ordered scores and shit
            int idx = 1;
            foreach (Article A in allArticles)
            {
                TabControl mainTempTab = new TabControl();
                TabPage mainTempContainer = new TabPage();
                mainTempContainer.Text = "Article " + idx;
                TabPage tempTab = new TabPage();
                RichTextBox tempRtb = new RichTextBox();

                foreach (Paragraph p in A.paragraphs)
                {
                    //richTextBox1.AppendText(p.Text + "\n\n\n");
                    tempRtb.AppendText(p.Text + "\n\n\n");
                }
                tempTab.Text = "Paragraphs";
                tempTab.Controls.Add(tempRtb);
                tempRtb.Dock = DockStyle.Fill;
                mainTempTab.TabPages.Add(tempTab);

                //---------

                //----------

                tempTab = new TabPage();
                ListView tempView = new ListView();
                tempView.View = View.Details;
                tempView.GridLines = true;
                tempView.Columns.Add(new ColumnHeader().Name = "Score");
                tempView.Columns.Add(new ColumnHeader().Name = "Text");
                foreach (Paragraph p in A.paragraphs)
                {

                    //richTextBox1.AppendText(s.Text + "\n\n\n");
                    tempView.Items.Add(new ListViewItem(new string[] { p.Grade.ToString(), p.Text }));
                    //tempRtb.AppendText(s.Text + "|" + s.Grade + "\n\n\n");



                }
                tempView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                tempTab.Text = "Importance";
                tempTab.Controls.Add(tempView);
                tempView.Dock = DockStyle.Fill;
                mainTempTab.TabPages.Add(tempTab);

                //----

                tempTab = new TabPage();
                tempView = new ListView();
                tempView.View = View.Details;
                tempView.GridLines = true;
                tempView.Columns.Add(new ColumnHeader().Name = "Element");

                tempView.Columns.Add(new ColumnHeader().Name = "Text");
                foreach (Paragraph p in A.paragraphs)
                {


                    List<string>[] cls = { p.listDates, p.listPersons, p.listLocations, p.listOrganizations };
                    string[] classiferNames = { "DATE", "PERSON", "LOCATION", "ORGANIZATION" };

                    bool paragraphAdded = false;
                    for (int i = 0; i < cls.Length; i++)
                    {

                        foreach (string entity in cls[i])
                        {

                            tempView.Items.Add(new ListViewItem(new string[] { classiferNames[i], entity }));

                            if (p.Text.Contains("attacks on the press"))
                            {
                                int x = 1;
                            }
                            if (classiferNames[i] == "PERSON" || classiferNames[i] == "LOCATION" || classiferNames[i] == "ORGANIZATION")
                            {

                                bool existingClassifier = false;
                                foreach (ComparisonPool C in pooledParagraphs)
                                {
                                    if (C.Classifier == entity)
                                    {
                                        if (!paragraphAdded)
                                        {
                                            C.addParagraph(p);
                                            paragraphAdded = true;
                                        }
                                        existingClassifier = true;
                                        break;
                                    }
                                }
                                if (existingClassifier == false && !paragraphAdded)
                                {
                                    ComparisonPool temp = new ComparisonPool(entity);
                                    pooledParagraphs.Add(temp);
                                    temp.addParagraph(p);
                                    paragraphAdded = false;
                                    i = cls.Length;
                                    break;
                                }
                            }
                        }
                    }
                    //richTextBox1.AppendText(node.InnerText + "   ");




                }
                tempView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                tempTab.Text = "Subjects";
                tempTab.Controls.Add(tempView);
                tempView.Dock = DockStyle.Fill;
                mainTempTab.TabPages.Add(tempTab);




                mainTempTab.Dock = DockStyle.Fill;
                mainTempContainer.Controls.Add(mainTempTab);
                tabControl1.TabPages.Add(mainTempContainer);
                idx++;


            }

            foreach (Paragraph pp in comparison(pooledParagraphs))
            {
                richTextBox1.AppendText(pp.Text + "\n\n\n");
            }

        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

        //Debug method of comparison, only outputs the pools
        private List<Paragraph> testcomparison(List<ComparisonPool> pooledParagraphs)
        {

            List<ComparisonPool> tempPooled = new List<ComparisonPool>();
            foreach (ComparisonPool C in pooledParagraphs)
            {
                if (C.Pool.Count > 2)
                {
                    tempPooled.Add(C);
                }
            }
            pooledParagraphs = tempPooled;

            List<Paragraph> finalParagraphs = new List<Paragraph>();
            Paragraph tempParagraph;



            foreach (ComparisonPool C in pooledParagraphs)
            {
                foreach (Paragraph p1 in C.Pool)
                {

                    tempParagraph = p1;


                    finalParagraphs.Add(tempParagraph);
                }

            }
            return finalParagraphs;
        }


        //Real comparison
        private List<Paragraph> comparison(List<ComparisonPool> pooledParagraphs)
        {

            List<ComparisonPool> tempPooled = new List<ComparisonPool>();
            foreach (ComparisonPool C in pooledParagraphs)
            {
                if (C.Pool.Count > 2)
                {
                    tempPooled.Add(C);
                }
            }
            pooledParagraphs = tempPooled;

            List<Paragraph> finalParagraphs = new List<Paragraph>();
            Paragraph tempParagraph;



            foreach (ComparisonPool C in pooledParagraphs)
            {
                List<Paragraph> tempPool = new List<Paragraph>();
                foreach (Paragraph p1 in C.Pool.ToList())
                {

                    tempParagraph = p1;
                    tempPool = new List<Paragraph>();
                    foreach (Paragraph p2 in C.Pool.ToList())
                    {
                        if (p1.Text != p2.Text && !p1.Deleted && !p2.Deleted)
                        {
                            if (classifierCompare(p1.listClassifiers(), p2.listClassifiers()))
                            {
                                if (p1.Grade > p2.Grade)
                                {
                                    tempParagraph = p1;
                                    //delete p2
                                    p2.Deleted = true;
                                }
                                else if (p2.Grade > p1.Grade)
                                {
                                    tempParagraph = p2;
                                    //delete p1
                                    p1.Deleted = true;
                                }
                                else
                                {
                                    if (p1.Text.Length < p2.Text.Length)
                                    {

                                        tempParagraph = p2;
                                        //delete p1
                                        p1.Deleted = true;
                                    }
                                    else
                                    {
                                        tempParagraph = p1;
                                        //delete p2
                                        p2.Deleted = true;
                                    }
                                }
                            }
                            else if (p1.listClassifiers() != p2.listClassifiers())
                            {
                                continue;
                            }
                        }
                    }

                    finalParagraphs.Add(tempParagraph);
                    C.Pool.RemoveAll(item => tempPool.Contains(item));
                }

            }
            return finalParagraphs;
        }

        //Throws out all pargraphs with grade lower than 1
        //Supposed to hold all comparison logic
        private void tempSentenceProcess(List<Article> allArticles)
        {
            foreach (Article a in allArticles)
            {
                //This holds all sentences with a grade higher than 1
                List<Paragraph> tempParagraphs = new List<Paragraph>();

                for (int sidx = 0; sidx < a.paragraphs.Count; sidx++)
                {
                    if (a.paragraphs[sidx].Grade > 1 && !tempParagraphs.Contains(a.paragraphs[sidx]))
                    {
                        tempParagraphs.Add(a.paragraphs[sidx]);
                    }
                }
                
                //Rewrites the original sentence list with the reconstructed one
                a.paragraphs = tempParagraphs;
            }

        }
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
            //anArticle.title = System.Net.WebUtility.HtmlDecode(getHtmlWeb.DocumentNode.SelectSingleNode("//title").InnerText);
            HtmlNodeCollection n = getHtmlWeb.DocumentNode.SelectNodes("//p | div[contains (@class, body)]");
            if (n.Count > 0)
            {
                foreach (HtmlNode node in n)
                {
                    anArticle.AddParagraph(System.Net.WebUtility.HtmlDecode(node.InnerText));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = tabControl1.TabPages.Count; i > 1; i--) { tabControl1.TabPages.Remove(tabControl1.TabPages[i - 1]); }
            foreach (Control b in Articles.Controls)
            {
                b.Text = "";
                b.BackColor = Color.White;
            }
            richTextBox1.Text = "";
        }

        private List<string> javastrings(java.util.List bs)
        {
            List<string> tempo = new List<string>();
            for (int i = 0; i < bs.size(); i++)
            {
                tempo.Add((string)bs.get(i));
            }
            return tempo;
        }
        private List<int> javanums(java.util.List bs)
        {
            List<int> tempo = new List<int>();
            for (int i = 0; i < bs.size(); i++)
            {
                tempo.Add((int)bs.get(i));
            }
            return tempo;
        }

        private bool classifierCompare(List<string> p1c, List<string> p2c)
        {
            List<string> cmpA;
            List<string> cmpB;


            if (p1c.Count > p2c.Count)
            {
                cmpA = p1c;
                cmpB = p2c;
            }
            else
            {
                cmpB = p1c;
                cmpA = p2c;
            }
            int sim = 0;
            foreach (string smstring in cmpB)
            {
                if (cmpA.Contains(smstring))
                {
                    sim++;
                }
            }
            if (sim > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //private List<List<Paragraph>> PoolParagraphs(Paragraph current, string keySubject)
        //{

        //}
    }
}

