using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Vocabulary_Database_Processing
{
    class SimpleWordList : List<SimpleWord> 
    {
        public static string sDictionary = @"dictionary";
        public SimpleWordList()
            : base()
        {

        }
        public void LoadFromPrimitiveFile(string infile)
        {
            if (!System.IO.File.Exists(infile))
                return;
            StreamReader ifile = new StreamReader(infile);
            string line;
            SimpleWord sw;
            while ((line = ifile.ReadLine()) != null)
            {
                sw = new SimpleWord();
                if (sw.UpdateWordNExplanation(line))
                {
                    this.Add(sw);
                }
            }
            ifile.Close();
        }

        public void LoadFromWordFormNInf(string infile)
        {
            if (!System.IO.File.Exists(infile))
                return;
            StreamReader ifile = new StreamReader(infile);
            string line;
            SimpleWord sw;
            while ((line = ifile.ReadLine()) != null)
            {
                sw = new SimpleWord();
                if (sw.UpdateWordNInf(line))
                {
                    this.Add(sw);
                }
            }
            ifile.Close();
        }

        public XElement ToXElementInfinitiveDerivative()
        {
            XElement XE = new XElement(sDictionary);
            foreach (SimpleWord sw in this)
            {
                XE.Add(sw.ToXElementInfinitiveDerivative());
            }
            return XE;
        }
        public void MergeDerivative()
        {
            int i, j;
            int size = this.Count;
            i = 0;
            while (i < this.Count )
            {
                this[i].AddDerivative(this[i].inf);
                this[i].AddDerivative(this[i].word);
                j=i+1;
                while (j < this.Count && String.Equals(this[i].inf, this[j].inf))
                {
                    this[j].derivateives = this[i].derivateives;
                    this[i].AddDerivative(this[j].word);
                    ++j;
                }
                i = j;
            }
        }
    }
}
