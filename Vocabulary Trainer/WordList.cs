using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
//using System.Linq;
using System.Xml;

namespace Vocabulary_Database_Processing
{
    class WordList : MyList<WordInfo>
    {
        public static string sDictionary  = @"dictionary";
        public void LoadFromFile(string filename)
        {

        }
        public WordList()
            : base()
        {

        }
        public void LoadXML(string filename)
        {
            #region

            XElement xml_ifile = XElement.Load(filename);
            //XDocument xml_ifile = XDocument.Load(filename);
            string pattern = @"""[\w\b\s]+""";
            var entries = from ent in xml_ifile.Elements("entry")
                          select ent;
            //var entries = xml_ifile.Elements(WordInfo.sWordEntry);
            XmlReader reader = XmlReader.Create(filename);

            //foreach (XElement entry in entries)
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                    if (reader.Name == "entry")
                    {
                        XElement entry = XElement.ReadFrom(reader) as XElement;
                        WordInfo wi = new WordInfo();
                        string S = entry.Element(Derivative.sDerivative).Value;
                        foreach (Match mat in Regex.Matches(S, pattern))
                        {
                            wi.derivatives.Add(mat.Value.Replace(@"""", ""));
                        }
                        wi.infinitive = entry.Element(WordInfo.sInfinitive).Value;

                        //var termElements = from term in entry.Elements(Term.sTerm);
                        //                   select term;
                        var termElements = entry.Elements(Term.sTerm);
                        foreach (XElement term in termElements)
                        {
                            //Term T = new Term();
                            #region
                            //T.meaning.Value = term.Element(Meaning.sMeaning).Value;
                            //S = term.Element(Synonym.sSynonym).Value;
                            //foreach (Match mat in Regex.Matches(S, pattern))
                            //{
                            //    T.synonym.Add(mat.Value.Replace(@"""", ""));
                            //}
                            //S = term.Element(Antonym.sAntonym).Value;
                            //foreach (Match mat in Regex.Matches(S, pattern))
                            //{
                            //    T.antonym.Add(mat.Value.Replace(@"""", ""));
                            //}

                            //var examElement = from examples in term.Elements(Examples.sExample)
                            //                  select examples;
                            ////var examElement = term.Elements(Examples.sExample);
                            //foreach (XElement example in examElement)
                            //{
                            //    T.examples.Add(example.Value);
                            //}
                            #endregion
                            Term T = new Term(term);
                            wi.TermList.Add(T);
                        }
                        this.Add(wi);
                    }
            }
            #endregion
        }
        public XElement ToXElement()
        {
            XElement XE= new XElement(sDictionary);
            foreach (WordInfo w in this)
                XE.Add(w.ToXElement());
            return XE;
        }
    }
}
