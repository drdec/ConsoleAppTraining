using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace ConsoleAppTraining
{
    public class Program
    {
        public static void Main()
        {

            var result = TopWords.Top3("a a c b b");

            foreach (var i in result)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }
    }

    public class TopWords
    {
        public static List<string> Top3(string s)
        {
            char[] symbol = new char[127];

            for (int i = 0; i < 127; i++)
            {
                if ((i < 65 || i > 93) && (i < 96 || i > 122) && (i != 39))
                {
                    symbol[i] = Convert.ToChar(i);
                }
            }

            s = s.ToLower();

            string[] temp = s.Split(symbol, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == "'") continue;

                if (dictionary.ContainsKey(temp[i]))
                {
                    dictionary[temp[i]]++;
                }
                else
                {
                    dictionary.Add(temp[i], 1);
                }
            }

            List<string> list = new List<string>();

            int cz = dictionary.Count > 3 ? 3 : dictionary.Count;

            for (int j = 0; j < cz; j++)
            {
                string res = "";
                int maxValue = 0;

                foreach (KeyValuePair<string, int> i in dictionary.Where(i => i.Value > maxValue))
                {
                    res = i.Key;
                    maxValue = i.Value;
                }

                list.Add(res);
                dictionary.Remove(res);

            }

            for (int i = 0; i < list.Count; i++)
            {
                bool flag = false;
                for (int j = 0; j < list[i].Length; j++)
                {
                    if (list[i][j] == '\'')
                    {
                        try
                        {
                            if (list[i][j + 1] == '\'')
                            {
                                flag = true;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                }

                if (flag)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }

            return list;
        }
    }

}
