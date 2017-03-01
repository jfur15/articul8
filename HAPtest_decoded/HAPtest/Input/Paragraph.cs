using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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


            // Split paragraph into array of strings

            List<string> arraySentences = new List<string>();

            Regex r = new Regex("(?<=[.?!;])\\s+(?=\\p{Lu})");
            foreach (string s in r.Split(text))
            {
                sentences.Add( new Sentence(s.Trim()));
            }
        }

    }
}
