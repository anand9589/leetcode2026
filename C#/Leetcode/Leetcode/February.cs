using Common;
using System.Text;

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
        //public int[] ConstructTransformedArray(int[] nums)
        //{
        //    int[] result = new int[nums.Length];

        //    for (int i = 0; i < nums.Length; i++)
        //    {
        //        if (nums[i] == 0) continue;

        //        if (nums[i] < 0)
        //        {
        //            int k = Math.Abs(Math.Abs(nums[i]) - i);

        //            if ()

        //                k %= nums.Length;

        //            result[i] =

        //            if (k > 0)
        //            {

        //            }
        //            if (Math.Abs(nums[i]) >= nums.Length)
        //            {
        //                result[i] = nums[i];
        //            }
        //        }
        //        else
        //        {
        //            int k = nums[i] % nums.Length;
        //            result[i] = nums[i + k];
        //        }
        //    }

        //    return result;

        //}
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

        #region 15 --> 67. Add Binary
        public string AddBinary(string a, string b)
        {
            List<char> list = new List<char>();
            int carryOn = 0;

            int alen = a.Length - 1;
            int blen = b.Length - 1;

            while (alen >= 0 || blen >= 0)
            {
                int num1 = alen >= 0 ? a[alen] - '0' : 0;
                int num2 = blen >= 0 ? b[blen] - '0' : 0;

                int res = num1 + num2 + carryOn;

                if (res < 2)
                {
                    list.Add((char)(res + (int)'0'));
                    carryOn = 0;
                }
                else
                {
                    if (res == 2)
                    {
                        list.Add('0');
                    }
                    else
                    {
                        list.Add('1');
                    }
                    carryOn = 1;
                }

                alen--;
                blen--;
            }
            if (carryOn == 1)
            {
                list.Add('1');
            }
            return new string(list.ToArray().Reverse().ToArray());
        }
        #endregion


        #region 21 --> 762. Prime Number of Set Bits in Binary Representation
        public int CountPrimeSetBits(int left, int right)
        {
            int result = 0;

            HashSet<int> primes = new HashSet<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31 };

            for (int i = left; i <= right; i++)
            {
                int bits = countBits(i);
                if (primes.Contains(bits)) { result++; }
            }

            return result;
        }

        private int countBits(int n)
        {
            int counter = 0;
            while (n > 0)
            {
                int bit = n & 1;
                counter += bit;
                n >>= 1;
            }
            return counter;
        }



        #endregion



        #region 23 --> 1461. Check If a String Contains All Binary Codes of Size K

        HashSet<string> codes1461;
        public bool HasAllCodes(string s, int k)
        {
            if (k > s.Length) return false;

            HashSet<int> seen = new HashSet<int>();
            int num = 0;

            for (int i = 0; i < s.Length; i++)
            {
                num = ((num << 1) & ((1 << k) - 1)) | (s[i] - '0');

                if (i >= k - 1)
                    seen.Add(num);
            }

            return seen.Count == (1 << k);
        }
        public bool HasAllCodes2(string s, int k)
        {
            int m = 1 << k;
            if (s.Length - k + 1 < m) return false;
            HashSet<string> codes = new HashSet<string>();

            for (int i = 0; i < s.Length - k + 1; i++)
            {
                codes.Add(s.Substring(i, k));
            }


            


            return codes.Count==m;
        }
        public bool HasAllCodes1(string s, int k)
        {
            if (k > s.Length) return false;
            codes1461 = new HashSet<string>();
            string str = s.Substring(0, k);

            codes1461.Add(str);

            for (int i = 1; i < s.Length - k + 1; i++)
            {
                codes1461.Add(s.Substring(i, k));
            }

            StringBuilder b = new StringBuilder(new string('0', k));



            return HasAllCodes_Helper(b, 0);
        }

        private bool HasAllCodes_Helper(StringBuilder b, int index)
        {
            if (!codes1461.Contains(b.ToString())) return false;

            for (int i = index; i < b.Length; i++)
            {
                char c = b[i];
                if (c == '0')
                {
                    b[i] = '1';

                }
                else
                {
                    b[i] = '0';
                }
                if (!HasAllCodes_Helper(b, index + 1)) return false;
                b[i] = c;
            }
            return true;
        }
        #endregion

        #region x --> 729. My Calendar I
        public class MyCalendar
        {
            CalendarSegment calendarSegment;

            public MyCalendar()
            {
                calendarSegment = new CalendarSegment();
            }

            public bool Book(int startTime, int endTime)
            {
                return calendarSegment.Book(startTime, endTime - 1);
            }


        }

        public class CalendarSegment
        {
            public CalendarSegment Left { get; set; }
            public CalendarSegment Right { get; set; }
            public int Start { get; set; }
            public int End { get; set; }

            private int tempStart = -1;
            private int tempEnd = -1;
            private int mid;
            public CalendarSegment()
            {
                Start = 0;
                End = 1000000000;
                mid = (Start + End) / 2;
            }
            public CalendarSegment(int start, int end)
            {
                Start = start;
                End = end;
                mid = (Start + End) / 2;
            }

            public CalendarSegment(int start, int end, int tempS, int tempE)
            {
                Start = start;
                End = end;
                this.tempStart = tempS;
                this.tempEnd = tempE;
                mid = (Start + End) / 2;
            }

            public bool Book(int startTime, int endTime)
            {
                if (startTime > End || endTime < Start) return true;

                if (Left == null && Right == null && tempStart == -1 && tempEnd == -1)
                {
                    tempStart = startTime;
                    tempEnd = endTime;
                    return true;
                }

                if (tempStart != -1)
                {
                    if ((startTime >= tempStart && startTime <= tempEnd) || (endTime >= tempStart && endTime <= tempEnd)) { return false; }

                    if (endTime + 1 == tempStart)
                    {
                        tempStart = startTime;
                        return true;
                    }

                    if (tempEnd + 1 == startTime)
                    {
                        tempEnd = endTime;
                        return true;
                    }

                    allot();
                    tempStart = -1;
                    tempEnd = -1;
                }

                if (endTime <= mid)
                {
                    if (Left == null)
                    {
                        Left = new CalendarSegment(Start, mid, startTime, endTime);
                    }
                    return Left.Book(startTime, endTime);
                }
                else if (startTime > mid)
                {
                    if (Right == null)
                    {
                        Right = new CalendarSegment(mid + 1, End, startTime, endTime);
                        return true;
                    }
                    return Right.Book(startTime, endTime);
                }


                return Left.Book(startTime, endTime) && Right.Book(startTime, endTime);

            }

            public void allot()
            {
                //Left = new CalendarSegment(Start, mid);
                //Right = new CalendarSegment(mid+1, End);

                if (tempEnd <= mid)
                {
                    Left = new CalendarSegment(Start, mid, tempStart, tempEnd);
                    //Right = new CalendarSegment(mid+1, End);
                }
                else if (tempStart > mid)
                {
                    Right = new CalendarSegment(mid + 1, End, tempStart, tempEnd);
                    //Left = new CalendarSegment(Start, mid);
                }
                else
                {
                    Left = new CalendarSegment(Start, mid, tempStart, mid);
                    Right = new CalendarSegment(mid + 1, End, mid + 1, tempEnd);
                }
            }
        }
        /*
        public class MyCalendar1
        {
            //CalendarSegment segment;
            SortedList<int, int> lst;
            public MyCalendar1()
            {
                lst = new SortedList<int, int>();
                //segment = new CalendarSegment(0, 1000000000);
            }

            public bool Book(int startTime, int endTime)
            {
                if(lst.Count == 0)
                {
                    lst.Add(startTime, endTime-1);
                    return true;
                }


                if (lst.ContainsKey(startTime)) { return false; }
                
                int low = 0;
                int high = lst.Count - 1;
                while (low < high) { 
                    int mid = (low + high) / 2;
                    int key = lst.GetKeyAtIndex(mid);
                    if (key > endTime)
                    {
                        high = mid;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }

                int startTimeatLow = lst.GetKeyAtIndex(low);
                int endTimeAtLow = lst.GetValueAtIndex(low);
                

                if((startTime >= startTimeatLow && startTime<= endTimeAtLow)||(startTime)



                return false;
            }

            //class CalendarSegment
            //{
            //    public int Start;
            //    public int End;
            //    public bool Booked { get; set; }

            //    public CalendarSegment LeftCalendar { get; set; }
            //    public CalendarSegment RightCalendar { get; set; }

            //    int tempStart = -1;
            //    int tempEnd = -1;



            //    public CalendarSegment(int start, int end)
            //    {
            //        this.Start = start;
            //        this.End = end;
            //    }

            //    public CalendarSegment(int start, int end, int tempS, int tempE)
            //    {
            //        this.Start = start;
            //        this.End = end;
            //        this.tempStart = tempS;
            //        this.tempEnd = tempE;
            //    }

            //    public bool Book(int startTime, int endTime)
            //    {
            //        if (LeftCalendar == null && RightCalendar == null && tempStart == -1 && tempEnd == -1)
            //        {
            //            tempStart = startTime;
            //            tempEnd = endTime;
            //            return true;
            //        }

            //        if (tempStart != -1 && tempEnd != -1)
            //        {

            //            if ((startTime >= tempStart && startTime <= tempEnd) || (endTime >= tempStart && endTime <= tempEnd)) return false;

            //            BookTemp(tempStart, tempEnd);
            //            tempStart = -1;
            //            tempEnd = -1;
            //        }

            //        int mid = (Start + End) / 2;

            //        if (endTime <= mid)
            //        {
            //            if (LeftCalendar == null)
            //            {
            //                LeftCalendar = new CalendarSegment(Start, mid, startTime, endTime);
            //                return true;
            //            }
            //            else
            //            {
            //                return LeftCalendar.Book(startTime, endTime);
            //            }
            //        }
            //        else if (startTime >= mid)
            //        {
            //            if(RightCalendar == null)
            //            {
            //                RightCalendar = new CalendarSegment(mid + 1, End, startTime, endTime);
            //                return true;
            //            }
            //            else
            //            {
            //                return RightCalendar.Book(startTime, endTime);
            //            }
            //        }
            //        else
            //        {
            //            if(LeftCalendar == null)
            //            {
            //                LeftCalendar = 
            //            }
            //            else
            //            {
            //                if(!LeftCalendar.Book()) 
            //            }
            //        }

            //        return false;

            //        //if (endTime < Start || startTime > End) return false;

            //        //if (Start == End)
            //        //{                        
            //        //    if (Booked) return false;
            //        //    return true;
            //        //}

            //        //int mid = (Start + End) / 2;

            //        //if (startTime <= mid && endTime <= mid)
            //        //{
            //        //    if (LeftCalendar == null)
            //        //    {
            //        //        LeftCalendar = new CalendarSegment(Start, mid);
            //        //    }
            //        //    return LeftCalendar.Book(startTime, endTime);
            //        //}
            //        //else if (startTime > mid && endTime > mid)
            //        //{
            //        //    if (RightCalendar == null)
            //        //    {
            //        //        RightCalendar = new CalendarSegment(mid + 1, End);
            //        //    }
            //        //    return RightCalendar.Book(startTime, endTime);
            //        //}
            //        //if (LeftCalendar == null)
            //        //{
            //        //    LeftCalendar = new CalendarSegment(Start, mid);
            //        //}

            //        //if (RightCalendar == null)
            //        //{
            //        //    RightCalendar = new CalendarSegment(mid + 1, End);
            //        //}

            //        //return LeftCalendar.Book(startTime, mid) && RightCalendar.Book(mid, endTime);

            //    }

            //    private void BookTemp(int tempStart, int tempEnd)
            //    {
            //        int mid = (Start + End) / 2;

            //        if (tempEnd <= mid)
            //        {
            //            LeftCalendar = new CalendarSegment(Start, mid, tempStart, tempEnd);
            //        }
            //        else if (tempStart > mid)
            //        {
            //            RightCalendar = new CalendarSegment(mid + 1, End, tempStart, tempEnd);
            //        }
            //        else
            //        {
            //            LeftCalendar = new CalendarSegment(Start, mid, tempStart, mid);
            //            RightCalendar = new CalendarSegment(mid + 1, End, mid + 1, tempEnd);
            //        }
            //    }
            //}
        }
        */
        #endregion
    }
}
