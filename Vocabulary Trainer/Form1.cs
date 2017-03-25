using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace Vocabulary_Database_Processing
{
    public partial class MainForm : Form
    {
        const int Convert_GRE_SAT_to_XML = 0;
        const int Create_Infinitive_Derivative_XML = 1;
        const int Convert_WordNet_to_XML = 2;
        const int Create_Infinitive_List = 3;
        const int Create_Single_Word_Collection = 4;
        const int Create_MacMillan_XML = 5;
        const int Create_ConciseEnglish_XML = 6;
        const int Experiment = 7;

        string fileNamein;
        string fileNameout;

        string EntireWordCollectionFile = @"D:\Coding\C Sharp\Data\EntireWordCollection.txt";
        const string InfinitiveWordfile = @"D:\Coding\C Sharp\Data\InfinitiveCollection.txt";
        public MainForm()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            //  - filter
            //  -- default directory
            if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileNamein = dlgOpen.FileName;
            }
        }

        private void btnCreateRelation_Click(object sender, EventArgs e)
        {
            SimpleWordList sw = new SimpleWordList();
            //SimpleWordList swlOut = new SimpleWordList();
            sw.LoadFromPrimitiveFile(fileNamein);
            int size = sw.Count;
            for (int i = 0; i < size - 1; ++i)
            {
                for (int j = i + 1; j < size; ++j)
                {
                    if (String.Equals(sw[i].explanation, sw[j].explanation))
                    {
                        if (sw[i].inf.Length < sw[j].inf.Length)
                        {
                            sw[j].inf = sw[i].inf;
                            sw[j].derivateives.Add("");
                        }
                        else if (sw[i].inf.Length > sw[j].inf.Length)
                        {
                            sw[i].inf = sw[j].inf;
                        }
                    }
                }
            }

            StreamWriter ofile = new StreamWriter(fileNameout);
            foreach (SimpleWord s in sw)
            {
                ofile.WriteLine(s.word + " = " + s.inf);
            }
            ofile.Close();
            MessageBox.Show("Finish ... ");
        }

        private void btnBrowseIn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            //  - filter
            //  -- default directory
            if (dlgOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileNamein = dlgOpen.FileName;
                tbFileIn.Text = dlgOpen.FileName;
            }
        }

        private void BrowseOut_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlgSave = new SaveFileDialog();
            //  - filter
            //  -- default directory
            if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileNameout = dlgSave.FileName;
                tbFileOut.Text = dlgSave.FileName;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configfile = @"config.txt";
            StreamReader config_file = new StreamReader(configfile);
            fileNamein = tbFileIn.Text = config_file.ReadLine();
            fileNameout = tbFileOut.Text = config_file.ReadLine();
            config_file.Close();
        }
        private void Form_Closing(object sender, EventArgs e)
        {
            StreamWriter config = new StreamWriter("config.txt");
            config.WriteLine(fileNamein);
            config.WriteLine(fileNameout);
            config.Close();
        }

        private void btnInfDer_Click(object sender, EventArgs e)
        {

        }

        private void btnRun_Click(object sender, EventArgs e)
        {

            if (chlbOption.GetItemChecked(Convert_GRE_SAT_to_XML))
            {
                #region
                MyWordList WL = new MyWordList();
                StreamReader ifile = new StreamReader(fileNamein);
                string line;
                while ((line = ifile.ReadLine()) != null)
                {
                    // Load data from file to WL
                    MyWordInfo w = new MyWordInfo();
                    #region
                    char[] Separators = { ' ', '\n' };
                    string[] strspt = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
                    Term T;
                    string s;
                    if (VRegex.IsUnsign(strspt[0]))
                    {
                        w.rank = Convert.ToInt32(strspt[0]);
                        w.infinitive = strspt[1];
                        s = line.Replace(strspt[0] + " " + strspt[1] + " = ", "");
                    }
                    else
                    {
                        w.rank = 0;
                        w.infinitive = strspt[0];
                        s = line.Replace(strspt[0] + " = ", "");
                    }
                    s = VRegex.BreakToSentencesGRESAT(s);
                    T = new Term();
                    T.type = "";
                    T.meaning.UpdateMeaning(s);
                    w.TermList = new List<Term>();
                    w.TermList.Add(T);
                    #endregion
                    WL.Add(w);
                }
                XElement EX = WL.ToXElement();
                EX.Save(fileNameout);
                ifile.Close();
                #endregion
            }

            else if (chlbOption.GetItemChecked(Create_Infinitive_Derivative_XML))
            {
                #region
                SimpleWordList WL = new SimpleWordList();
                WL.LoadFromWordFormNInf(fileNamein);
                WL.Sort(new CompareSimpleWordByInf());
                WL.MergeDerivative();
                //WL.Sort(new CompareSimpleWordByInf());
                int i = 0;
                while (i < WL.Count - 1)
                {
                    if (String.Equals(WL[i].inf, WL[i + 1].inf))
                    {
                        WL.RemoveAt(i + 1);
                    }
                    else
                        ++i;
                }
                XElement XE = WL.ToXElementInfinitiveDerivative();
                XE.Save(fileNameout);
                #endregion
            }


            else if (chlbOption.GetItemChecked(Convert_WordNet_to_XML))
            {
                #region
                WordList WL = new WordList();
                // fileNamein = wordnet.txt
                StreamReader ifile = new StreamReader(fileNamein);
                WordEntryCollection EntireCollection = new WordEntryCollection();
                WordEntryCollection InfinitiveCollection = new WordEntryCollection();
                string line;
                // Load data to EntireCollection
                // Load Data to Infinitive List
                #region
                EntireCollection.LoadFromFile(EntireWordCollectionFile);
                EntireCollection.Sort();
                EntireCollection.SaveToFile(EntireWordCollectionFile);

                InfinitiveCollection.LoadFromFile(InfinitiveWordfile);
                #endregion
                // Set the beginning position to read ifile to 0
                //ifile.BaseStream.Position = 0;
                //ifile.DiscardBufferedData();

                while ((line = ifile.ReadLine()) != null)
                {
                    // Load data from file to WL
                    WordInfo w = new WordInfo();
                    Term T = new Term();
                    string s, explanation;
                    s = VRegex.getWord(line);
                    if (VRegex.IsValidSingleWordEntry(s))// && InfinitiveCollection.Contains(s))
                    {
                        w.infinitive = s;
                        explanation = VRegex.GetExpanation(line, s);
                        explanation = VRegex.BreakToSentencesWordnet(explanation, EntireCollection);
                        T.UpdateMeaing(explanation);
                        T.type = "";
                        w.TermList.Add(T);
                        WL.Add(w);
                    }
                }
                XElement EX = WL.ToXElement();
                EX.Save(fileNameout);
                ifile.Close();
                #endregion
            }
            else if (chlbOption.GetItemChecked(Create_Infinitive_List))
            {
                #region
                StreamReader ifile = new StreamReader(fileNamein);
                StreamWriter ofile = new StreamWriter(fileNameout);
                string line;
                while (null != (line = ifile.ReadLine()))
                {
                    string s = VRegex.getWord(line);
                    if (VRegex.IsValidSingleWordEntry(s))
                    {
                        ofile.WriteLine(VRegex.GetExpanation(line, s));
                    }
                }
                ifile.Close();
                ofile.Close();
                #endregion
            }
            else if (chlbOption.GetItemChecked(Create_Single_Word_Collection))
            {
                #region
                StreamReader ifile = new StreamReader(fileNamein);
                StreamWriter ofile = new StreamWriter(fileNameout);
                string line;
                while (null != (line = ifile.ReadLine()))
                {
                    string s = VRegex.getWord(line);
                    if (VRegex.IsValidSingleWordEntry(s))
                    {
                        ofile.WriteLine(s);
                    }
                }
                ifile.Close();
                ofile.Close();
                #endregion
            }
            else if (chlbOption.GetItemChecked(Experiment))
            {
                #region

                WordEntryCollection EntireCollection = new WordEntryCollection();
                EntireCollection.LoadFromFile(EntireWordCollectionFile);
                //EntireCollection.Sort();
                //EntireCollection.SaveToFile(EntireWordCollectionFile);
                //string sentence = "v.come or bring to a finish or an end; finishHe finished the dishesShe completed the requirements for her Master's DegreeThe fastest runner finished the race in just over 2 hours; others finished in over 4 hoursbring to a whole, with all the necessary parts or elementsA child would complete the familycomplete or carry out; dispatch, dischargedischarge one's dutiescomplete a pass; nailwrite all the required information onto a form; fill out, fill in, make outfill out this questionnaire, please!make out a formadj.having every necessary or normal part or component or stepa complete meala complete wardrobea complete set of the Britannicaa complete set of chinaa complete defeata complete accountingperfect and complete in every respect; having all necessary qualities; consummatea complete gentlemanconsummate happinessa consummate performancehighly skilled; accomplishedan accomplished pianista complete musicianwithout qualification; used informally as (often pejorative) intensifiers; arrant(a), complete(a), consummate(a), double-dyed(a), everlasting(a), gross(a), perfect(a), pure(a), sodding(a), stark(a), staring(a), thoroughgoing(a), utter(a), unadulteratedan arrant foola complete cowarda consummate foola double-dyed villaingross negligencea perfect idiotpure follywhat a sodding messstark staring mada thoroughgoing villainutter nonsensethe unadulterated truthhaving come or been brought to a conclusion; concluded, ended, over(p), all over, terminatedthe harvesting was completethe affair is over, ended, finishedthe abruptly terminated interview  adj.accepting willingly; acceptiveacceptive of every new ideaan acceptant type of mind";
                string sentence = "Britannicaa";
                sentence = VRegex.BreakToSentencesWordnet(sentence, EntireCollection);
                MessageBox.Show(sentence);
                #endregion
                //Load  data for WordList
                #region
                /*
                WordList WL = new WordList();
                WL.LoadXML(fileNamein);
                WordList NewWL = new WordList();
                WL.Sort(new CompareWordInfoByInfinitive());
                //Load Data for SimpleWordList
                //XElement xml_ifile = XElement.Load(fileNamein);
                //xml_ifile.Save(@"D:\Coding\C Sharp\Data\temp.xml");
                //WL.ToXElement().Save(@"D:\Coding\C Sharp\Data\temp.xml");
                
                SimpleWordList SL = new SimpleWordList();
                SL.LoadFromWordFormNInf(@"D:\Coding\C Sharp\Data\Relation_Derivative_To_Infinitive.txt");
                SL.Sort(new CompareSimpleWordByInf());
                SL.MergeDerivative();

                int i = 0;
                while (i < SL.Count - 1)
                {
                    if (String.Equals(SL[i].inf, SL[i + 1].inf))
                        SL.RemoveAt(i + 1);
                    else
                        ++i;
                }
                #endregion

                #region
                int j = 0;
                for (i = 0; i < SL.Count; ++i)
                {
                    while ((j < WL.Count) && (0 < String.Compare(SL[i].inf, WL[j].infinitive, StringComparison.OrdinalIgnoreCase)))
                    {
                        //WL.RemoveAt(j);
                        ++j;
                    }
                    if (j < WL.Count && String.Equals(SL[i].inf, WL[j].infinitive, StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (string s in SL[i].derivateives)
                            WL[j].derivatives.Add(s);
                        NewWL.Add(WL[j]);
                    }
                    //++j;
                }

                XElement XE = NewWL.ToXElement();
                XE.Save(fileNameout);
  */
                ////SL.ToXElementInfinitiveDerivative().Save(@"D:\Coding\C Sharp\Data\temp.txt");
                #endregion

            }
            else if (chlbOption.GetItemChecked(Create_MacMillan_XML))
            {
                #region
                StreamReader ifile = new StreamReader(fileNamein, Encoding.UTF8);
                WordList MacMillan = new WordList();
                string line;
                while (null != (line = ifile.ReadLine()))
                {

                    WordInfo w = new WordInfo();
                    Term T = new Term();
                    string s, explanation;
                    s = VRegex.getWord(line);
                    if (VRegex.IsValidSingleWordEntry(s))// && InfinitiveCollection.Contains(s))
                    {
                        w.infinitive = s;
                        explanation = VRegex.GetExpanation(line, s);
                        explanation = VRegex.BreakToSentencesMacMillan(explanation); //, EntireCollection);
                        T.UpdateMeaing(explanation);
                        T.type = "";
                        w.TermList.Add(T);
                        MacMillan.Add(w);

                    }
                }
                XElement XE = MacMillan.ToXElement();
                XE.Save(fileNameout);
                ifile.Close();
                #endregion
            }
            else if (chlbOption.GetItemChecked(Create_ConciseEnglish_XML))
            {
                #region
                WordList WL = new WordList();
                // fileNamein = concise.txt
                StreamReader ifile = new StreamReader(fileNamein);
                WordEntryCollection EntireCollection = new WordEntryCollection();
                string line;
                // Load data to EntireCollection
                // Load Data to Infinitive List
                
                EntireCollection.LoadFromFile(EntireWordCollectionFile);
                //EntireCollection.Sort();
                //EntireCollection.SaveToFile(EntireWordCollectionFile);

                //InfinitiveCollection.LoadFromFile(InfinitiveWordfile);
                


                while ((line = ifile.ReadLine()) != null)
                {
                    // Load data from file to WL
                    WordInfo w = new WordInfo();
                    Term T = new Term();
                    string s, explanation;
                    s = VRegex.getWord(line);
                    if (VRegex.IsValidSingleWordEntry(s))// && InfinitiveCollection.Contains(s))
                    {
                        w.infinitive = s;
                        explanation = VRegex.GetExpanation(line, s);
                        explanation = VRegex.BreakToSentencesConcise(explanation, EntireCollection);
                        T.UpdateMeaing(explanation);
                        T.type = "";
                        w.TermList.Add(T);
                        WL.Add(w);
                    }
                }
                XElement EX = WL.ToXElement();
                EX.Save(fileNameout);
                ifile.Close();
                #endregion
            }
            MessageBox.Show("Mission completed.");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

