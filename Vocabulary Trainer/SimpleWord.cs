using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Vocabulary_Database_Processing
{
    class SimpleWord 
    {
        public static string sEntry = @"entry";
        public static string sFrom = @"form";
        public static string sDerivatives = @"derivative";
        public static string sInfinitive = @"infinitive";
        public static string sMeaning = @"meaning";
        public string word;
        public string inf;
        public List<string> derivateives;
        public string explanation;
        public SimpleWord()
        {
            derivateives = new List<string>();
        }
        public bool UpdateWordNExplanation(string inputLIne)
        {
            string s = VRegex.getWord(inputLIne);
            //if (VRegex.IsValidWordEntry(s))
            if (VRegex.IsValidSingleWordEntry(s))
            {
                this.word = s;
                this.inf = s;
                this.AddDerivative(s);
                explanation = VRegex.GetExpanation(inputLIne, s);
                return true;
            }
            else
                return false;
        }
        public bool UpdateWordNInf(string inputLIne)
        {
            string s = VRegex.getWord(inputLIne);
            if (VRegex.IsValidWordEntry(s))
            {
                this.word = s;
                this.inf = VRegex.GetExpanation(inputLIne, s);
                this.AddDerivative(s);
                this.AddDerivative(inf);
                return true;
            }
            else
                return false;
        }
        public void AddDerivative(string s)
        {
            if (!derivateives.Contains(s))
                derivateives.Add(s);
        }
        public SimpleWord(XElement XE)
        {
            
        }
        public XElement ToXElementInfinitiveDerivative()
        {
            XElement XE = new XElement(sEntry);
            XE.Add(new XElement(sInfinitive, inf));
            string str = "";
            foreach (string s in derivateives)
                str += @"""" + s + @"""" + " ";
            XE.Add(new XElement(sDerivatives, str));
            XE.Add(new XElement(sFrom, word));
            return XE;
        }

    }
    /*
    class  CompareSimpleWordByInf : IComparer<SimpleWord>
    {
        public int Compare(SimpleWord w1, SimpleWord w2)
        {
            if (w1 == null)
            {
                if (w2 == null)
                    return  0;
                else
                    return  -1;
            }
            else
            {
                if (null == w2)
                    return 1;
                else
                    return String.Compare(w1.inf, w2.inf, true);
            }
        }
    }
     * */
}
