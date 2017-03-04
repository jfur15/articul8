using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HAPtest
{
    public class GetText
    {
        public GetText()
        {

        }
       public void URLSplit(string url, RichTextBox rtb)
        {
            HtmlAgilityPack.HtmlWeb myWeb = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument myDoc = myWeb.Load(url);

            string nextWord;
            string currentWord;
            string cutWord;
            int indexOfPeriod;
            int nextWordNum;

            // Use space as delimiter to split string into individual words.
            char[] delimiters = new char[] { ' ', ',' };

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Split Article 1~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//


            // Extract data from first article.
            string title1 = myDoc.DocumentNode.SelectSingleNode("//title").InnerText;
            string mainText1 = myDoc.DocumentNode.SelectSingleNode("//body").InnerText;

            // Split main body of text from article 1 into an array of strings.
            string[] parts = mainText1.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            rtb.AppendText(title1 + "\n\n\n");

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
                            rtb.AppendText(currentWord + " ");
                            temp += currentWord + " ";
                        }
                        // If the current word ends in a period...
                        else if (currentWord.Last() == '.')
                        {
                            // Move down 2 next lines. (New Sentence)
                            rtb.AppendText(currentWord + " " + "\n\n");
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
                                    rtb.AppendText(currentWord.Substring(0, indexOfPeriod));
                                    rtb.AppendText("\n\n\n" + (currentWord.Substring(indexOfPeriod + 1)) + " ");

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
                                    rtb.AppendText(currentWord.Substring(0, j));
                                    rtb.AppendText("\n\n\n" + (currentWord.Substring(j + 1)) + " ");

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
                                    rtb.AppendText(currentWord + " ");
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
                    rtb.AppendText(currentWord + " ");
                    temp += currentWord + " ";
                }
            }
        }
    }
}
