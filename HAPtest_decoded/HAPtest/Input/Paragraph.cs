using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPtest
{
    class Paragraph
    {
        public List<Sentence> sentences;
        private string text;

        public string Text { get { return text; } set { text = value; } }

        public Paragraph(string aParagraph)
        {
            text = aParagraph;

            sentences = new List<Sentence>();

            // Use period as delimiter to split string into individual sentences.
            char[] delimiters = new char[] { '.' };

            // Split paragraph into array of strings
            string[] arraySentences = aParagraph.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            // Convert array of strings into a list of Sentence objects
            for (int i = 0; i < arraySentences.Length; i++)
            {
                Sentence newSentence = new Sentence(arraySentences[i].Trim());
                sentences.Add(newSentence);
            }
        }
    }
}
