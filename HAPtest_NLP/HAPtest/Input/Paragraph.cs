using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using java.io;
using System.Text.RegularExpressions;
using edu.stanford.nlp.tagger.maxent;
using java.util;
using System.Xml;

namespace HAPtest
{
    public class Paragraph
    {
        private string text;
        public string xmlText = "";
        int grade = 0;
        public List<string> listLocations;
        public List<string> listOrganizations;
        public List<string> listPersons;
        public List<string> listDates;
        public string Text { get { return text; } set { text = value; } }
        public int Grade { get { return grade; } }
        public Paragraph(string aParagraph)
        {
            text = aParagraph;
            listLocations = new List<string>();
            listOrganizations = new List<string>();
            listPersons = new List<string>();
            listDates = new List<string>();
            xmlText = HAPtest.NLPObjs.cfier.classifyWithInlineXML(System.Text.RegularExpressions.Regex.Replace(text, "<|>", ""));
            entitizeParagraph();

            gradeParagraph();

            /*
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
            }*/
        }
        private void entitizeParagraph()
        {
            if (xmlText != "")
            {
                XmlDocument tempXml = new XmlDocument();
                string tempText = System.Net.WebUtility.HtmlDecode("<bullshit>" + xmlText + "</bullshit>");

                tempText = System.Text.RegularExpressions.Regex.Replace(tempText, "[^\u0000-\u007F]+", "");
                tempText = System.Text.RegularExpressions.Regex.Replace(tempText, "&", " and ");

                tempXml.LoadXml(tempText);

                List<string>[] cls = { listDates, listPersons, listLocations, listOrganizations };
                string[] classifiers = { "DATE", "PERSON", "LOCATION", "ORGANIZATION" };
                for (int i = 0; i < cls.Count() - 1; i++)
                {
                    foreach (XmlNode node in tempXml.SelectNodes("//" + classifiers[i]))
                    {
                        cls[i].Add(node.InnerText);
                    }
                }

            }
        }

        // Set grade variable to determine priority of string
        private void gradeParagraph()
        {

            /*foreach (string word in words)
            {
                if (word.Any(char.IsDigit))
                {
                    grade += 1;
                }

            }*/
            grade += listLocations.Count + listPersons.Count;
            grade += listLocations.Count + listPersons.Count;

            //Contains a quote?
            bool x = text.Contains("“");
            if (text.Contains("“") || text.Contains("”") || text.Contains("\""))
            {
                grade += 6;
            }


            if (text.Contains(')'))
            {
                grade -= 10;
            }

            grade += listDates.Count();


        }

        public List<string> listClassifiers()
        {
            List<string> classes = new List<string>();
            classes.AddRange(listOrganizations);
            classes.AddRange(listLocations);
            classes.AddRange(listDates);
            classes.AddRange(listPersons);
            return classes;
        }

    }
}
