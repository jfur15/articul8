using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace HAPtest
{
    class Sentence
    {
        public List<string> words;
        string text;
        int grade = 0;
        List<string> wLeaders = new List<string>() {"Trump", "Netanyahu", "Trudeau",
                "May", "Putin", "Al-Assad", "Merkel", "Jong-Un", "Castro", "Sanders",
                "Khamenei", "Abe", "Jin-Ping", "Francis", "Gentiloni" };

        public string Text { get { return text; } set { text = value; } }
        public int Grade { get { return grade; } }

        public Sentence(string aSentence)
        {
            text = aSentence;

            words = new List<string>();

            // Use delimiters to split string into individual words.
            string[] delimiters = new string[] { " ", ".", ",", "\n", "\n\t", "\t" };

            // Split sentence into array of strings/words
            string[] arrayWords = aSentence.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            // Convert array of words into a list
            for (int i = 0; i < arrayWords.Length; i++)
            {
                words.Add(arrayWords[i].Trim());
            }

            gradeSentence();
        }

        // Set grade variable to determine priority of string
        private void gradeSentence()
        {
            foreach (string word in words)
            {
                if (word.Any(char.IsDigit))
                {
                    DateTime temp = new DateTime();
                    //improve...
                    if (DateTime.TryParse(word, out temp))
                    {
                        if (DateTime.Parse(word).Year > 1800 && DateTime.Parse(word).Year < 2050)
                        {
                            grade += 1;
                        }
                    }
                    else { grade += 1; }
                }
            }

            string g = System.Net.WebUtility.HtmlDecode("&quot;");

            if (text.Contains("“") || text.Contains("”") || text.Contains("\""))
            {
                grade += 3;
            }
            if (text.Contains(')'))
            {
                grade -= 5;
            }

            bool upper = false;
            for (int i = 1; i < words.Count; i++)
            {
                if (wLeaders.Contains(words[i]))
                {
                    grade += 4;
                }
                else if(char.IsUpper(words[i][0]))
                {
                    if (upper == false)
                    {
                        grade += 1;
                        upper = true;
                    }
                }
            }


        }
    }
}
