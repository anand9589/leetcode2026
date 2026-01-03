using Leetcode;

namespace Leetcode2026.Tests
{
    public class Tests
    {
        private January january;

        [SetUp]
        public void Setup()
        {
            january = new January();
        }

        [Test]
        public void Test1()
        {

            Assert.Pass();
        }

        #region 3 --> 1411. Number of Ways to Paint N × 3 Grid


        [Test]
        public void NumOfWaysTest1()
        {
            int k =  january.NumOfWays(1);
            Assert.That(k.Equals(12));
        }


        [Test]
        public void NumOfWaysTest2()
        {
            
            int k = january.NumOfWays(2);
            Assert.That(k.Equals(54));

            k = january.NumOfWays(3);
            Assert.That(k.Equals(246));
            k = january.NumOfWays(4);
            Assert.That(k.Equals(1122));

            k = january.NumOfWays(5);
            Assert.That(k.Equals(5118));
        }


        #endregion
    }
}