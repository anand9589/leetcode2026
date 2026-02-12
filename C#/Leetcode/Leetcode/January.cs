using Common;
using System.Security.Cryptography;

namespace Leetcode
{
    public class January
    {
        #region x -->
        #endregion
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
            int[] dp = new int[n + 1];

            for (int i = 0; i <= n; i++)
            {
                dp[i] = int.MinValue;
            }

            for (int i = 1; i <= m; i++)
            {
                int last = int.MinValue;

                for (int j = 1; j <= n; j++)
                {
                    int old = dp[j];

                    int prod = nums1[i - 1] * nums2[j - 1] + Math.Max(0, last);

                    dp[j] = Math.Max(prod, Math.Max(dp[j], dp[j - 1]));

                    last = old;
                }
            }

            return dp[n];
        }
        public int MaxDotProduct1(int[] nums1, int[] nums2)
        {
            int m = nums1.Length;
            int n = nums2.Length;
            int[][] dp = new int[m + 1][];
            for (int i = 0; i <= m; i++)
            {
                dp[i] = new int[n + 1];
                for (int j = 0; j <= n; j++)
                {
                    dp[i][j] = int.MinValue;
                }
            }

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    int prod = nums1[i - 1] * nums2[j - 1] + Math.Max(0, dp[i - 1][j - 1]);

                    dp[i][j] = Math.Max(prod, Math.Max(dp[i - 1][j], dp[i][j - 1]));
                }
            }
            return dp[m][n];
        }
        #endregion

        #region 9 --> 865. Smallest Subtree with all the Deepest Nodes

        /**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
            public TreeNode SubtreeWithAllDeepest(TreeNode root)
            {
                return GetDeepest(root, 0).node;
            }

            private (TreeNode node, int deep) GetDeepest(TreeNode node, int deep)
            {
                if (node == null) return (node, 0);

                (var leftNode, var leftDeep) = GetDeepest(node.left, deep + 1);
                (var rightNode, var rightDeep) = GetDeepest(node.right, deep + 1);

                if (leftDeep > rightDeep) return (leftNode, leftDeep + 1);
                else if (leftDeep < rightDeep) return (rightNode, rightDeep + 1);
                else
                    return (node, leftDeep + 1);
            }
 */

        public TreeNode SubtreeWithAllDeepest(TreeNode root)
        {
            return SubtreeWithAllDeepestHelper(root, 0).node;
        }

        private (TreeNode node, int level) SubtreeWithAllDeepestHelper(TreeNode root, int currLevel)
        {

            if (root == null) return (root, 0);

            (TreeNode leftNode, int leftLevel) = SubtreeWithAllDeepestHelper(root.left, currLevel + 1);
            (TreeNode rightNode, int rightLevel) = SubtreeWithAllDeepestHelper(root.right, currLevel + 1);

            if (leftLevel > rightLevel) return (leftNode, leftLevel + 1);
            if (rightLevel > leftLevel) return (rightNode, rightLevel + 1);

            return (root, currLevel);

        }

        public TreeNode getSmallestNode(TreeNode node1, TreeNode node2)
        {
            if (node1.val < node2.val) return node1;
            return node2;
        }
        #endregion

        #region 10 --> 712. Minimum ASCII Delete Sum for Two Strings

        public int MinimumDeleteSum(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;

            int[] dp = new int[n + 1];

            for (int j = 1; j <= n; j++)
            {
                dp[j] = dp[j - 1] + s2[j - 1];
            }

            for (int i = 1; i <= m; i++)
            {
                int prevDiag = dp[0];
                dp[0] += s1[i - 1];

                for (int j = 1; j <= n; j++)
                {
                    int temp = dp[j];

                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[j] = prevDiag;
                    }
                    else
                    {
                        dp[j] = Math.Min(
                            dp[j] + s1[i - 1],
                            dp[j - 1] + s2[j - 1]
                        );
                    }

                    prevDiag = temp;
                }
            }

            return dp[n];
        }

        public int MinimumDeleteSum1(string s1, string s2)
        {
            int s1Len = s1.Length;
            int s2Len = s2.Length;

            int[][] dp = new int[s1Len + 1][];
            for (int i = 0; i <= s1Len; i++)
            {
                dp[i] = new int[s2Len + 1];
                if (i == 0)
                {
                    for (int j = 1; j <= s2Len; j++)
                    {
                        dp[i][j] = dp[i][j - 1] + s2[j - 1];
                    }
                }
                else
                {
                    dp[i][0] = dp[i - 1][0] + s1[i - 1];
                }
            }

            for (int i = 1; i <= s1Len; i++)
            {
                for (int j = 1; j <= s2Len; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i][j] = dp[i - 1][j - 1];
                    }
                    else
                    {
                        dp[i][j] = Math.Min(dp[i - 1][j] + s1[i - 1], dp[i][j - 1] + s2[j - 1]);
                    }
                }
            }

            return dp[s1Len][s2Len];
        }
        #endregion

        #region 12 --> 1266. Minimum Time Visiting All Points
        public int MinTimeToVisitAllPoints(int[][] points)
        {
            int time = 0;
            int startX = points[0][0];
            int startY = points[0][1];

            for (int i = 1; i < points.Length; i++)
            {
                int diffX = Math.Abs(points[i][0] - startX);
                int diffY = Math.Abs(points[i][1] - startY);


                time += Math.Max(diffX, diffY);
                startX = points[i][0];
                startY = points[i][1];
            }

            return time;
        }
        #endregion

        #region 13 --> 3453. Separate Squares I
        public double SeparateSquaresI(int[][] squares)
        {
            double result = 0.0;

            double totalArea = 0.0;
            double low = int.MaxValue, high = 0.0;
            foreach (int[] square in squares)
            {
                low = Math.Min(square[1], low);
                high = Math.Max(square[1] + square[2], high);

                totalArea += ((double)square[2] * (double)square[2]);
            }

            double halfArea = (double)(totalArea / 2);

            while (low < high)
            {
                double mid = (low + high) / 2;

                double currentArea = 0.0;
                foreach (int[] square in squares)
                {

                    if (square[1] >= mid) continue;
                    int y1 = square[1];
                    double y2 = y1 + square[2];
                    if (y2 > mid)
                    {
                        y2 = mid;
                    }

                    currentArea += ((double)square[2] * (double)(y2 - y1));

                    if (currentArea >= halfArea) break;
                }

                if (currentArea < halfArea)
                {
                    low = Math.Round(mid + 0.000001, 5);
                }
                else
                {
                    high = Math.Round(mid - 0.000001, 5);
                }
            }

            return low;
        }
        #endregion

        #region 14 --> 3454. Separate Squares II
        public double SeparateSquares(int[][] squares)
        {
            List<(int x, int y, int x1, int id)> lst = new List<(int x, int y, int x1, int id)>();

            for (int i = 0; i < squares.Length; i++)
            {
                int x = squares[i][0];
                int y = squares[i][1];
                int y1 = y + squares[i][2];
                int x1 = x + squares[i][2];
                int id = i + 1;

                lst.Add((x, y, x1, id));
                lst.Add((x, y1, x1, -id));
            }

            lst.Sort((a, b) =>
            {
                if (a.y == b.y)
                {
                    //if(a.x == b.x) return a.x1 - b.x1;
                    return a.x - b.x;
                }

                return a.y - b.y;
            });

            int startX = lst[0].x;
            int startY = lst[0].y;
            int startID = lst[0].id;
            int startX1 = lst[0].x1;
            long totalArea = 0;

            List<(int x1, int x2, int y1, int y2)> sq = new List<(int x1, int x2, int y1, int y2)>();

            for (int i = 1; i < lst.Count; i++)
            {
                if (lst[i].id > 0)
                {
                    sq.Add((startX, startX1, startY, lst[i].y));
                    long len = startX1 - startX;
                    long wid = lst[i].y - startY;

                    totalArea += (len * wid);
                    //diff between x axis
                    int diff = lst[i].x - startX1;
                    startX = Math.Min(startX, lst[i].x);
                    startX1 = Math.Max(startX1, lst[i].x1);
                    startY = lst[i].y;
                    startID = lst[i].id;
                }
                else
                {
                    //
                }
            }


            return 0.0;
        }
        #endregion

        #region 15 --> 2943. Maximize Area of Square Hole in Grid
        public int MaximizeSquareHoleArea(int n, int m, int[] hBars, int[] vBars)
        {

            int min = Math.Min(GetConsecutiveSequence(hBars), GetConsecutiveSequence(vBars)) + 1;

            return min * min;
        }

        public int GetConsecutiveSequence(int[] arr)
        {
            int res = 1;

            Array.Sort(arr);

            for (int i = 0; i < arr.Length;)
            {
                int j = i + 1;
                int currentCount = 1;
                for (; j < arr.Length && arr[i] + (j - i) == arr[j]; j++)
                {
                    currentCount++;
                }
                res = Math.Max(res, currentCount);
                i = j;
            }

            return res;
        }
        #endregion

        #region 16 --> 2975. Maximum Square Area by Removing Fences From a Field
        public int MaximizeSquareArea(int m, int n, int[] hFences, int[] vFences)
        {
            long result = 0;
            if (m == n)
            {
                result = (long)(m - 1) * (long)(n - 1);
            }
            else
            {
                List<int> hDistance = getDistance(hFences, m);
                List<int> vDistance = getDistance(vFences, n);

                result = getMaxCommonElement(hDistance, vDistance);
                if (result > 0)
                {
                    result = result * result;
                }
            }
            return (int)(result % MOD);
        }

        private int getMaxCommonElement(List<int> hDistance, List<int> vDistance)
        {
            int hIndex = hDistance.Count - 1;
            int vIndex = vDistance.Count - 1;

            while (hIndex >= 0 && vIndex >= 0)
            {
                if (hDistance[hIndex] == vDistance[vIndex]) return hDistance[hIndex];

                if (hDistance[hIndex] > vDistance[vIndex])
                {
                    hIndex--;
                }
                else
                {
                    vIndex--;
                }

            }
            return -1;
        }

        private List<int> getDistance(int[] fences, int m)
        {
            List<int> lines = new List<int>();
            lines.Add(1);
            Array.Sort(fences);
            lines.AddRange(fences);
            lines.Add(m);
            SortedSet<int> distance = new SortedSet<int>();
            for (int i = 0; i < lines.Count - 1; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    distance.Add(lines[j] - lines[i]);
                }
            }

            return distance.ToList();
        }
        #endregion


        #region 18 --> 1895. Largest Magic Square
        //int[][] rowSum;
        //int[][] colSum;
        //public int LargestMagicSquare(int[][] grid)
        //{
        //    int m = grid.Length;
        //    int n = grid[0].Length;
        //    rowSum = new int[m][];
        //    colSum = new int[m][];

        //    for (int i = 0; i < m; i++)
        //    {
        //        rowSum[i] = new int[n];
        //        colSum[i] = new int[n];
        //    }
        //    rowSum[0][0] = grid[0][0];
        //    colSum[0][0] = grid[0][0];
        //    for (int i = 1; i < m; i++)
        //    {
        //        colSum[i][0] = colSum[i - 1][0] + grid[i][0];
        //    }

        //    for (int i = 1; i < n; i++)
        //    {
        //        rowSum[0][i] = rowSum[0][i - 1] + grid[0][i];
        //    }

        //    for (int i = 1; i < m; i++)
        //    {
        //        for (int j = 1; j < n; j++)
        //        {
        //            rowSum[i][j] = rowSum[i][j - 1] + grid[i][j];
        //            colSum[i][j] = colSum[i - 1][j] + grid[i][j];
        //        }
        //    }

        //    int maxPossible = Math.Min(m, n);

        //    while (maxPossible > 1)
        //    {
        //        for (int i = 0; i + maxPossible < m; i++)
        //        {
        //            for (int j = 0; j + maxPossible < n; j++)
        //            {
        //                if (canMakeMagicSquare(grid, maxPossible, i, j))
        //                {
        //                    return maxPossible;
        //                }
        //            }
        //        }

        //        maxPossible--;
        //    }

        //    return 1;
        //}

        //private bool canMakeMagicSquare(int[][] grid, int row, int col, int rowEnd, int colEnd)
        //{
        //    if (row + maxPossible - 1 > grid.Length || col + maxPossible > grid[0].Length) return false;
        //    int currRowSum = grid[row][col];
        //    int currColSum = grid[row][col];
        //    int i = 1;
        //    for (; i < maxPossible; i++)
        //    {
        //        currRowSum += grid[row][col + i];
        //        currColSum += grid[row + i][col];
        //    }
        //    if (currColSum != currRowSum)
        //    {
        //        return false;
        //    }

        //    int reqSum = currColSum;
        //    i = 1;
        //    for (; i < maxPossible; i++)
        //    {
        //        currRowSum =
        //    }


        //    return true;
        //}
        #endregion

        #region 22 --> 3507. Minimum Pair Removal to Sort Array I
        public int MinimumPairRemoval2(int[] nums)
        {
            return 0;
        }
        public int MinimumPairRemoval1(int[] nums)
        {
            int result = 0;
            var list = nums.ToList();

            while (list.Count > 1)
            {
                bool asc = true;
                int minSum = int.MaxValue;
                int indexToRemove = -1;

                for (int i = 0; i < list.Count - 1; i++)
                {
                    int currSum = list[i] + list[i + 1];
                    if (list[i] > list[i + 1])
                    {
                        asc = false;
                    }
                    if (currSum < minSum)
                    {
                        indexToRemove = i;
                        minSum = currSum;
                    }
                }
                if (asc) break;
                result++;
                list[indexToRemove] = minSum;
                list.RemoveAt(indexToRemove + 1);
            }

            return result;
        }
        #endregion

        #region 23 --> 3510. Minimum Pair Removal to Sort Array II
        public int MinimumPairRemoval(int[] nums)
        {
            int result = 0;
            bool asc = true;

            ListNode listNode = new ListNode(long.MinValue);

            listNode.next = new ListNode(nums[0]);
            ListNode temp = listNode.next;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < temp.val)
                {
                    asc = false;
                }
                temp.next = new ListNode(nums[i]);
                temp = temp.next;
            }


            while (!asc)
            {
                asc = true;
                temp = listNode.next;
                ListNode nodeToUpdate = null;
                long minSum = long.MaxValue;
                while (temp != null && temp.next != null)
                {
                    if (temp.val > temp.next.val)
                    {
                        asc = false;
                    }

                    long currSum = temp.val + temp.next.val;
                    if (currSum < minSum)
                    {
                        minSum = currSum;
                        nodeToUpdate = temp;
                    }
                    temp = temp.next;
                }

                if (!asc && nodeToUpdate != null)
                {
                    result++;
                    nodeToUpdate.val = minSum;
                    ListNode nextNodeToLink = nodeToUpdate.next.next;


                    nodeToUpdate.next = null;
                    nodeToUpdate.next = nextNodeToLink;
                }
            }


            return result;
        }
        public int MinimumPairRemoval3(int[] nums)
        {
            DoubleListNode head = new DoubleListNode(int.MinValue);

            head.next = new DoubleListNode(nums[0]);
            DoubleListNode temp = head.next;
            temp.prev = head;
            int minSum = int.MaxValue;
            bool asc = true;
            DoubleListNode nodeToRemove = null;
            for (int i = 1; i < nums.Length; i++)
            {
                DoubleListNode doubleListNode = new DoubleListNode(nums[i]);

                temp.next = doubleListNode;
                doubleListNode.prev = temp;

                if (temp.val > temp.next.val)
                {
                    asc = false;
                }
                int currMinsum = temp.val + temp.next.val;
                if (currMinsum < minSum)
                {
                    nodeToRemove = temp;
                    minSum = currMinsum;
                }
                temp = temp.next;
            }
            int result = 0;
            while (!asc && nodeToRemove != null)
            {
                asc = true;
                result++;
                DoubleListNode prev = nodeToRemove.prev;
                DoubleListNode next = nodeToRemove.next.next;
                DoubleListNode doubleListNode = new DoubleListNode(minSum);

                prev.next = doubleListNode;
                doubleListNode.prev = prev;
                doubleListNode.next = next;
                if (next != null)
                {
                    next.prev = doubleListNode;
                }

                temp = head.next;

                minSum = int.MaxValue;
                nodeToRemove = null;
                while (temp != null && temp.next != null)
                {
                    if (temp.val > temp.next.val)
                    {
                        asc = false;
                    }

                    int currMinsum = temp.val + temp.next.val;
                    if (currMinsum < minSum)
                    {
                        nodeToRemove = temp;
                        minSum = currMinsum;
                    }
                    temp = temp.next;
                }
            }

            return result;
        }
        #endregion


        #region 24 --> 1877. Minimize Maximum Pair Sum in Array
        public int MinPairSum(int[] nums)
        {
            int res = int.MinValue;

            Array.Sort(nums);

            int left = -1, right = nums.Length;

            while (++left < --right)
            {
                res = Math.Max(res, nums[left] + nums[right]);
            }

            return res;
        }
        #endregion

        #region 25 --> 1984. Minimum Difference Between Highest and Lowest of K Scores
        public int MinimumDifference(int[] nums, int k)
        {
            Array.Sort(nums);

            int result = int.MaxValue;
            k--;
            for (int i = k; i < nums.Length; i++)
            {
                int currDiff = nums[i] - nums[i - k];
                result = Math.Min(result, currDiff);
            }


            return result;
        }
        #endregion

        #region 26 --> 1200. Minimum Absolute Difference
        public IList<IList<int>> MinimumAbsDifference(int[] arr)
        {
            Array.Sort(arr);
            IList<IList<int>> result = new List<IList<int>>();

            int minDiff = int.MaxValue;

            for (int i = 1; i < arr.Length; i++)
            {
                int currDiff = Math.Abs(arr[i] - arr[i - 1]);
                if (currDiff > minDiff) continue;
                if (currDiff < minDiff)
                {
                    result.Clear();
                }
                result.Add(new List<int>() { arr[i - 1], arr[i] });
            }

            return result;

        }
        #endregion

        #region 2976. Minimum Cost to Convert String I
        long[][] costSheet;
        public long MinimumCost(string source, string target, char[] original, char[] changed, int[] cost)
        {
            long[] costs = new long[cost.Length];

            costSheet = new long[26][];
            for (int i = 0; i < 26; i++)
            {
                costSheet[i] = new long[26];
            }

            for (int i = 0; i < original.Length; i++)
            {
                int origindalIndex = original[i] - 'a';
                int changedIndex = changed[i] - 'a';

                costSheet[origindalIndex][changedIndex] = cost[i];

            }

            minimizeThCostSheet();

            for (int i = 0; i < source.Length; i++)
            {
                //costs[i] = getCost(source[i], target[i], original, changed, cost, 0);
                if (source[i] == target[i]) continue;

                int sourceIndex = source[i] - 'a';
                int targetIndex = target[i] - 'a';

                if (costSheet[sourceIndex][targetIndex] == 0) return -1;

                costs[i] = costSheet[sourceIndex][targetIndex];
            }

            return costs.Sum();
        }

        private void minimizeThCostSheet()
        {
            PriorityQueue < (int x, int y, long c), long> priorityQueue = new PriorityQueue<(int, int, long), long>();
            for (int i = 0; i < 26; i++)
            {

                for (int j = 0; j < 26; j++)
                {
                    if (costSheet[i][j] > 0)
                    {
                        priorityQueue.Enqueue((i, j, costSheet[i][j]), costSheet[i][j]);
                    }
                }

            }

            while (priorityQueue.Count > 0)
            {
                var dq = priorityQueue.Dequeue();

                if (costSheet[dq.x][dq.y] < dq.c) continue;

                for (int i = 0; i < 26; i++)
                {
                    if (i == dq.y || i == dq.x) continue;
                    if (costSheet[dq.y][i] > 0)
                    {
                        long currCost = dq.c;

                        long newCost = currCost + costSheet[dq.y][i];

                        if (costSheet[dq.x][i] == 0 || costSheet[dq.x][i] > newCost)
                        {
                            priorityQueue.Enqueue((dq.x, i, newCost), newCost);
                            costSheet[dq.x][i] = newCost;
                        }

                    }
                }
            }
        }

        private void dfsCost(int i, int j)
        {

        }

        private long getCost(char source, char target, char[] original, char[] changed, int[] cost, int currCost)
        {
            if (source == target) return currCost;
            return 0;
        }

        #endregion
        #region playground
        public static NakedGroup GroupElements(List<int[]> arrays)
        {
            // Example: n arrays with unique elements per array


            // Try to find groups of size X (from 2 up to n-1)
            for (int x = 2; x < arrays.Count; x++)
            {
                var result = FindNakedGroup(arrays, x);
                if (result != null)
                {
                    return result;
                    //Console.WriteLine($"Found a group of {x} arrays sharing elements: {string.Join(", ", result.Elements)}");
                    //Console.WriteLine($"Array Indices in group: {string.Join(", ", result.Indices)}");
                    //break;
                }
            }
            return null;
        }
        public static NakedGroup FindNakedGroup(List<int[]> source, int groupSize)
        {
            var indices = Enumerable.Range(0, source.Count).ToList();

            // Check all combinations of 'groupSize' arrays
            foreach (var combo in GetCombinations(indices, groupSize))
            {
                // The key logic: Union all elements in this specific combination
                var union = combo.SelectMany(idx => source[idx]).Distinct().ToArray();

                // If Count of unique elements == Count of arrays, it's a naked group
                if (union.Length == groupSize)
                {
                    return new NakedGroup { Indices = combo.ToArray(), Elements = union };
                }
            }
            return null;
        }


        // Helper to generate combinations
        static IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return list.SelectMany((t, i) =>
                GetCombinations(list.Skip(i + 1), length - 1).Select(c => (new T[] { t }).Concat(c)));
        }

        public class NakedGroup
        {
            public int[] Indices { get; set; }
            public int[] Elements { get; set; }
        }
        #endregion
    }
}
