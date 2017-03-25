using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Vocabulary_Database_Processing
{
    class VRegex : Regex
    {
        static public bool IsWordEntry(string input)
        {
            string pattern = @"[^a-zA-Z\b\s]";
            Match mat = Match(input, pattern);
            return !mat.Success;
        }
        static public bool IsValidWordEntry(string input)
        {
            string pattern = @"^[a-zA-Z\b\s]+$";
            Match mat = Match(input, pattern);
            return mat.Success;
        }

        static public bool IsValidSingleWordEntry(string input)
        {
            string pattern = @"^[a-zA-Z]+$";
            Match mat = Match(input, pattern);
            return mat.Success;
        }

        static public string getWord(string line)
        {
            string pattern = @" = ";
            string output = "";

            Match mat = Match(line, pattern);
            if (mat.Success)
            {
                output = line.Remove(mat.Index);
            }
            return output;
        }

        //static public string getSingleWord(string line)
        //{
        //    string pattern = @"^[a-zA-Z]+$";
        //    string output = getWord(line);

        //    Match mat = Match(output, pattern);
        //    if (mat.Success)
        //    {
        //        output = line.Remove(mat.Index);
        //    }
        //    return output;
        //}

        static public string GetExpanation(string line, string word)
        {
            string output = "";
            Match mat = Match(line, word + " = ");
            output = line.Remove(0, mat.Length);
            return output;
        }
        static public string GetExpanation(string line)
        {
            string word = getWord(line);
            return GetExpanation(line, word);
        }

        public static bool IsUnsign(string input)
        {
            return Regex.IsMatch(input, @"^[0-9]\d*$");
        }

        public static string BreakToSentencesGRESAT(string input)
        {
            // this method was designed to process GRE/SAT vocabulary database
            #region
            string output = input;
            string pattern = @"\([\w\b]\)|\[GRE\/SAT\]|\[GRE\]|\[SAT\]|\d\.|\s{2}[A-Z]";
            //string pattern = @"\([\w\b]\)|\[GRE\/SAT\]|\[GRE\]|\[SAT\]|\badj\.|\bn\.|\bvt\.|\d\.|\s{2}[A-Z]";

            int y = 0;
            foreach (Match match in Regex.Matches(output, pattern))
            {
                output = output.Insert(match.Index + y, Environment.NewLine);
                y += 2;
            }

            return output;
            #endregion
        }
        public static string BreakToSentencesWordnet(string input)
        {
            string output = "";
            return output;
        }

        public static string BreakToSentencesWordnet(string explanation, WordEntryCollection EntireWordCollection)
        {
            string pattern = @"(v\.)|(n\.)|(adj\.)|(adv\.)";
            string output = explanation;
            int position;
            int lengthIncreament = 0;

            foreach (Match mat in Regex.Matches(explanation, pattern))
            {
                output = output.Insert(mat.Index + mat.Value.Length + lengthIncreament, Environment.NewLine);
                if (mat.Index > 0)
                {
                    output = output.Insert(mat.Index + lengthIncreament, Environment.NewLine);
                    lengthIncreament += 2;
                }
                lengthIncreament += 2;
            }

            pattern = @"[a-zA-Z]+";
            lengthIncreament = 0;
            foreach (Match mat in Regex.Matches(output, pattern))
            {
                // Check if the mat.value is a valid word
                if ((position = EntireWordCollection.FindSimilarity(mat.Value)) > 0)
                {
                    //MessageBox.Show(mat.Value + "   " + position);
                    output = output.Insert(mat.Index + position + lengthIncreament, Environment.NewLine);
                    lengthIncreament += 2;
                }
            }
            return output;
        }

        public static string BreakToSentencesMacMillan(string explanation)
        {
            string htmlTag = @"(<\s*(\S+)(\s[^>]*)?>)|(\/[\s\S]*\/)";
            string htmlNewLn = @"(<\s*\/div\s*>)|(<\s*\/br\s*>)|(<\s*br\s*>)";
            string pron = @"(adjective)|(verb)|(noun)|(adverb)";
            string star = @"\/[\s\S]*\/";
            Match mat;
            //char ch = 28;

            //if (explanation.Contains("MIA is an abbreviation for Missing in Action"))
            //{
            //    MessageBox.Show(explanation);
            //}

            mat = Regex.Match(explanation, pron);
            if (mat.Success)
            {
                explanation = explanation.Remove(0, mat.Index);
            }

            mat = Regex.Match(explanation, htmlNewLn);
            while (mat.Success)
            {
                explanation = explanation.Replace(mat.Value, Environment.NewLine);
                mat = Regex.Match(explanation, htmlNewLn);
            }
            mat = Regex.Match(explanation, htmlTag);
            while (mat.Success)
            {
                explanation = explanation.Replace(mat.Value, "");
                mat = Regex.Match(explanation, htmlTag);
            }
            int i = 0;
            while (i < explanation.Length)
            {
                if (explanation[i] == 0x1C || explanation[i] == 6 || explanation[i] == 0x1D || explanation[i] == 0x13)
                {
                    //MessageBox.Show("Very sfsad'fadslg'algdfg;dfgjkldfgldfgkldfgf");
                    //MessageBox.Show(explanation[i].ToString());
                    //MessageBox.Show(explanation);
                    explanation = explanation.Remove(i, 1);
                }
                else
                    ++i;
            }
            return explanation;
        }

        public static string BreakToSentencesConcise(string explanation, WordEntryCollection EntireCollection)
        {
            string BreakLine = @"(v\.)|(n\.)|(adj\.)|(adv\.)";
            Match mat = Regex.Match(explanation, BreakLine);
            if (mat.Success)
            {
                explanation = explanation.Remove(0, mat.Index);
            }

            //mat = Regex.Match(explanation, BreakLine);
            //int Inc = 0;
            //foreach (Match m in Regex.Matches(explanation, BreakLine))
            //{
            //    explanation = explanation.Insert(mat.Index + mat.Length + Inc, Environment.NewLine);
            //    if (mat.Index != 0)
            //    {
            //        explanation = explanation.Insert(mat.Index + Inc, Environment.NewLine);
            //        Inc += 2;
            //    }
            //    Inc += 2;
            //}

            string complex = @"((v\.)|(n\.)|(adj\.)|(adv\.))([^.]+)((v\.)|(n\.)|(adj\.)|(adv\.))";
            string output = "";
            string single;
            int Inc;
            int position;
            single = explanation;
            while (explanation != null || explanation == "")
            {
                Inc = 1;
                mat = Regex.Match(explanation, BreakLine);
                if (mat.Success)
                {
                    output += mat.Value + Environment.NewLine;
                    explanation = explanation.Replace(mat.Value, "");
                }
                mat = Regex.Match(explanation, BreakLine);
                if (mat.Success)
                {
                    single = explanation.Remove(mat.Index, explanation.Length - mat.Index);
                    explanation = explanation.Remove(0,mat.Index);
                    //MessageBox.Show(explanation);
                }
                else
                {
                    single = explanation;
                    explanation = null;
                }

                //while (mat.Success)
                //{
                //    single = single.Replace(mat.Value, "");
                //    mat = Regex.Match(single, BreakLine);
                //}
                string word = @"[a-zA-Z]+";
                Inc = 0;
                int index = 1;
                single = index.ToString() + ". " + single;
                foreach (Match m1 in Regex.Matches(single, word))
                {
                    if ((position = EntireCollection.FindSimilarity(m1.Value)) > 0)
                    {
                        //MessageBox.Show(mat.Value + "   " + position);
                        ++index;
                        single = single.Insert(m1.Index + position + Inc, Environment.NewLine + index.ToString() + ". ");
                        Inc += 5;
                    }
                }
                output += single + Environment.NewLine;
            }
            //output = BreakToSentencesWordnet(explanation, EntireCollection);
            return output;
        }

    }
}
