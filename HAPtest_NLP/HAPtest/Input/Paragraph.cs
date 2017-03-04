using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.io;
using System.Text.RegularExpressions;
using edu.stanford.nlp.tagger.maxent;
using java.util;

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

            char[] sentenceEnders = { '.', '?', '!' };

            var sr = new StringReader(text);
            var rawWords = HAPtest.NLPObjs.tizer.getTokenizer(sr).tokenize();
            sr.close();

            string tempString = "";
            for (int i = 0; i < rawWords.size(); i++)
            {
                var s = rawWords.get(i);
                if (s.ToString() != null)
                {
                    string tempss = s.ToString();
                    if (tempss.Length == 1 && sentenceEnders.Contains(tempss[0]))
                    {
                        tempString = tempString + tempss;
                        arraySentences.Add(tempString);
                        tempString = "";
                    }
                    else
                    {
                        tempString = tempString + " " + tempss;
                    }
                }
            }
           
            foreach (string s in arraySentences)
            {
                sentences.Add( new Sentence(s.Trim()));
            }
        }

    }
}
