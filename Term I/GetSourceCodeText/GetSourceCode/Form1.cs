// Team News Scraper:
// Derek Edwards
// Steven Reeves
// Meagan Olsen
// John Furlan
//
// News article scraping comparison program.
//
// Currently contains hardcoded examples of 2 news articles from the same source: 
// The first was the original article, which was flattering to Presidential Candidate Bernie Sanders. 
// The Second is an altered version of the same article that was released later 
// that day and is highly critical of Bernie Sanders.
//
// Program goal is to extract the facts in the form of bullet points and eventually 
// merge the two and get a clear, unbiased result.

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
            // Transcode for url 1.
            string page1 = transcoder.Transcode(url1, out success);
            // Transcode for url 2.
            string page2 = transcoder.Transcode(url2, out success);

            if (success)
            {
                string nextWord;
                string currentWord;
                string cutWord;
                int indexOfPeriod;
                int nextWordNum;

                // Use space as delimiter to split string into individual words.
                char[] delimiters = new char[] { ' ', ',' };

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Split Article 1~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

                HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                doc1.LoadHtml(page1);

                // Extract data from first article.
                string title1 = doc1.DocumentNode.SelectSingleNode("//title").InnerText;
                string mainText1 = doc1.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerText;

                // Split main body of text from article 1 into an array of strings.
                string[] parts = mainText1.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Split Article 2~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

                HtmlAgilityPack.HtmlDocument doc2 = new HtmlAgilityPack.HtmlDocument();
                doc2.LoadHtml(page2);

                // Extract data from second article.
                string title2 = doc2.DocumentNode.SelectSingleNode("//title").InnerText;
                string mainText2 = doc2.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerText;

                // Split main body of text from article 2 into an array of strings.
                string[] parts2 = mainText2.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Parse Article 1 Into TextBox1~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

                richTextBox1.AppendText(title1 + "\n\n\n");

                List<string> Sentences = new List<string>();
                string temp = "";

                // Format output to left display as individual sentences and paragraphs with line breaks in between.
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    currentWord = parts[i];
                    nextWord = parts[i + 1];
                    indexOfPeriod = (currentWord.IndexOf('.'));

                    cutWord = currentWord.Substring(1);

                    // If current word contains a period
                    if (currentWord.Contains('.'))
                    {
                        // If current word contains one period...
                        if (((currentWord.Count(x => x == '.')) == 1))
                        {
                            // If the current word is a title or contains a number immediately after period...
                            if ((currentWord == "Mr.") || (int.TryParse(nextWord, out nextWordNum)) || (char.IsLower(nextWord, 0)))
                            {
                                // Add current word to text box
                                richTextBox1.AppendText(currentWord + " ");
                                temp += currentWord + " ";
                            }
                            // If the current word ends in a period...
                            else if (currentWord.Last() == '.')
                            {
                                // Move down 2 next lines. (New Sentence)
                                richTextBox1.AppendText(currentWord + " " + "\n\n");
                                Sentences.Add(temp + currentWord + " ");
                                temp = "";
                            }
                            else
                            {
                                try
                                {
                                    // If letter after period is uppercase or punctuation
                                    if (char.IsUpper(currentWord, (indexOfPeriod + 1)) || char.IsPunctuation(currentWord, (indexOfPeriod + 1)))
                                    {
                                        // Move down 3 lines. (New Paragraph)
                                        richTextBox1.AppendText(currentWord.Substring(0, indexOfPeriod));
                                        richTextBox1.AppendText("\n\n\n" + (currentWord.Substring(indexOfPeriod + 1)) + " ");

                                        temp += currentWord.Substring(0, indexOfPeriod);
                                        Sentences.Add(temp);
                                        temp = currentWord.Substring(indexOfPeriod + 1) + " ";
                                       
                                    }
                                }
                                catch (Exception x)
                                {
                                    MessageBox.Show("Error: One Period");
                                }
                            }
                        }
                        // If current word contains more than one period...
                        else if (((currentWord.Count(x => x == '.')) > 1))
                        {
                            // If current word has multiple periods, one at the end...
                            if (currentWord.Last() == '.')
                            {
                                for (int j = 0; j < currentWord.Length - 1; j++)
                                {
                                    if (currentWord[j] == '.')
                                    {
                                        richTextBox1.AppendText(currentWord.Substring(0, j));
                                        richTextBox1.AppendText("\n\n\n" + (currentWord.Substring(j + 1)) + " ");
                                        
                                        temp = temp + currentWord.Substring(0, j);
                                        Sentences.Add(temp);
                                        temp = currentWord.Substring(j + 1) + " ";
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    // If the current word is a title or contains a number immediately after period...
                                    if ((currentWord == "Mr.") || (int.TryParse((parts[indexOfPeriod + 1]), out nextWordNum)))
                                    {
                                        // Add current word to text box
                                        richTextBox1.AppendText(currentWord + " ");
                                        temp += currentWord + " ";
                                    }
                                }
                                catch (Exception x)
                                {
                                    MessageBox.Show("Error: Multiple Periods");
                                }
                            }
                        }
                    }
                    else
                    {
                        // Add current word to text box
                        richTextBox1.AppendText(currentWord + " ");
                        temp += currentWord + " ";
                    }
                }

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Parse Article 1 Into TextBox1~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
                
                richTextBox2.AppendText(title2 + "\n\n\n");

                List<string> Sentences2 = new List<string>();
                string temp2 = "";

                    // Format output to left display as individual sentences and paragraphs with line breaks in between.
                    for (int i = 0; i < parts2.Length - 1; i++)
                    {
                        currentWord = parts2[i];
                        nextWord = parts2[i + 1];
                        indexOfPeriod = (currentWord.IndexOf('.'));

                        cutWord = currentWord.Substring(1);

                        // If current word contains a period
                        if (currentWord.Contains('.'))
                        {
                            // If current word contains one period...
                            if (((currentWord.Count(x => x == '.')) == 1))
                            {
                                // If the current word is a title or contains a number immediately after period...
                                if ((currentWord == "Mr.") || (int.TryParse(nextWord, out nextWordNum)) || (char.IsLower(nextWord, 0)))
                                {
                                    // Add current word to text box
                                    richTextBox2.AppendText(currentWord + " ");
                                    temp2 += currentWord + " ";
                                }
                                // If the current word ends in a period...
                                else if (currentWord.Last() == '.')
                                {
                                    // Move down 2 next lines. (New Sentence)
                                    richTextBox2.AppendText(currentWord + " " + "\n\n");
                                    Sentences2.Add(temp2 + currentWord + " ");
                                    temp2 = "";
                                }
                                else
                                {
                                    try
                                    {
                                        // If letter after period is uppercase or punctuation
                                        if (char.IsUpper(currentWord, (indexOfPeriod + 1)) || char.IsPunctuation(currentWord, (indexOfPeriod + 1)))
                                        {
                                            // Move down 3 lines. (New Paragraph)
                                            richTextBox2.AppendText(currentWord.Substring(0, indexOfPeriod));
                                            richTextBox2.AppendText("\n\n\n" + (currentWord.Substring(indexOfPeriod + 1)) + " ");
                                            
                                            temp2 += currentWord.Substring(0, indexOfPeriod);
                                            Sentences2.Add(temp2);
                                            temp2 = currentWord.Substring(indexOfPeriod + 1) + " ";
                                        }
                                    }
                                    catch (Exception x)
                                    {
                                        MessageBox.Show("Error: One Period");
                                    }
                                }
                            }
                            // If current word contains more than one period...
                            else if (((currentWord.Count(x => x == '.')) > 1))
                            {
                                // If current word has multiple periods, one at the end...
                                if (currentWord.Last() == '.')
                                {
                                    for (int j = 0; j < currentWord.Length - 1; j++)
                                    {
                                        if (currentWord[j] == '.')
                                        {
                                            richTextBox2.AppendText(currentWord.Substring(0, j));
                                            richTextBox2.AppendText("\n\n\n" + (currentWord.Substring(j + 1)) + " ");
                                            temp2 = temp + currentWord.Substring(0, j);
                                            Sentences2.Add(temp2);
                                            temp2 = currentWord.Substring(j + 1) + " ";
                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        // If the current word is a title or contains a number immediately after period...
                                        if ((currentWord == "Mr.") || (int.TryParse((parts2[indexOfPeriod + 1]), out nextWordNum)))
                                        {
                                            // Add current word to text box
                                            richTextBox2.AppendText(currentWord + " ");
                                            temp2 += currentWord + " ";
                                        }
                                    }
                                    catch (Exception x)
                                    {
                                        MessageBox.Show("Error: Multiple Periods");
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Add current word to text box
                            richTextBox2.AppendText(currentWord + " ");
                            temp2 += currentWord + " ";
                        }
                    }
                //Compare here
                List<string> SentencesFinal = new List<string>();

                for (int i = 0; i < Sentences.Count; i++)
                {

                    for (int j = 0; j < Sentences2.Count; j++)
                    {
                        if(Sentences[i] == Sentences2[j])
                        {
                            SentencesFinal.Add(Sentences[i]);
                        }
                    }
                }

                foreach(string s in SentencesFinal)
                {
                    //Append to final text box
                    richTextBox3.AppendText(s + "\n\n");
                }

                }
            }
        }
    }