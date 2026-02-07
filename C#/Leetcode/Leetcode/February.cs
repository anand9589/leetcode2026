namespace Leetcode
{
    public class February
    {
        #region x -->
        #endregion

        #region 1 --> 3010. Divide an Array Into Subarrays With Minimum Cost I
        public int MinimumCost(int[] nums)
        {
            int min = nums[0];
            int min1, min2;

            if (nums[1] > nums[2])
            {
                min1 = nums[2];
                min2 = nums[1];
            }
            else
            {
                min1 = nums[1];
                min2 = nums[2];
            }

            for (int i = 3; i < nums.Length; i++)
            {
                if (nums[i] < min1)
                {
                    min2 = min1;
                    min1 = nums[i];
                }
                else if (nums[i] < min2)
                {
                    min2 = nums[i];
                }
            }

            return min + min1 + min2;
        }
        #endregion

        #region 6 --> 3634. Minimum Removals to Balance Array
        public int MinRemoval(int[] nums, int k)
        {
            if (nums.Length == 1) return 0;
            int result = int.MaxValue;
            Array.Sort(nums);

            for (int i = 0; i < nums.Length; i++)
            {
                if (i >= result) break;
                int min = nums[i];

                long max = (long)min * (long)k;

                if(max > int.MaxValue)
                {
                    result = Math.Min(result, i);
                    continue;
                }

                int index = MinRemovalBinarySearch(nums, i + 1, (int)max);

                if (index > i)
                {
                    int curRem =  nums.Length -( index - i);
                    result = Math.Min(result, curRem);
                }
            }
            return result;
        }

        public int MinRemovalBinarySearch(int[] nums, int low, int max)
        {
            int high = nums.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;

                if (nums[mid] <= max)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return low;
        }
        #endregion

        #region 7 --> 1653. Minimum Deletions to Make String Balanced
        public int MinimumDeletions(string s)
        {
            int b = 0, d = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'b')
                {
                    b++;
                }
                else if (b > 0)
                {
                    d++;
                    b--;
                }
            }

            return d;
        }
        public int MinimumDeletions1(string s)
        {
            int[] dp = new int[s.Length + 1];
            int b = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'b')
                {
                    b++;
                    dp[i + 1] = dp[i];
                }
                else
                {
                    dp[i + 1] = Math.Min(dp[i] + 1, b);
                }
            }

            return dp[s.Length];
        }
        #endregion
    }
}
