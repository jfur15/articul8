using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPtest
{
    class Sentence
    {
        List<string> words;
        string text;
        int grade;

        public string Text { get { return text; } set { text = value; } }
        public int Grade { get { return grade; } }

        public Sentence(string aSentence)
        {
            text = aSentence;

            words = new List<string>();

            // Use delimiters to split string into individual words.
            char[] delimiters = new char[] { ' ', '.', ',' };

            // Split sentence into array of strings/words
            string[] arrayWords = aSentence.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            // Convert array of words into a list
            for (int i = 0; i < arrayWords.Length; i++)
            {
                words.Add(arrayWords[i]);
            }
        }

        // Set grade variable to determine priority of string
        private void gradeSentence()
        {
        }
    }
}
