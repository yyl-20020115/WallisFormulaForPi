﻿using System.Diagnostics;
using System.Numerics;

namespace WallisFormulaForPi;

public class Program
{
    //计算圆周率的沃里斯乘积算法
    //分子和分母分别计算
    //最后彼此相除
    //可以得到极高精度的计算结果
    //Pi/2 = Sum(n!/(2n+1)!!,n>=0)
    //Pi/2 = 0!/1! + 1!/3! + 2!/5!+...
    static BigInteger WallisProductForPi(BigInteger n)
    {
        var upper = n;
        var lower = (n << 1) + 1;
        upper += lower;
        for (--n; n >= 1; --n)
        {
            upper *= n;
            upper += (lower *= ((n << 1) + 1));
        }

        upper *= BigInteger.Pow(10, (int)Math.Ceiling(BigInteger.Log10(lower)));

        return (upper << 1) / lower;
    }
    static BigInteger Calc_E(BigInteger n)
    {
        var upper = BigInteger.One;
        var lower = n;
        upper += lower;
        for (--n; n >= 1; --n)
        {
            upper += (lower *= n);
        }

        upper *= BigInteger.Pow(10, (int)Math.Ceiling(BigInteger.Log10(lower)));

        return upper / lower;
    }

    static double Calc_Golden_Section(long n)
    {
        var s = 0.0;
        for (long i = 0; i < n; i++)
        {
            s = 1.0 + 1.0 / s;
        }
        return s - 1.0;
    }
    static double Calc_Sqrt2(long n)
    {
        var s = 0.0;
        for (long i = 0; i < n; i++)
        {
            s = 1.0 / (2.0 + s);
        }
        return 1.0 + s;
    }
    //单阶乘
    static long Factorial1(long n) => n >= 1 ? n * Factorial1(n - 1) : 1;
    //双阶乘
    static long Factorial2(long n) => n >= 1 ? n * Factorial2(n - 2) : 1;
    //沃里斯乘积形式求圆周率（的一半）
    //Pi/2 = Sum(n!/(2n+1)!!,n>=0)
    //Pi/2 = 0!/1! + 1!/3! + 2!/5!+...
    //此函数因为decimal精度不够，所以结果在第15次迭代之后出错
    //若要获得更正确的结果，请使用Python版本的WFP.py进行高次迭代
    static decimal WallisProductForPiDiv2(int max)
    {
        decimal s = 0;
        for (int n = 0; n <= max; n++)
        {
            decimal f1 = Factorial1(n);
            decimal f2 = Factorial2(2 * n + 1);
            s += f1 / f2;
        }
        return s;
    }
    /// <summary>
    //0       Pi / 2 = 1.5707963267948966, s=1
    //1       Pi / 2 = 1.5707963267948966, s=1.3333333333333333333333333333
    //2       Pi / 2 = 1.5707963267948966, s=1.4666666666666666666666666666
    //3       Pi / 2 = 1.5707963267948966, s=1.5238095238095238095238095237
    //4       Pi / 2 = 1.5707963267948966, s=1.5492063492063492063492063491
    //5       Pi / 2 = 1.5707963267948966, s=1.5607503607503607503607503606
    //6       Pi / 2 = 1.5707963267948966, s=1.5660783660783660783660783659
    //7       Pi / 2 = 1.5707963267948966, s=1.5685647685647685647685647684
    //8       Pi / 2 = 1.5707963267948966, s=1.5697348403230756171932642519
    //9       Pi / 2 = 1.5707963267948966, s=1.5702890848401684314997008494
    //10      Pi / 2 = 1.5707963267948966, s=1.5705530108006888192646706577
    //11      Pi / 2 = 1.5707963267948966, s=1.5706792362600681351522649138
    //12      Pi / 2 = 1.5707963267948966, s=1.5707398244805702067783101568
    //13      Pi / 2 = 1.5707963267948966, s=1.5707689965867378708945541627
    //14      Pi / 2 = 1.5707963267948966, s=1.5707830796724739846058443724
    //15      Pi / 2 = 1.5707963267948966, s=1.5707898940687979105951783448
    //16      Pi / 2 = 1.5707963267948966, s=1.5707931980185307231960675436
    //17      Pi / 2 = 1.5707963267948966, s=1.5720537562771873841109201530
    //18      Pi / 2 = 1.5707963267948966, s=1.5712541172833223799181741024
    //19      Pi / 2 = 1.5707963267948966, s=1.6621681818083471872333682946
    //20      Pi / 2 = 1.5707963267948966, s=-3.3927582543189338135211826460
    //21      Pi / 2 = 1.5707963267948966, s=-1.5032325578386644392795104774
    //22      Pi / 2 = 1.5707963267948966, s=-1.3637316045664853349204097715
    //23      Pi / 2 = 1.5707963267948966, s=1.4310016743495748554473553210
    //24      Pi / 2 = 1.5707963267948966, s=2.9792169001591796295424227251
    //25      Pi / 2 = 1.5707963267948966, s=48.560443173620516089682171666
    //26      Pi / 2 = 1.5707963267948966, s=48.368557499795514338475059471
    //27      Pi / 2 = 1.5707963267948966, s=47.601518302259020662711590070
    //28      Pi / 2 = 1.5707963267948966, s=44.029583981379705653595686502
    //29      Pi / 2 = 1.5707963267948966, s=42.917767093641865804038915153
    //30      Pi / 2 = 1.5707963267948966, s=77.236176026210844772999552441    
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        Console.WriteLine(Calc_Sqrt2(100));
        Console.WriteLine(Calc_Golden_Section(100));
        BigInteger n = 10000;

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var pi = WallisProductForPi(n);
        stopwatch.Stop();

        Console.WriteLine($"pi={pi}");
        //Console.WriteLine(pi);
        Console.WriteLine($"Iterates = {n}, Digits = {(int)Math.Ceiling(BigInteger.Log10(pi))}, Duration = {stopwatch.Elapsed}");

        stopwatch.Start();
        var e = Calc_E(n);
        stopwatch.Stop();
        Console.WriteLine($"e={e}");
        Console.WriteLine($"Iterates = {n}, Digits = {(int)Math.Ceiling(BigInteger.Log10(e))}, Duration = {stopwatch.Elapsed}");

        //double MP2 = Math.PI / 2.0;
        //for (int n = 0; n <= 30; n++)
        //{
        //    Console.WriteLine($"{n}\tPi / 2 = {MP2}, s={WallisProductForPiDiv2(n)}");
        //}
    }
}
