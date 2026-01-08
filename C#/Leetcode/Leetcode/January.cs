using Common;

namespace Leetcode
{
    public class January
    {
        private const int MOD = 1000_000_007;
        #region 1 --> 66. Plus One
        public int[] PlusOne(int[] digits)
        {
            List<int> result = new List<int>();
            int carry = 0;
            int curr = digits[digits.Length - 1] + 1;
            if (curr == 10)
            {
                result.Add(0);
                carry = 1;
            }
            else
            {
                result.Add(curr);
            }
            for (int i = digits.Length - 2; i >= 0; i--)
            {
                curr = digits[i] + carry;
                if (curr == 10)
                {
                    result.Insert(0, 0);
                    carry = 1;
                }
                else
                {
                    result.Insert(0, curr);
                    carry = 0;
                }
            }
            if (carry > 0) result.Insert(0, carry);
            return result.ToArray();
        }
        #endregion

        #region 2 --> 
        #endregion

        #region 3 --> 1411. Number of Ways to Paint N × 3 Grid
        int nWays1411_1 = 0;
        int[][] nWaysGrid1411_1 = new int[0][];
        public int NumOfWays_1(int n)
        {
            nWaysGrid1411_1 = new int[n][];
            for (int i = 0; i < n; i++)
            {
                nWaysGrid1411_1[i] = new int[3];
            }

            NumOfWays_1(0, 0);

            return nWays1411_1;
        }

        private void NumOfWays_1(int row, int col)
        {
            if (row == nWaysGrid1411_1.Length)
            {
                nWays1411_1++;
                return;
            }
            if (col == 3)
            {
                NumOfWays_1(row + 1, 0);
                return;
            }

            int lastDigit = 0;
            if (col > 0) lastDigit = nWaysGrid1411_1[row][col - 1];

            int upperDigit = 0;
            if (row > 0) upperDigit = nWaysGrid1411_1[row - 1][col];
            for (int i = 1; i <= 3; i++)
            {

                if (upperDigit == i || lastDigit == i) continue;

                nWaysGrid1411_1[row][col] = i;

                NumOfWays_1(row, col + 1);

                nWaysGrid1411_1[row][col] = 0;
            }
        }

        public int NumOfWays(int n)
        {
            int[][][][] dp = new int[n + 1][][][];
            for (int i = 0; i < n; i++)
            {
                dp[i] = new int[4][][];
                for (int j = 0; j < 4; j++)
                {
                    dp[i][j] = new int[4][];
                    for (int k = 0; k < 4; k++)
                    {
                        dp[i][j][k] = new int[4];
                        for (int l = 0; l < 4; l++)
                        {
                            dp[i][j][k][l] = -1;
                        }
                    }
                }
            }

            return solve(n, 0, -1, -1, -1, dp);
        }
        private int solve(int n, int index, int p1, int p2, int p3, int[][][][] dp)
        {
            if (index == n) return 1;

            if (dp[index][p1 + 1][p2 + 1][p3 + 1] != -1) return dp[index][p1 + 1][p2 + 1][p3 + 1];

            int result = 0;

            for (int i = 0; i < 3; i++)
            {
                if (i == p1) continue;
                for (int j = 0; j < 3; j++)
                {
                    if (i == j || j == p2) continue;
                    for (int k = 0; k < 3; k++)
                    {
                        if (j == k || k == p3) continue;

                        result = (result + solve(n, index + 1, i, j, k, dp)) % MOD;
                    }
                }
            }

            return dp[index][p1 + 1][p2 + 1][p3 + 1] = result;
        }
        #endregion

        #region 4 --> 1390. Four Divisors
        public int SumFourDivisors(int[] nums)
        {
            int result = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();

            foreach (int n in nums)
            {
                if (!map.ContainsKey(n))
                {
                    map[n] = GetDivisorsSum(n);
                }
                result += map[n];
            }

            return result;
        }

        private int GetDivisorsSum(int n)
        {
            if (n <= 7) return 0;
            int i = 2;
            while (n % i != 0)
            {
                i++;
            }


            int n2 = i, n3 = n / i;

            if (n2 == n || n2 == n3) return 0;

            for (i = n2 + 1; i < n3; i++)
            {
                if (n % i == 0) return 0;
            }

            return 1 + n + n2 + n3;

        }
        #endregion

        #region 5 --> 1975. Maximum Matrix Sum
        public long MaxMatrixSum(int[][] matrix)
        {
            long result = 0;

            bool odd = false;
            int se = int.MaxValue;
            foreach (int[] row in matrix)
            {
                foreach (int n in row)
                {
                    int c = n;
                    if (n < 0)
                    {
                        odd = !odd;
                        c = Math.Abs(c);
                    }

                    if (se > c)
                    {
                        se = c;
                    }
                    result += c;
                }
            }

            if (odd)
            {
                result -= (2 * se);
            }

            return result;
        }
        public long MaxMatrixSum1(int[][] matrix)
        {
            long result = 0;

            bool odd = false;
            int se = int.MaxValue;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] <= 0)
                    {
                        odd = !odd;
                        matrix[i][j] *= -1;
                    }

                    if (se > matrix[i][j])
                    {
                        se = matrix[i][j];
                    }
                    result += matrix[i][j];
                }
            }

            if (odd)
            {
                result -= (2 * se);
            }

            return result;
        }
        #endregion

        #region 6 --> 1161. Maximum Level Sum of a Binary Tree
        public int MaxLevelSum(TreeNode root)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();

            int currLevel = 0;
            int result = 0;
            int resultSum = root.val;
            if (root.left != null)
            {
                queue.Enqueue(root.left);
            }
            if (root.right != null)
            {
                queue.Enqueue(root.right);
            }

            while (queue.Count > 0)
            {
                currLevel++;
                int currSum = 0;
                int k = queue.Count;

                while (k-- > 0)
                {
                    var currNode = queue.Dequeue();
                    currSum += currNode.val;
                    if (currNode.left != null)
                    {
                        queue.Enqueue(currNode.left);
                    }
                    if (currNode.right != null)
                    {
                        queue.Enqueue(currNode.right);
                    }
                }

                if (currSum > resultSum)
                {
                    resultSum = currSum;
                    result = currLevel;
                }
            }
            return result + 1;
        }
        #endregion

        #region 7 --> 1339. Maximum Product of Splitted Binary Tree
        long totalSum = 0;
        long maxProduct = 0;
        public int MaxProduct(TreeNode root)
        {
            totalSum = calculateTreeNodeSum(root);
            maxProductFromSubTree(root);
            return (int)(maxProduct % MOD);
        }

        private long maxProductFromSubTree(TreeNode root)
        {
            if (root == null) return 0;

            long currSum = root.val + maxProductFromSubTree(root.left) + maxProductFromSubTree(root.right);

            if (currSum < totalSum)
            {
                long currProduct = currSum * (totalSum - currSum);

                maxProduct = Math.Max(maxProduct, currProduct);
            }

            return currSum;
        }

        private long calculateTreeNodeSum(TreeNode root)
        {
            if (root == null) return 0;

            return root.val + calculateTreeNodeSum(root.left) + calculateTreeNodeSum(root.right);
        }

        Dictionary<TreeNode, long> subtreeMapSum = new Dictionary<TreeNode, long>();
        public int MaxProduct2(TreeNode root)
        {
            totalSum = calculateTreeNodeSum2(root);
            getMaxProduct2(root);
            return (int)(maxProduct % MOD);
        }

        private void getMaxProduct2(TreeNode root)
        {
            if (root != null)
            {

                long currentSubTreeSum = subtreeMapSum[root];

                if (currentSubTreeSum < totalSum)
                {
                    long currProduct = currentSubTreeSum * (totalSum - currentSubTreeSum);

                    maxProduct = Math.Max(maxProduct, currProduct);
                }

                getMaxProduct2(root.left);
                getMaxProduct2(root.right);
            }
        }

        private long calculateTreeNodeSum2(TreeNode root)
        {
            if (root == null) return 0;

            subtreeMapSum[root] = root.val + calculateTreeNodeSum2(root.left) + calculateTreeNodeSum2(root.right);

            return subtreeMapSum[root];
        }

        //public int MaxProduct1(TreeNode root)
        //{
        //    int result = 0;

        //    calculateTreeNodeSum1(root, map);
        //    long sum = map[root];
        //    foreach (TreeNode node in map.Keys)
        //    {
        //        long currTreeNodeSum = map[node];

        //        long n2 = sum - currTreeNodeSum;

        //        int currResult = (int)((currTreeNodeSum * n2) % MOD);

        //        result = Math.Max(result, currResult);
        //    }

        //    return result;
        //}

        //private void calculateTreeNodeSum1(TreeNode root, Dictionary<TreeNode, long> map)
        //{
        //    if (root != null)
        //    {
        //        map[root] = root.val;

        //        if (root.left != null)
        //        {
        //            calculateTreeNodeSum1(root.left, map);
        //            map[root] += map[root.left];
        //        }
        //        if (root.right != null)
        //        {
        //            calculateTreeNodeSum1(root.right, map);
        //            map[root] += map[root.right];
        //        }
        //    }
        //}
        #endregion

        #region 8 --> 1458. Max Dot Product of Two Subsequences
        public int MaxDotProduct(int[] nums1, int[] nums2)
        {
            int m = nums1.Length;
            int n = nums2.Length;
            int[][] dp = new int[m+1][];
            for (int i = 0; i <= m; i++) {
                dp[i] = new int[n + 1];
                for (int j   = 0; j <= n; j++)
                {
                    dp[i][j] = int.MinValue;
                }
            }

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    int prod = nums1[i - 1] * nums2[j - 1] + Math.Max(0, dp[i - 1][j-1]);

                    dp[i][j] = Math.Max(prod, Math.Max(dp[i - 1][j], dp[i][j - 1]));
                }
            }
            return dp[m][n];
        }
        #endregion
    }
}
