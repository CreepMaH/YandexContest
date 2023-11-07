using System;
using System.Collections.Generic;

namespace YandexContest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] nums = Console.ReadLine()?.Trim().Split(' ') ?? throw new ArgumentException();
            int a = Convert.ToInt32(nums[0]),
                b = Convert.ToInt32(nums[1]),
                c = Convert.ToInt32(nums[2]),
                d = Convert.ToInt32(nums[3]);

            int numerator = a * d + b * c;
            int denominator = b * d;

            List<int> numeratorDividers = GetSimpleDividers(numerator);
            //numeratorDividers.Add(numerator);
            List<int> numeratorSimpleDividers = FilterNonSimpleDividers(numerator, numeratorDividers);
            numeratorSimpleDividers.Add(numerator);

            List<int> denominatorDividers = GetSimpleDividers(denominator);
            //denominatorDividers.Add(denominator);
            List<int> denominatorSimpleDividers = FilterNonSimpleDividers(denominator, denominatorDividers);
            denominatorSimpleDividers.Add(denominator);

            int maxCommonDivider = GetMaxCommonDivider(numeratorSimpleDividers, denominatorSimpleDividers);
            Console.WriteLine($"{numerator / maxCommonDivider} {denominator / maxCommonDivider}");
        }

        private static List<int> GetSimpleDividers(int num)
        {
            List<int> simpleDividers = new List<int>();

            int currentNum = num;
            int currentDivider = 2;

            while (currentDivider < num)
            {
                int divRest = currentNum % currentDivider;
                if (divRest == 0)
                {
                    currentNum /= currentDivider;
                    if (!simpleDividers.Contains(currentDivider))
                    {
                        simpleDividers.Add(currentDivider);
                    }
                }
                else
                {
                    currentDivider++;
                }
            }
            return simpleDividers;
        }

        private static List<int> FilterNonSimpleDividers(int num, List<int> dividers)
        {
            List<int> result = new List<int>();

            bool[] nonSimpleDividersMap = new bool[num];
            foreach (int div in dividers)
            {
                if (nonSimpleDividersMap[div - 1] != true)
                {
                    result.Add(div);
                }

                int currentDiv = div * 2;
                while (currentDiv <= num)
                {
                    nonSimpleDividersMap[currentDiv - 1] = true;
                    currentDiv *= 2;
                }
            }

            return result;
        }

        private static int GetMaxCommonDivider(List<int> dividers1, List<int> dividers2)
        {
            int maxCommonDivider = 1;
            int minCount = Math.Min(dividers1.Count, dividers2.Count);

            for (int i = 0; i < minCount; i++)
            {
                if (dividers1[i] == dividers2[i])
                {
                    maxCommonDivider *= dividers1[i];
                }
            }

            return maxCommonDivider;
        }
    }
}