using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Vocabulary_Database_Processing
{
    class WordEntryCollection : List<string>
    {
        public new bool Contains(string s)
        {
            int begin = 0;
            int end = this.Count - 1;
            return RecursiceContains(begin, end, s);
        }

        public void LoadFromFile(string filename)
        {
            StreamReader ifile = new StreamReader(filename);
            string s;
            while (null != (s = ifile.ReadLine()))
                Add(s);
            ifile.Close();
        }
        public void SaveToFile(string filename)
        {
            StreamWriter ofile = new StreamWriter(filename);
            foreach (string s in this)
                ofile.WriteLine(s);
            ofile.Close();
        }
        private bool RecursiceContains(int begin, int end, string s)
        {
            #region
            if (String.Equals(this[begin], s, StringComparison.OrdinalIgnoreCase) || String.Equals(this[end], s, StringComparison.OrdinalIgnoreCase))
                return true;
            else
            {
                if (begin + 1 == end) return false;
                else
                {
                    int middle = (begin + end) / 2;
                    if (String.Equals(this[middle], s, StringComparison.OrdinalIgnoreCase))
                        return true;
                    else if (String.Compare(this[middle], s, true) < 0)
                        return RecursiceContains(middle, end, s);
                    else return RecursiceContains(begin, middle, s);
                }
            }
            #endregion
        }
        public int FindSimilarity(string word)
        {
            int begin = 0;
            int end = this.Count - 1;
            word = word.ToLower();
            return RecursiveFindSimilarity(begin, end, word);
        }

        private int RecursiveFindSimilarity(int begin, int end, string s)
        {
            #region
            if (String.Equals(this[begin], s, StringComparison.OrdinalIgnoreCase) || String.Equals(this[end], s, StringComparison.OrdinalIgnoreCase))
                return 0;
            else
            {
                if (begin + 1 == end)
                {
                    int i = begin;
                    while (i >= 0 && !(s.Contains(this[i]) && this.Contains(s.Remove(0, this[i].Length))))
                    {
                        --i;
                    }
                    if (i >= 0)
                        return this[i].Length;
                    else
                        return 0;
                }
                else
                {
                    int middle = (begin + end) / 2;
                    if (String.Equals(this[middle], s, StringComparison.OrdinalIgnoreCase))
                        return 0;
                    else if (String.Compare(this[middle], s, true) < 0)
                        return RecursiveFindSimilarity(middle, end, s);
                    else return RecursiveFindSimilarity(begin, middle, s);
                }
            }
            #endregion
        }
    }
}
