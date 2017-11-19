using System;
using System.Collections.Generic;
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

        [TestMethod]
        public void InputQueueIs_1_5_3_andTwoTill()
        {
            AssertShouldBe(new[] { 1, 5, 3 }, 2, 5);
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
        public int CaculateRequestTime(int[] queue, int tillsNumber)
        {
            var tills = new TillCenter(queue, tillsNumber);
            return tills.StartWorking();
        }
    }

    public class TillCenter
    {
        private List<Till> tills;

        private int[] queue;
        private List<Till> freeTills = new List<Till>();
        private int position;

        private int totalRequestTime
        {
            get { return tills.Max(till => till.totalRequestTime); }
        }

        private bool isComplete
        {
            get { return position == queue.Length; }
        }

        public TillCenter(int[] queue, int tillsNumber)
        {
            this.queue = queue;

            for (int i = 0; i < tillsNumber; i++)
            {
                tills.Add(new Till());
            }
        }

        public int StartWorking()
        {
            while (true)
            {
                if (isFreeTill())
                {
                    if (isComplete)
                    {
                        return totalRequestTime;
                    }

                    freeTills.ForEach(till => till.AddRequest(queue[position]));
                }

                reduceOneRequestTime();
            }
        }

        private void reduceOneRequestTime()
        {
            tills.ForEach(till => till.ReduceRequestTime());
        }

        private bool isFreeTill()
        {
            return freeTills.Any();
        }
    }

    public class Till
    {
        private int remaainingTime { get; set; }

        public int totalRequestTime { get; set; }

        public void AddRequest(int requestTime)
        {
            IsFree = false;
            remaainingTime = requestTime;
            totalRequestTime += requestTime;
        }
        
        public bool IsFree { get; internal set; }

        public void ReduceRequestTime()
        {
            remaainingTime--;
            if (remaainingTime == 0)
            {
                IsFree = true;
            }
        }
    }
}
