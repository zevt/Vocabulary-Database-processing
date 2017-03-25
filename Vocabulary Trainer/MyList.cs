using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary_Database_Processing
{
    class MyList<T> : List<T> where T : IComparable<T>
    {
        public void BinaryAdd(T other)
        {
            #region
            if (this.Count == 0)
                Add(other);
            else if (this.Count == 1)
            {
                if (this[0].CompareTo(other) < 0)
                    Add(other);
                else if ((this[0].CompareTo(other) > 0))
                    Insert(0, other);
            }
            else
            {
                if (this[Count - 1].CompareTo(other) < 0)
                    Add(other);  // Add to the end
                else if (this[0].CompareTo(other) > 0)
                    Insert(0, other);
                else
                {
                    // Recursive algorithm finding correct position to insert other
                    #region
                    bool finish = false;
                    //bool AlreadyExist = false;
                    int begin, end, index;
                    begin = 0; end = this.Count - 1;
                    index = -1;
                    int middle;
                    // Peform recursive algorithm finding correct position to insert other;
                    while (!finish)
                    {
                        if (begin + 1 == end)
                        {
                            finish = true;
                            if (this[begin].CompareTo(other) < 0 && this[end].CompareTo(other) > 0)
                                index = end;
                        }
                        else
                        {
                            if (this[begin].CompareTo(other) == 0 || this[end].CompareTo(other) == 0)
                                finish = true;
                            else
                            {
                                middle = (begin + end) / 2;
                                if (this[middle].CompareTo(other) > 0)
                                    end = middle;
                                else if (this[middle].CompareTo(other) < 0)
                                    begin = middle;
                                else
                                    finish = true;
                            }
                        }
                    }
                    #endregion
                    if (index > -1)
                    {
                        Insert(index, other);
                    }
                }
            }

            #endregion
        }
        public bool BinaryContains(T item)
        {
            if (this.Count == 0) return false;
            if (this.Count == 1)
                if (this[0].Equals(item))
                    return true;
                else return false;
            int begin = 0;
            int end = this.Count - 1;
            if (this[begin].CompareTo(item) > 0 || this[end].CompareTo(item) < 0)
                return false;
            return (RecursiveBinaryContains(begin, end, item));
        }

        private bool RecursiveBinaryContains(int begin, int end, T item)
        {
            #region
            if (this[begin].Equals(item) || this[end].Equals(item))
                return true;
            else
            {
                if (begin + 1 == end)
                    return false;
                int middle = (begin + end) / 2;
                if (this[middle].CompareTo(item) < 0)
                {
                    return RecursiveBinaryContains(middle, end, item);
                }
                else if (this[middle].CompareTo(item) > 0)
                {
                    return RecursiveBinaryContains(begin, middle, item);
                }
                else return true;
            }
            #endregion
        }
        public int GetIndexOf(T item)
        {
            if (this.Count == 0) return -1;
            if (this.Count == 1)
            {
                if (this[0].Equals(item)) return 0;
                else return -1;
            }
            else
            {
                if (this[0].CompareTo(item) > 0 || this[Count - 1].CompareTo(item) < 0)
                    return -1;
                int begin = 0; 
                int end = Count - 1;
                return RecursiveGetIndexOf(begin, end, item);
            }
        }
        private int RecursiveGetIndexOf(int begin, int end, T item)
        {
            if (this[begin].Equals(item)) return begin;
            if (this[end].Equals(item)) return end;
            if (begin + 1 == end) return -1;
            int middle = (begin + end) / 2;
            if (this[middle].CompareTo(item) > 0)
                return RecursiveGetIndexOf(begin, middle, item);
            else if (this[middle].CompareTo(item) < 0)
                return RecursiveGetIndexOf(middle, end, item);
            else return middle;
        }
    }
}
