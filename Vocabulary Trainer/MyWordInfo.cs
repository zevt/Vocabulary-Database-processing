using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Vocabulary_Database_Processing
{
    class MyWordInfo : WordInfo
    
    {
        public static string sLastUpdate = @"last_update";
        public static string sLearned = "learned";
        public static string sRank = @"rank";
        public DateTime Last_Update { get; set; }
        public int rank { get; set; }
        public bool Learned { get; set; }
        public MyWordInfo() : base() { }

        public MyWordInfo(XElement XE)
            : base(XE)
        {
            if (XE != null)
            {
                XElement e = XE.Element(sRank);
                rank = Convert.ToInt32(e.Value);
                e = XE.Element(sLastUpdate);
                Last_Update = DateTime.Parse(e.Value);
                e = XE.Element(sLearned);
                Learned = Convert.ToBoolean(e.Value);
            }
        }
        public MyWordInfo(WordInfo w)
            : base(w)
        {
            rank = 0;
            Learned = false;
            Last_Update = Convert.ToDateTime("01/01/1900");
        }

        public MyWordInfo(MyWordInfo w)
            : base(w)
        {
            rank = w.rank;
            Learned = w.Learned;
            Last_Update = w.Last_Update;

        }

        public new XElement ToXElement()
        {
            XElement e = base.ToXElement();
            e.Add(new XElement(sRank, rank.ToString()));
            e.Add(new XElement(sLastUpdate, Last_Update.ToString()));
            e.Add(new XElement(sLearned, Learned.ToString()));
            return e;
        }
        public static MyWordInfo PickFromPair(MyWordInfo w1, MyWordInfo w2)
        {
            if (w1.rank < w2.rank) return w1;
            else if (w1.rank > w2.rank) return w2;
            else
            {
                if (w1.Last_Update.CompareTo(w2.Last_Update) <= 0) return w1;
                else return w2;
            }
        }
        public void RankIncrease()
        {
            ++rank;
            Last_Update = DateTime.Now;
        }
        public void RankDecrease()
        {
            rank = rank / 2;
        }
    }
}

