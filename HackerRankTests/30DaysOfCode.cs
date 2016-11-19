using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRankTests
{
    class _30DaysOfCode
    {


        private void Review()
        {
            int count = int.Parse(Console.ReadLine());
            List<string> wordList = new List<string>();
            for (int i = 0; i < count; i++)
            {
                wordList.Add(Console.ReadLine());
            }

            foreach (var word in wordList)
            {
                string s1 = string.Empty;
                string s2 = string.Empty;

                for (int i = 0; i < word.Length; i++)
                {
                    if (i == 0 || i % 2 == 0)
                        s1 += word.Substring(i, 1);
                    else
                        s2 += word.Substring(i, 1);
                }
                Console.WriteLine(s1 + " " + s2);
            }
        }
    }
}
