using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shuffler
{
    public partial class Shuffler : Form
    {
        public Shuffler()
        {
            InitializeComponent();
        }
        List<String> listPaths = new List<String>();
        String selectedPath;

        private void Shuffler_Load(object sender, EventArgs e)
        {

        }



        void DirSearch(string sDir)
        {
            try
            {
                dataGridView.Rows.Clear();
                listPaths.Clear();

                foreach (string f in GetFiles(sDir, "*.mp3|*.wav|*.acc|*.pcm|*.aiff|*.ogg|*.wma|*.flac|.alac"))
                {
                    Console.WriteLine(f);
                    dataGridView.Rows.Add(f, f);
                    listPaths.Add(f);
                }
            }
            catch (Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }



        public void ChooseFolder()
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = folderBrowserDialog.SelectedPath;
                Console.WriteLine(selectedPath);
                DirSearch(selectedPath);
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            ChooseFolder();
        }

        private void btnShuffler_Click(object sender, EventArgs e)
        {
            UnshufflerMusic();
            ShufflerMusic();

        }

        public string[] GetFiles(string SourceFolder, string Filter)
        {
            // ArrayList will hold all file names
            ArrayList alFiles = new ArrayList();

            // Create an array of filter string
            string[] MultipleFilters = Filter.Split('|');

            // for each filter find mathing file names
            foreach (string FileFilter in MultipleFilters)
            {
                // add found file names to array list
                alFiles.AddRange(Directory.GetFiles(SourceFolder, FileFilter));
            }

            // returns string array of relevant file names
            return (string[])alFiles.ToArray(typeof(string));
        }

        private void ShufflerMusic()
        {
            int count = dataGridView.Rows.Count;
            Random rand = new Random();
            List<int> listNumbers = new List<int>();
            int number;
            for (int i = 1; i < count; i++)
            {
                do
                {
                    number = rand.Next(1, count);
                } while (listNumbers.Contains(number));
                listNumbers.Add(number);
            }
            foreach (int i in listNumbers)
            {
                string fileName;
                if (selectedPath.EndsWith(":\\"))
                {
                    fileName = listPaths[i - 1].Substring(selectedPath.Length);
                }
                else
                {
                    fileName = listPaths[i - 1].Substring(selectedPath.Length + 1);
                }

                if (Math.Floor(Math.Log10(listNumbers[i - 1]) + 1) == 1)
                {
                    File.Move(listPaths[i - 1], selectedPath + "\\" + "0000" + listNumbers[i - 1] + "%-%" + fileName);
                    Console.WriteLine("0000" + listNumbers[i - 1] + "%-%" + fileName);
                }
                else if (Math.Floor(Math.Log10(listNumbers[i - 1]) + 1) == 2)
                {
                    File.Move(listPaths[i - 1], selectedPath + "\\" + "000" + listNumbers[i - 1] + " %-%" + fileName);
                    Console.WriteLine("000" + listNumbers[i - 1] + "%-%" + fileName);
                }
                else if (Math.Floor(Math.Log10(listNumbers[i - 1]) + 1) == 3)
                {
                    File.Move(listPaths[i - 1], selectedPath + "\\" + "00" + listNumbers[i - 1] + "%-%" + fileName);
                    Console.WriteLine("00" + listNumbers[i - 1] + "%-%" + fileName);
                }
                else if (Math.Floor(Math.Log10(listNumbers[i - 1]) + 1) == 4)
                {
                    File.Move(listPaths[i - 1], selectedPath + "\\" + "0" + listNumbers[i - 1] + "%-%" + fileName);
                    Console.WriteLine("0" + listNumbers[i - 1] + "%-%" + fileName);
                }
                else
                {
                    File.Move(listPaths[i - 1], selectedPath + "\\" +listNumbers[i - 1] + "%-%" + fileName);
                    Console.WriteLine(listNumbers[i - 1] + "%-%" + fileName);
                }

            }
            DirSearch(selectedPath);
        }


        private void UnshufflerMusic()
        {
            int count = dataGridView.Rows.Count;
            for (int i = 1; i < count; i++)
            {
                string fileName;
                if (selectedPath.EndsWith(":\\"))
                {
                    fileName = listPaths[i - 1].Substring(selectedPath.Length);
                }
                else
                {
                    fileName = listPaths[i - 1].Substring(selectedPath.Length + 1);
                }
                if (listPaths[i - 1].Contains("%-%"))
                {
                    fileName = "\\" + fileName.Substring(fileName.IndexOf("%-%") + 3);
                    File.Move(listPaths[i - 1], selectedPath + fileName);
                    Console.WriteLine(selectedPath + fileName);
                }
            }
            DirSearch(selectedPath);
        }

        private void btnUnshuffler_Click(object sender, EventArgs e)
        {
            UnshufflerMusic();

        }
    }
}
