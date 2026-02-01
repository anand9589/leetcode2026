namespace Leetcode
{
    internal class February
    {
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
    }
}
