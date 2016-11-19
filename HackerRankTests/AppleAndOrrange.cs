using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRankTests
{
    class AppleAndOrrange
    {
        static void Main_1(String[] args)
        {
            string[] tokens_s = Console.ReadLine().Split(' ');
            int s = Convert.ToInt32(tokens_s[0]);
            int t = Convert.ToInt32(tokens_s[1]);
            string[] tokens_a = Console.ReadLine().Split(' ');
            int a = Convert.ToInt32(tokens_a[0]);
            int b = Convert.ToInt32(tokens_a[1]);
            string[] tokens_m = Console.ReadLine().Split(' ');
            int m = Convert.ToInt32(tokens_m[0]);
            int n = Convert.ToInt32(tokens_m[1]);
            string[] apple_temp = Console.ReadLine().Split(' ');
            int[] apple = Array.ConvertAll(apple_temp, Int32.Parse);
            string[] orange_temp = Console.ReadLine().Split(' ');
            int[] orange = Array.ConvertAll(orange_temp, Int32.Parse);

            int appCount=0;
            foreach (int  app in apple)
            {
                int landing = a + app;
                if (landing >= s && landing <= t)
                    appCount++;
            }

            Console.WriteLine(appCount);

            int oraCount = 0;
            foreach (int ora in orange)
            {
                int landing = b + ora;
                if (landing >= s && landing <= t)
                    oraCount++;
            }
            Console.WriteLine(oraCount);
        }
    }
}
