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
            string tempString = "";
            int wordLen = 0; //wordLen represents how long a word has to be before we can split a sentence.

            char[] sentenceEnders = { '.', '?', '!' };

            //Iterate through every single character of aParagraph
            for (int i = 0; i < aParagraph.Length; i++)
            {
                //Add each character to a temporary string and wordLen
                tempString = tempString + aParagraph[i];
                wordLen++;

                //If a space appears, we need to reset wordLen.
                if (char.IsWhiteSpace(aParagraph[i]))
                {
                    wordLen = 0;
                }

                //If there are at least 4 characters before a sentence ender, we split the paragraph
                if (sentenceEnders.Contains(aParagraph[i]) && wordLen >= 4)
                {
                    arraySentences.Add(tempString);//Add to the main list of sentences
                    tempString = "";        //Reset the temporary string
                    wordLen = 0;            //reset the word length
                }
            }

            foreach (string s in arraySentences)
            {
                sentences.Add( new Sentence(s.Trim()));
            }
        }

    }
}
