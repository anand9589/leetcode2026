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
        int[][] nWaysGrid1411_1 = null;
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
    }
}
