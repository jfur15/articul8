using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.ie.crf;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.tagger.maxent;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.coref;
namespace HAPtest
{
    static class NLPObjs
    {
        static CRFClassifier _cfier;
        static TokenizerFactory _tizer;

        public static CRFClassifier cfier
        {
            get { return _cfier; }
            set { _cfier = value; }
        }
        public static TokenizerFactory tizer
        {
            get { return _tizer; }
            set { _tizer = value; }
        }
    }
}
