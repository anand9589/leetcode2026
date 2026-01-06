using Common;
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

        #region 6 --> 1161. Maximum Level Sum of a Binary Tree

        [Test]
        public void MaxLevelSumTest1()
        {
            TreeNode treeNode = new TreeNode(1,
                new TreeNode(7,
                    new TreeNode(8), new TreeNode(-7))
                , new TreeNode(0));

            var k = january.MaxLevelSum(treeNode);
            Assert.That(k, Is.EqualTo(2));
        }
        #endregion

        #region 5 --> 1975. Maximum Matrix Sum

        [Test]
        public void MaxMatrixSumTest1()
        {
            string s = "[[1,2,3],[-1,-2,-3],[1,2,3]]";
            int[][] matrix = Helper.GetMultiDimensionalArrayBasedOnString(s);
            var k = january.MaxMatrixSum(matrix);
            Assert.That(k, Is.EqualTo(16));
        }

        [Test]
        public void MaxMatrixSumTest2()
        {
            string s = "[[-1,0,-1],[-2,1,3],[3,2,2]]";
            int[][] matrix = Helper.GetMultiDimensionalArrayBasedOnString(s);
            var k = january.MaxMatrixSum(matrix);
            Assert.That(k, Is.EqualTo(15));
        }

        [Test]
        public void MaxMatrixSumTest3()
        {
            string s = "[[2,9,3],[5,4,-4],[1,7,1]]";
            int[][] matrix = Helper.GetMultiDimensionalArrayBasedOnString(s);
            var k = january.MaxMatrixSum(matrix);
            Assert.That(k, Is.EqualTo(34));
        }

        #endregion

        #region 4 --> 1390. Four Divisors


        [Test]
        public void SumFourDivisorsTest1()
        {
            var k = january.SumFourDivisors(new int[] { 21, 4, 7 });
            Assert.That(k.Equals(32));
        }
        #endregion

        #region 3 --> 1411. Number of Ways to Paint N × 3 Grid


        [Test]
        public void NumOfWaysTest1()
        {
            int k = january.NumOfWays(1);
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