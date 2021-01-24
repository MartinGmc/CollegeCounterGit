using CollegeCounter.MathHelper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeCounterTests
{
    class StatFunctionsTest
    {
        [SetUp]
        public void Setup()
        {
        }


        /// <summary>
        /// weighted average calculation test
        /// </summary>
        [Test]
        public void WeightedAverageCalculationTest()
        {
            int[] input = new int[] { 1, 2, 11, 25, 88 };
            int[] weights = new int[] { 10, 20, 15, 18, 22 };

            var result = StatFunctions.CalculateWeightedAverage(input, weights);

            Assert.IsTrue(result == 30.6);

        }

        /// <summary>
        /// Median calculation test (odd)
        /// </summary>
        [Test]
        public void MedianOddCalculationTest()
        {
            int[] input = new int[] { 1, 2, 11, 25, 88 };

            var result = StatFunctions.CalculateMedian(input);

            Assert.IsTrue(result == 11);

        }

        /// <summary>
        /// Median calculation test (even)
        /// </summary>
        [Test]
        public void MedianEvenCalculationTest()
        {
            int[] input = new int[] { 1, 2, 11, 24, 88,99 };

            var result = StatFunctions.CalculateMedian(input);

            Assert.IsTrue(result == 17.5);

        }

        /// <summary>
        /// Mode calculation test 
        /// </summary>
        [Test]
        public void ModeCalculationTest()
        {
            int[] input = new int[] { 1, 2, 11, 11, 88, 99 };

            var result = StatFunctions.CalculateModeValue(input);

            Assert.IsTrue(result[0] == 11);

        }


        /// <summary>
        /// average calculation test 
        /// </summary>
        [Test]
        public void AverageCalculationTest()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5 };

            var result = StatFunctions.CalculateAverage(input);

            Assert.IsTrue(result == 3);

        }

    }
}
