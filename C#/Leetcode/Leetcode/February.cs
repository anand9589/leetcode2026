using Common;

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

        #region 5 --> 3379. Transformed Array
        public int[] ConstructTransformedArray(int[] nums)
        {
            int[] result = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0) continue;

                if (nums[i] < 0)
                {
                    int k = Math.Abs(Math.Abs(nums[i]) - i);

                    if ()

                        k %= nums.Length;

                    result[i] =

                    if (k > 0)
                    {

                    }
                    if (Math.Abs(nums[i]) >= nums.Length)
                    {
                        result[i] = nums[i];
                    }
                }
                else
                {
                    int k = nums[i] % nums.Length;
                    result[i] = nums[i + k];
                }
            }

            return result;

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

                if (max > int.MaxValue)
                {
                    result = Math.Min(result, i);
                    continue;
                }

                int index = MinRemovalBinarySearch(nums, i + 1, (int)max);

                if (index > i)
                {
                    int curRem = nums.Length - (index - i);
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

        #region 8 --> 110. Balanced Binary Tree
        public bool IsBalanced(TreeNode root)
        {
            if (root == null) return true;

            return dfs(root) >= 0;
        }

        private int dfs(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            // Recursively calculate height of left subtree
            int leftHeight = dfs(root.left);

            // Recursively calculate height of right subtree
            int rightHeight = dfs(root.right);

            // Check if any subtree is unbalanced or height difference exceeds 1
            if (leftHeight == -1 || rightHeight == -1 || Math.Abs(leftHeight - rightHeight) > 1)
            {
                // Propagate unbalanced state up the tree
                return -1;
            }

            // Return height of current subtree (1 + maximum child height)
            return 1 + Math.Max(leftHeight, rightHeight);
        }
        #endregion


        #region 12 --> 3713. Longest Balanced Substring I
        public int LongestBalanced(string s)
        {
            int n = s.Length;
            int result = 0;

            for (int i = 0; i < n; i++)
            {
                int[] freq = new int[26];
                int distinct = 0;
                int maxFreq = 0;

                for (int j = i; j < n; j++)
                {
                    int index = s[j] - 'a';

                    if (freq[index] == 0)
                        distinct++;

                    freq[index]++;
                    maxFreq = Math.Max(maxFreq, freq[index]);

                    int length = j - i + 1;

                    if (length == distinct * maxFreq)
                        result = Math.Max(result, length);
                }
            }

            return result;
        }
        int[] freq3713;
        public int LongestBalanced1(string s)
        {
            int result = 1;


            for (int i = 0; i < s.Length; i++)
            {

                freq3713 = new int[26];
                for (int j = i; j < s.Length; j++)
                {
                    freq3713[s[j] - 'a']++;
                    if (isValidString3713())
                    {
                        result = Math.Max(result, j - i + 1);
                    }
                }
            }

            return result;
        }

        public bool isValidString3713()
        {
            int currCount = 0;
            for (int i = 0; i < 26; i++)
            {
                if (freq3713[i] == 0) continue;

                if (currCount == 0)
                {
                    currCount = freq3713[i];
                }
                else
                {
                    if (freq3713[i] != currCount) return false;
                }
            }
            return true;
        }
        #endregion
    }
}
