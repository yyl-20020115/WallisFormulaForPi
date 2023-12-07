import math

def factorial1(n):
    if n>=1:
        return n*factorial1(n-1)
    else:
        return 1
def factorial2(n):
    if n>=1:
        return n*factorial2(n-2)
    else:
        return 1
def wallis_pi_2(m):
    s = 0
    for n in range(0,m):
        f1 = factorial1(n)
        f2 = factorial2(2 * n + 1)
        s += f1 / f2
    return s

print(math.pi/2)
print(wallis_pi_2(100))

# python WFP.py
# 1.5707963267948966
# 1.5707963267948961
