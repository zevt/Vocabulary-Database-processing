using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Vocabulary_Database_Processing
{

    class Synonym : List<string>
    {
        public static string sSynonym = "synonym";
        public Synonym()
            : base()
        {

        }

        public Synonym(XElement xelement)
        {
            if (xelement != null)
            {
                string s = xelement.Value;
                string pattern = @"""[\w\b\s]+""";
                foreach (Match match in Regex.Matches(s, pattern))
                    this.Add(match.Value.Replace(@"""", ""));
            }
        }

        public Synonym(string InStr)
            : base()
        {

        }

        public XElement ToXElement()
        {
            return new XElement(sSynonym, this.ToString());
        }
        public new string ToString()
        {
            string str = "";
            foreach (string s in this)
            {
                str += @"""" + s + @"""" + " ";
            }
            if (!str.Equals(""))
                str = sSynonym + ": " + str + "\n";
            return str;
        }
        public void ReadString(string Instr)
        {
            string pattern = @"[a-zA-Z]+";
            foreach (Match match in Regex.Matches(Instr, pattern))
            {
                this.Add(match.Value);
            }
        }

        public new void Add(string syn)
        {
            if (!this.Contains(syn))
                base.Add(syn);
        }

    }

    class Antonym : List<string>
    {
        public static string sAntonym = "antonym";
        public Antonym(string InStr = "")
            : base()
        {

        }
        public Antonym(XElement xelement)
        {
            if (xelement != null)
            {
                string s = xelement.Value;
                string pattern = @"""[\w\b\s]+""";
                foreach (Match match in Regex.Matches(s, pattern))
                    this.Add(match.Value.Replace(@"""", ""));
            }
        }

        public XElement ToXElement()
        {
            return new XElement(sAntonym, this.ToString());
        }
        public new string ToString()
        {
            string str = "";
            foreach (string s in this)
            {
                str += @"""" + s + @"""" + " ";
            }
            if (!str.Equals(""))
                str = sAntonym + ": " + "\n";
            return str;
        }

        public new void Add(string ant)
        {
            if (!this.Contains(ant))
                base.Add(ant);
        }
        public void ReadString(string Instr)
        {
            string pattern = @"[a-zA-Z]+";
            foreach (Match match in Regex.Matches(Instr, pattern))
            {
                this.Add(match.Value);
            }
        }
    }

    class Examples : List<string>
    {
        public static string sExample = @"example";
        public Examples()
            : base()
        {

        }
        public Examples(string sentence)
            : base()
        {
            this.Add(sentence);
        }

        public void Add(XElement xelement)
        {
            if (xelement != null)
            {
                string s = xelement.Value;
                this.Add(s);
            }
        }

        public Examples(List<XElement> examples)
        {
            foreach (XElement ex in examples)
                this.Add(ex.Value);
        }

        public List<XElement> ToListOfXElement()
        {

            List<XElement> ex = new List<XElement>();
            foreach (string s in this)
            {
                ex.Add(new XElement(sExample, s));
            }
            return ex;
        }
        public new string ToString()
        {
            string str = "";
            foreach (string s in this)
                str += s;
            if (!str.Equals(""))
                str += "\n";
            return str;
        }
        public new void Add(string sentence)
        {
            if (!this.Contains(sentence))
                base.Add(sentence);
        }
    }

    class Meaning
    {
        public static string sMeaning = @"meaning";

        public string Value { get; set; }
        public Meaning(string Instr = "")
        {
            UpdateMeaning(Instr);
        }

        public Meaning(XElement XE)
        {
            this.Value = XE.Value;
        }
        public new string ToString()
        {
            return Value + "\n";
        }
        public void UpdateMeaning(string Instr)
        {
            this.Value = Instr;
        }

        public XElement ToXElement()
        {
            return new XElement(sMeaning, Value);
        }
    }

    class Term
    {
        public static string sTerm = @"term";
        public static string stype = @"type";
        public Meaning meaning;
        public Synonym synonym;
        public Antonym antonym;
        public Examples examples;
        public string type;
        public Term()
        {
            meaning = new Meaning();
            synonym = new Synonym();
            antonym = new Antonym();
            examples = new Examples();
        }
        public Term(string InStr)
        {
            meaning = new Meaning(InStr);
            synonym = new Synonym(InStr);
            antonym = new Antonym(InStr);
            examples = new Examples(InStr);
        }

        public Term(XElement XE)
        {
            if (XE != null)
            {
                type = XE.Attribute(stype).Value;
                meaning = new Meaning(XE.Element(Meaning.sMeaning));
                synonym = new Synonym(XE.Element(Synonym.sSynonym));
                antonym = new Antonym(XE.Element(Antonym.sAntonym));
                examples = new Examples();
                foreach (XElement xe in XE.Elements(Examples.sExample))
                    examples.Add(xe.Value);
            }
        }
        public new string ToString()
        {
            string str = "";
            str += meaning.ToString() + synonym.ToString() + antonym.ToString() + examples.ToString();
            return str;
        }
        public XElement ToXElement()
        {
            XElement XEterm = new XElement(sTerm, new XAttribute(stype, type), meaning.ToXElement(),
                synonym.ToXElement(), antonym.ToXElement());
            foreach (XElement ex in examples.ToListOfXElement())
            {
                XEterm.Add(ex);
            }
            return XEterm;
        }
        // Manipulating Examples
        public void AddExample(string sentence)
        {
            examples.Add(sentence);
        }
        public void RemoveExampleAt(int index)
        {
            examples.RemoveAt(index);
        }
        // Manipulate Synonym
        public void AddSynonym(string word)
        {
            synonym.Add(word);
        }
        public void RemoveSynonym(string word)
        {
            synonym.Remove(word);
        }
        // Manipulate Antonym
        public void AddAntonym(string word)
        {
            antonym.Add(word);
        }
        public void RemoveAntonym(string word)
        {
            antonym.Remove(word);
        }

        public void UpdateMeaing(string sentence)
        {
            meaning.UpdateMeaning(sentence);
        }

        public string ToHTML()
        {
            return "";
        }
    }

    class Derivative : List<string>
    {
        public static string sDerivative = "derivative";

        public Derivative(XElement XE)
            : base()
        {
            string pattern = @"""[\w\b\s]+""";
            foreach (Match mat in Regex.Matches(XE.Value, pattern))
            {
                this.Add(mat.Value.Replace(@"""", ""));
            }
        }

        public Derivative()
            : base()
        {
            // TODO: Complete member initialization
        }

        public new bool Add(string s)
        {
            if (!this.Contains(s))
            {
                base.Add(s);
                return true;
            }
            else return false;
        }
        public new string ToString()
        {
            string str = "";
            foreach (string s in this)
            {
                str += @"""" + s + @"""" + " ";
            }
            if (!str.Equals(""))
                str += "\n";
            return str;
        }
        public XElement ToXElement()
        {
            string str = "";
            foreach (string s in this)
            {
                str += @"""" + s + @"""" + " ";
            }
            return new XElement(sDerivative, str);
        }
    }
    class WordInfo : IEquatable<WordInfo>, IComparable<WordInfo>
    {
        public static string sWordEntry = @"entry";
        //public static string sRank = @"rank";
        public static string sInfinitive = @"infinitive";
        public static string sVerb = @"Verb";
        public static string sAdjective = @"Adjective";
        public static string sNoun = @"Noun";

        public string infinitive;
        public Derivative derivatives;
        public List<Term> TermList;

        public WordInfo()
        {
            TermList = new List<Term>();
            derivatives = new Derivative();
        }

        public WordInfo(XElement XE)
        {
            if (XE != null)
            {
                infinitive = XE.Element(sInfinitive).Value;
                derivatives = new Derivative(XE.Element(Derivative.sDerivative));
                TermList = new List<Term>();
                foreach (XElement e in XE.Elements(Term.sTerm))
                {
                    TermList.Add(new Term(e));
                }
            }
        }

        public WordInfo(WordInfo w)
        {
            this.derivatives = w.derivatives;
            this.infinitive = w.infinitive;
            this.TermList = w.TermList;
        }
        public void UpdateWordNetLine(string line)
        {

        }
        public XElement ToXElement()
        {
            XElement XEinfi = new XElement(sInfinitive, infinitive);
            XElement ex = new XElement(sWordEntry, XEinfi, derivatives.ToXElement());
            foreach (Term T in TermList)
            {
                ex.Add(T.ToXElement());
            }
            return ex;
        }
        public new string ToString()
        {
            string str = infinitive + "\n";
            str += derivatives.ToString();
            string VerbTerm = "";
            string AdjTerm = "";
            string NounTerm = "";
            string OtherTerm = "";
            foreach (Term T in TermList)
            {
                if (T.type.Equals(sVerb))
                    VerbTerm += T.ToString();
                else if (T.type.Equals(sAdjective))
                    AdjTerm += T.ToString();
                else if (T.type.Equals(sNoun))
                    NounTerm += T.ToString();
                else
                    OtherTerm += T.ToString();
            }
            if (VerbTerm != "")
                VerbTerm = sVerb + "\n" + VerbTerm;
            if (AdjTerm != "")
                AdjTerm = sAdjective + "\n" + AdjTerm;
            if (NounTerm != "")
                NounTerm = sNoun + "\n" + NounTerm;

            str += VerbTerm + NounTerm + AdjTerm + OtherTerm;
            return str;
        }
        public int CompareTo(WordInfo other)
        {
            return this.infinitive.CompareTo(other.infinitive);
        }

        //public int CompareTo(object other)
        //{

        //}
        public bool Equals(WordInfo other)
        {
            return this.infinitive.Equals(other.infinitive);
        }
        public string ToStringMeaning()
        {
            string str = "";
            //str += derivatives.ToString();
            string VerbTerm = "";
            string AdjTerm = "";
            string NounTerm = "";
            string OtherTerm = "";
            foreach (Term T in TermList)
            {
                if (T.type.Equals(sVerb))
                    VerbTerm += T.ToString();
                else if (T.type.Equals(sAdjective))
                    AdjTerm += T.ToString();
                else if (T.type.Equals(sNoun))
                    NounTerm += T.ToString();
                else
                    OtherTerm += T.ToString();
            }
            if (VerbTerm != "")
                VerbTerm = sVerb + "\n" + VerbTerm;
            if (AdjTerm != "")
                AdjTerm = sAdjective + "\n" + AdjTerm;
            if (NounTerm != "")
                NounTerm = sNoun + "\n" + NounTerm;

            str += VerbTerm + NounTerm + AdjTerm + OtherTerm;
            return str;
        }
    }
}


