using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPtest
{
    class Article
    {
        public List<Paragraph> paragraphs;
        private string uRL;

        public string URL { get { return uRL; } set { uRL = value; } }

        public Article(string aURL)
        {
            paragraphs = new List<Paragraph>();
            uRL = aURL;
        }

        public void AddParagraph(string input)
        {
            if (input.Length >= 10 && input.Contains(" "))
            { 
                paragraphs.Add(new Paragraph(input.Trim()));
            }
            
        }
    }
}
