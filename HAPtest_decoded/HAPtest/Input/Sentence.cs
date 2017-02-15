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
                    grade += 1;
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
            for (int i = 1; i < words.Count; i++)
            {
                if(char.IsUpper(words[i][0]))
                {
                    grade += 1;
                    break;
                }

            }


            if (text.Contains("“") || text.Contains("”") || text.Contains("\""))
            {
                grade += 3;
            }
            if (text.Contains(')'))
            {
                grade -= 5;
            }


        }
    }
}
