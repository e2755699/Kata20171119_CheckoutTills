using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kata20171119_CheckoutTills
{
    [TestClass]
    public class Kata
    {
        [TestMethod]
        public void InputQueueIs_1_2_3_4_andOneTill()
        {
            //arrange
            var queue = new[] {1, 2, 3, 4};
            var tills = 1;
            var expect = 10;
            //act
            int actual = new CheckoutTills().CaculateRequestTime(queue, tills);
            //assert
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void InputQueueIs_2_3_4_5_andOneTill()
        {
            AssertShouldBe(new[] { 2, 3, 4, 5 }, 1, 14);
        }

        [TestMethod]
        public void InputQueueIs_1_2_andTwoTill()
        {
            AssertShouldBe(new[] { 1, 2}, 2, 2);
        }

        [TestMethod]
        public void InputQueueIs_1_2_3_andTwoTill()
        {
            AssertShouldBe(new[] { 1, 2 ,3}, 2, 4);
        }


        private static void AssertShouldBe(int[] queue, int tills, int expect)
        {
            //act
            int actual = new CheckoutTills().CaculateRequestTime(queue, tills);
            //assert
            Assert.AreEqual(expect, actual);
        }
    }

    public class CheckoutTills
    {
        public int CaculateRequestTime(int[] queue, int tills)
        {
            if (tills == 1)
            {
                return queue.Aggregate((result, a) => result + a);
            }

            return Math.Max(queue[0], queue[1]);
        }
    }
}
