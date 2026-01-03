import unittest
from day1 import Solution

class TestPlusOne(unittest.TestCase1):
    def setUp(self):
        self.sol = Solution()
        
    def test1(self):
        digits = [1,2,3]
        self.sol.plusOne(digits)


if __name__ == '__main__':
    unittest.main()
        


