using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary_Database_Processing
{
    class CompareWordInfoByInfinitive : Comparer<WordInfo>
    {
        public override int Compare(WordInfo w1, WordInfo w2)
        {
            if (w1 != null)
            {
                if (w2 != null)
                    return String.Compare(w1.infinitive, w2.infinitive, true);
                else
                    return 1;
            }
            else
            {
                if (w2 == null)
                    return 0;
                else
                    return -1;
            }
        }
    }



    class CompareMyWordInfoByRank : Comparer<MyWordInfo>
    {
        public override int Compare(MyWordInfo w1, MyWordInfo w2)
        {
            return (w1.rank - w2.rank);
        }
    }


    class CompareSimpleWordByInf : IComparer<SimpleWord>
    {
        public int Compare(SimpleWord w1, SimpleWord w2)
        {
            if (w1 == null)
            {
                if (w2 == null)
                    return 0;
                else
                    return -1;
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
}
