using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace CaesarCipher
{
    class Program
    {
        static public char[] Alphabet = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        static int key;
        static string CText = System.IO.File.ReadAllText(@"C:\Users\hmammadov\source\repos\CaesarCipher\CaesarCipher\encdata.txt");
        //static string CText = "g fmnc wms bgblr rpylqjyrc gr zw fylb. rfyrq ufyr amknsrcpq ypc dmp. bmgle gr gl zw fylb gq glcddgagclr ylb rfyr'q ufw rfgq rcvr gq qm jmle. sqgle qrpgle.kyicrpylq() gq pcamkkclbcb. lmu ynnjw ml rfc spj";
        static char[] CHtxt = CText.ToCharArray();


        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            KFinder();
            Decrypt();
            //Console.ReadLine();
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);

            Console.ReadLine();
        }


        public static void KFinder()
        {
            int[] alpha = new int[26];
            for (int i = 0; i < CHtxt.Length; i++)
            {
                switch (CHtxt[i])
                {
                    case 'a':
                        alpha[0] += 1;
                        break;
                    case 'b':
                        alpha[1] += 1;
                        break;
                    case 'c':
                        alpha[2] += 1;
                        break;
                    case 'd':
                        alpha[3] += 1;
                        break;
                    case 'e':
                        alpha[4] += 1;
                        break;
                    case 'f':
                        alpha[5] += 1;
                        break;
                    case 'g':
                        alpha[6] += 1;
                        break;
                    case 'h':
                        alpha[7] += 1;
                        break;
                    case 'i':
                        alpha[8] += 1;
                        break;
                    case 'j':
                        alpha[9] += 1;
                        break;
                    case 'k':
                        alpha[10] += 1;
                        break;
                    case 'l':
                        alpha[11] += 1;
                        break;
                    case 'm':
                        alpha[12] += 1;
                        break;
                    case 'n':
                        alpha[13] += 1;
                        break;
                    case 'o':
                        alpha[14] += 1;
                        break;
                    case 'p':
                        alpha[15] += 1;
                        break;
                    case 'q':
                        alpha[16] += 1;
                        break;
                    case 'r':
                        alpha[17] += 1;
                        break;
                    case 's':
                        alpha[18] += 1;
                        break;
                    case 't':
                        alpha[19] += 1;
                        break;
                    case 'u':
                        alpha[20] += 1;
                        break;
                    case 'v':
                        alpha[21] += 1;
                        break;
                    case 'w':
                        alpha[22] += 1;
                        break;
                    case 'x':
                        alpha[23] += 1;
                        break;
                    case 'y':
                        alpha[24] += 1;
                        break;
                    case 'z':
                        alpha[25] += 1;
                        break;
                }
            }
            int maxValue = alpha.Max();
            key = alpha.ToList().IndexOf(maxValue) - 4;
        }

        public static void Decrypt()
        {
            for (int i = 0; i < CHtxt.Length; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    if (CHtxt[i] == Alphabet[j])
                    {
                        CHtxt[i] = Alphabet[(26 + j - key ) % 26];
                        break;
                    }
                }
            }
            string string_object = new string(CHtxt);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string_object);
        }
    }
}
