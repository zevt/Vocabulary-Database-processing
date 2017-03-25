using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;
namespace Vocabulary_Database_Processing
{
    class MyWordList : MyList<MyWordInfo>
    {
        public static string sDictionary = @"dictionary";
        public MyWordList()
            : base()
        {

        }

        public MyWordList(MyWordList WL)
        {
            foreach (MyWordInfo w in WL)
                this.Add(w);
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
                        MyWordInfo wi = new MyWordInfo();
                        string S = entry.Element(Derivative.sDerivative).Value;
                        foreach (Match mat in Regex.Matches(S, pattern))
                        {
                            wi.derivatives.Add(mat.Value.Replace(@"""", ""));
                        }
                        wi.infinitive = entry.Element(WordInfo.sInfinitive).Value;
                        wi.Learned = Convert.ToBoolean(entry.Element(MyWordInfo.sLearned).Value);
                        wi.rank = Convert.ToInt32(entry.Element(MyWordInfo.sRank).Value);
                        wi.Last_Update = DateTime.Parse(entry.Element(MyWordInfo.sLastUpdate).Value);
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
            reader.Close();
            #endregion
        }
        public XElement ToXElement()
        {
            XElement XE = new XElement(sDictionary);
            foreach (MyWordInfo w in this)
                XE.Add(w.ToXElement());
            return XE;
        }
        public MyWordInfo PickAWord(int Top = -1, Boolean Difficult = true)
        {

            if (Top == -1 || Top > this.Count)
                Top = this.Count;

            if (Top <= 0)
                return null;
            else
            {
                if (Difficult)
                {
                    #region
                    //this.Sort();
                    int Rank = this[Top - 1].rank;
                    int[][] Level = new int[Rank + 1][];
                    // Level[i][0] presents possibility of appearance of words with rank from 0 to i;
                    // Level[i][1] present total words with rank from 0 to i                
                    for (int i = 0; i <= Rank; ++i)
                        Level[i] = new int[2];
                    int index = 0;

                    for (int i = 0; i <= Rank; ++i)
                    {
                        if (i > 0)
                        {
                            Level[i][0] = Level[i - 1][0];
                            Level[i][1] = Level[i - 1][1];
                        }
                        else
                        {
                            Level[0][0] = 0;
                            Level[0][1] = 0;
                        }
                        index = Level[i][1];
                        while ((index < Top) && (i == this[index].rank))
                        {
                            //Level[i][0] += rank + rank/9 +1 - i;// represents appearance's possibility of words with rank from 0 to current
                            //Level[i][0] += (int)Math.Pow(2, rank - i);
                            //Level[i][1] += 1; // represents total word from rank 0 to current
                            ++index;
                        }
                        if (index > Level[i][1])
                        {
                            Level[i][1] = index;
                            Level[i][0] += (int)Math.Pow(2, Rank - i);
                        }
                    }

                    Random RND = new Random();
                    int rnd = RND.Next(Level[Rank][0]);
                    // determine rank of the word according to a generated  random number
                    index = 0;
                    while ((index <= Rank) && Level[index][0] <= rnd)
                    {
                        ++index;
                    }

                    RND = new Random();
                    // determine a random word with determined rank
                    int LowLimitIndex;
                    if (index == 0)
                        LowLimitIndex = 0;
                    else
                        LowLimitIndex = Level[index - 1][1];
                    rnd = LowLimitIndex + RND.Next(Level[index][1] - LowLimitIndex);
                    return this[rnd];

                    #endregion
                }
                else
                {
                    Random rd = new Random();
                    int index = rd.Next(Top);
                    return this[index];

                }
            }
        }
        public MyWordInfo PickAWord(Boolean Difficult = true)
        {
            return PickAWord(this.Count, Difficult);
        }

        public MyWordList SubList(int Size, bool randomly = true, bool difficult = true)
        {
            MyWordList wl = new MyWordList();
            if (Size <= 0 || this == null || this.Count == 0)
                return null;
            if (Size > this.Count)
                Size = this.Count;
            if (!randomly)
            {
                for (int i = 0; i < Size; ++i)
                    wl.Add(this[i]);
            }
            else if (difficult)
            {
                this.Sort(new CompareMyWordInfoByRank());
                MyWordList temp = new MyWordList(this);
                for (int i = 0; i < Size; ++i)
                {
                    MyWordInfo w1 = temp.PickAWord(difficult);
                    MyWordInfo w2 = temp.PickAWord(difficult);
                    MyWordInfo w = MyWordInfo.PickFromPair(w1, w2);
                    wl.Add(w);
                    temp.Remove(w);
                }
            }
            else
            {
                MyWordList temp = new MyWordList(this);
                for (int i = 0; i < Size; ++i)
                {
                    MyWordInfo w = temp.PickAWord(difficult);
                    wl.Add(w);
                    temp.Remove(w);
                }
            }
            return wl;
        }
    }
}
