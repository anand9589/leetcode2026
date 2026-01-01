namespace Leetcode
{
    public class January
    {
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
                if(curr == 10)
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
    }
}
