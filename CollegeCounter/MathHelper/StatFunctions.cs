using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeCounter.MathHelper
{
    public static class StatFunctions
    {
        /// <summary>
        /// method retun weightedaverage of input
        /// assume that values[1] have weight weights[1]
        /// </summary>
        /// <param name="values">list of values</param>
        /// <param name="weights">list of weights</param>
        /// <returns>calculated weighted average</returns>
        public static double CalculateWeightedAverage(int[] values, int[] weights)
        {
            double sumOfWeights = 0;
            double averageMultiplicated = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sumOfWeights = sumOfWeights + weights[i];
                averageMultiplicated = averageMultiplicated + (values[i] * weights[i]);
            }
            return (averageMultiplicated / sumOfWeights);
        }

        /// <summary>
        /// method returns average from input values
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateAverage(int[] values)
        {
            int sumOfValues = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sumOfValues = sumOfValues + values[i];
            }
            return (sumOfValues / values.Length);
        }

        /// <summary>
        /// method returns median from input values
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static double CalculateMedian(int[] values)
        {
            values = values.OrderBy(p => p).ToArray();
            if (values.Length % 2 == 0)
            {
                int index1 = (values.Count() / 2);
                
                double result = (double)(values[index1 - 1] + values[index1] ) / 2;
                return result;

            }
            else
            {
                int index = (int) Math.Ceiling((double)values.Count() / 2);
                return values[index-1];
            }
        }

        /// <summary>
        /// method returns Mode for input values
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<int> CalculateModeValue(int[] values)
        {
            List<NumberWithCounts> counts = new List<NumberWithCounts>();

            foreach (var item in values)
            {
                var checkExists = counts.FirstOrDefault(p => p.number == item);
                if (checkExists == null)
                {
                    counts.Add(new NumberWithCounts()
                    {
                        number = item,
                        counts = 1
                    });
                }
                else checkExists.counts = checkExists.counts + 1;
            }
            var countsmax = counts.Max(p => p.counts);
            var vals = counts.Where(p => p.counts == countsmax).ToList();
            List<int> result = new List<int>();
            foreach (var item in vals)
            {
                result.Add(item.number);
            }
            return result;
        }

        private class NumberWithCounts
        {
            public int number { get; set; }
            public int counts { get; set; }
        }
    }
}
