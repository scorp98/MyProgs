using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;

namespace FikaTaskLinq
{
    class Program
    {
        static Dictionary<string, int> CounterD = new Dictionary<string, int>();
        static List<FileInfo> FileList = new List<FileInfo>();
        static List<DirectoryInfo> DirsList = new List<DirectoryInfo>();
        static Dictionary<string, List<string>>Wsearch = new Dictionary<string, List<string>>();
        static Dictionary<string, List<string>> Wsearch1 = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {

            //string X = @"C:\Program Files";
            //X = @"C:\Users\hmammadov\Downloads\Microsoft.SkypeApp_kzf8qxf38zg5c!App\All\Network";
            bool ContDiffStr = true;
            while (ContDiffStr)
            {
                Console.Clear();
                Console.WriteLine("Please input full DirectoryName");
                string X = Console.ReadLine();

                TREE(X);
                bool ContSameStr = true;

                while (ContSameStr)
                {
                    Console.Clear();
                    Console.WriteLine("Chose an option:\n" +
                        "1.total files count\n" +
                        "2.total folders count\n" +
                        "3.total count of files per each existing extension\n" +
                        "4.last modified file\n" +
                        "5.smallest file size\n" +
                        "6.largest file size\n" +
                        "7.order files in ascending mode\n" +
                        "8.order files in descending mode\n" +
                        "9.function that search file by name (starts, contains or ends) using LINQ\n" +
                        "10.function that search file by file extension using LINQ\n" +
                        "11.files that contains same words");

                    int selector = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("\n" +
                        "\n");
                    Console.WriteLine($"{Environment.NewLine}");
                    switch (selector)
                    {
                        case 1:
                            Console.WriteLine(FileList.Count());
                            break;
                        case 2:
                            Console.WriteLine(DirsList.Count());
                            break;
                        case 3:
                            CounterOutput();
                            break;
                        case 4:
                            LFS();
                            break;
                        case 5:
                            SFS();
                            break;
                        case 6:
                            LFS();
                            break;
                        case 7:
                            AFO();
                            break;
                        case 8:
                            DFO();
                            break;
                        case 9:
                            Console.WriteLine("Select fuction to use\n" +
                                "1.Starts\n" +
                                "2.Contains\n" +
                                "3.Ends");
                            int Lselector = Convert.ToInt32(Console.ReadLine());
                            LinqQuery(Lselector);
                            break;
                        case 10:
                            Console.WriteLine("Type Extension to search");
                            string s = Console.ReadLine();
                            ExtSearch(s);
                            break;
                        case 11:
                            WSearcher();
                            break;
                    }

                    Console.WriteLine("\n\n\nDo you want to continue? y/n");
                    string str = Console.ReadLine();
                    if (str == "n")
                    {
                        ContDiffStr = false;
                        ContSameStr = false;
                    }
                    else if (str == "y")
                    {
                        Console.WriteLine("Do you want to contunie with the same Directory? y/n");
                        string str1 = Console.ReadLine();
                        if (str1 == "n")
                        {
                            ContSameStr = false;
                        }
                        
                    }
                }
            }
        }





        static void TREE(string sourceDirectory)
        {
            
            try
            {
                var Dictionaries = Directory.EnumerateDirectories(sourceDirectory);
                if (Dictionaries.Count() != 0)
                {
                    foreach (string currentDir in Dictionaries)
                    {

                        string DirName = Path.GetDirectoryName(currentDir); //currentDir.Substring(sourceDirectory.Length + 1);
                        //Console.WriteLine(currentDir);
                        DirectoryInfo info = new DirectoryInfo(currentDir);

                        DirsList.Add(info);

                        TREE(currentDir);
                    }
                }
                var Files = Directory.EnumerateFiles(sourceDirectory);
                if (Files.Count() != 0)
                {
                    foreach (string currentFile in Files)
                    {
                        string fileName = Path.GetFileName(currentFile); //currentFile.Substring(sourceDirectory.Length + 1);
                        //Console.ForegroundColor = ConsoleColor.Green;
                        //Console.WriteLine(currentFile);
                        //Console.ForegroundColor = ConsoleColor.White;
                        FileInfo info = new FileInfo(currentFile);
                        FileList.Add(info);
                        var ext = Path.GetExtension(fileName);
                        if (string.IsNullOrEmpty(ext))
                            Counter("Files without extension");
                        else
                            Counter(ext);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Counter(string Extension)
        {
            
                if (CounterD.ContainsKey(Extension))
                {
                    CounterD[Extension] += 1;
                }
                else
                {
                    CounterD.Add(Extension, 1);
                }
            
        }

        public static void CounterOutput()
        {
            foreach (KeyValuePair<string, int> KV in CounterD)
            {
                Console.WriteLine(KV.Key + "   " + KV.Value);
            }
        }

        //public static void LinqOUT()
        //{
        //    var x = FileList.Where(f => f.CreationTime == FileList.Max(c => c.CreationTime)).FirstOrDefault();
        //}

        public static void LMF()
        {
            var LMF = FileList.Where(f => f.CreationTime == FileList.Max(c => c.CreationTime)).FirstOrDefault();

            //var LMF = (from f in FileList
            //           orderby f.CreationTime descending
            //           select f.Name).First();
            Console.WriteLine(LMF);
        }

        public static void SFS()
        {
            var SFS = FileList.Where(f => f.Length == FileList.Min(c => c.Length)).FirstOrDefault();

            //var SFS = (from f in FileList
            //           orderby f.Length
            //           select f.Name).First();
            Console.WriteLine(SFS);
        }

        public static void LFS()
        {
            var LFS = FileList.Where(f => f.Length == FileList.Max(c => c.Length)).FirstOrDefault();

            //var LFS = (from f in FileList
            //           orderby f.Length descending
            //           select f.Name).First();
            Console.WriteLine(LFS);
        }

        public static void DFO()
        {
            var DFO = (from f in FileList
                       orderby f.Name descending
                       select f);
            foreach (var t in DFO)
                Console.WriteLine(t);
        }

        public static void AFO()
        {
            var AFO = (from f in FileList
                       orderby f.Name
                       select f);
            foreach (var t in AFO)
                Console.WriteLine(t);
        }

        public static void LinqQuery(int i)
        {
            Console.WriteLine("Enter the searchstring name here: ");
            string sstring = Console.ReadLine();
            switch (i)
            {
                case 1:
                    var query = from f in FileList
                                where f.Name.StartsWith(sstring)
                                select f;
                    foreach (var t in query)
                        Console.WriteLine(t.Name);
                    break;
                case 2:
                    query = from f in FileList
                                where f.Name.Contains(sstring)
                                select f;
                    foreach (var t in query)
                        Console.WriteLine(t.Name);
                    break;
                case 3:
                    query = from f in FileList
                                where f.Name.EndsWith(sstring)
                                select f;
                    foreach (var t in query)
                        Console.WriteLine(t.Name);
                    break;
            }
        }
        public static void ExtSearch(string EXT)
        {
            var query = from f in FileList
                        where f.Extension == EXT
                        select f.Name;
            foreach (var t in query)
                Console.WriteLine(t);
        }

        //s.Split(new char[] { ' ', ',', '.', ';', ':','(',')' });
        public static void WSearcher()
        {
            List<string> WordsSet = new List<string>();
            foreach (var t in FileList)
            {
                var s = t.Name.Split(new char[] { ' ', ',', '.', ';', ':', '(', ')', '-', '_', '{', '}', '\\', '|' });
                foreach (var item in s)
                {
                    WordsSet.Add(item.ToLower());
                    if (Wsearch.ContainsKey(t.Name))
                    {
                        Wsearch[t.Name].Add(item);
                    }
                    else
                    {
                        Wsearch.Add(t.Name, new List<string>());
                        Wsearch[t.Name].Add(item);
                    }
                }
            }
            foreach (KeyValuePair<string, List<string>> KV in Wsearch)
            {
                Console.WriteLine(KV.Key + "   " + KV.Value);
            }
            var x = WordsSet.Distinct();
            foreach (var s in x)
            {
                foreach (KeyValuePair<string, List<string>> KV in Wsearch)
                {
                    if (KV.Value.Contains(s))
                    {
                        if (Wsearch1.ContainsKey(s))
                        {
                            Wsearch1[s].Add(KV.Key);
                        }
                        else
                        {
                            Wsearch1.Add(s, new List<string>());
                            Wsearch1[s].Add(KV.Key);
                        }
                    }
                }
            }
            foreach (KeyValuePair<string, List<string>> KV in Wsearch1)
            {
                if (KV.Value.Count >= 2)
                {
                    Console.WriteLine("\n\n\n\n");
                    Console.WriteLine(KV.Key);
                    foreach (var t in KV.Value)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(t);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
    }
}
