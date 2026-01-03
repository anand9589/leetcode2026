class Solution:
    def plusOne(self, digits: List[int]) -> List[int]:
        n = len(digits)
        result = []
        carry = 1 
        for i in range(n,-1,-1):
            curr = i + carry

            if curr == 10:
                result.append(0)
            else:
                result.append(curr)
                carry = 0


        return result.reverse()
    


        