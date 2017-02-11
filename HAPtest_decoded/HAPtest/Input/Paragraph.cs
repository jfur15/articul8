using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAPtest
{
    class Paragraph
    {
        List<string> Sentences;
        string text;

        public string Text { get { return text; } set { text = value; } }

        public Paragraph(string aParagraph)
        {
            Sentences = new List<string>();
            text = aParagraph;
        }
    }
}
