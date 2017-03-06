using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.ie.crf;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.tagger.maxent;
using System.Xml;
using java.io;
namespace HAPtest
{
    class Sentence
    {
        public List<string> words;
        public string xmlText="";
        string text;
        int grade = 0;

        public List<string> listLocations;
        public List<string> listOrganizations;
        public List<string> listPersons;
        public List<string> listDates;

        public string Text { get { return text; } set { text = value; } }
        public int Grade { get { return grade; } }

        public Sentence(string aSentence)
        {
            text = aSentence;

            words = new List<string>();
            listLocations = new List<string>();
            listOrganizations = new List<string>();
            listPersons = new List<string>();
            listDates = new List<string>();

            xmlText = HAPtest.NLPObjs.cfier.classifyWithInlineXML(text);
            //xmlText = HAPtest.Cfier.cfier.apply(text);
            //xmlText = HAPtest.Cfier.cfier.classifyToString(text, "xml", false);
            // Convert array of words into a list


            //-----------------------------

            char[] sentenceEnders = { '.', '?', '!' };

            var sr = new StringReader(text);
            var rawWords = HAPtest.NLPObjs.tizer.getTokenizer(sr).tokenize();
            sr.close();
            
            for (int i = 0; i < rawWords.size(); i++)
            {
                var s = rawWords.get(i);
                if (s.ToString() != null)
                {
                    string tempss = s.ToString();
                    words.Add(tempss);
                }
            }



            entitizeSentence();
            gradeSentence();
        }

        private void entitizeSentence()
        {
            if (xmlText != "")
            {
                XmlDocument tempXml = new XmlDocument();
                string tempText = System.Net.WebUtility.HtmlDecode("<bullshit>" + xmlText + "</bullshit>");

                tempText = System.Text.RegularExpressions.Regex.Replace(tempText, "[^\u0000-\u007F]+", "");
                tempText = System.Text.RegularExpressions.Regex.Replace(tempText, "&", " and ");
                tempXml.LoadXml(tempText);

                List<string>[] cls = { listDates,listPersons,listLocations,listOrganizations };
                string[] classifiers = {"DATE", "PERSON", "LOCATION", "ORGANIZATION"};
                for (int i = 0; i < cls.Count()-1; i++)
                {
                    foreach (XmlNode node in tempXml.SelectNodes("//" + classifiers[i]))
                    {
                        cls[i].Add(node.InnerText);
                    }
                }

            }
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
            grade += listLocations.Count + listPersons.Count;

            //Contains a quote?
            if (text.Contains("“") || text.Contains("”") || text.Contains("\""))
            {
                grade += 3;
            }


            if (text.Contains(')'))
            {
                grade -= 5;
            }

            grade += listDates.Count();


        }
    }
}
