using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestArgs
{
    public static void method1(int _a, ref int _b)
    {
        _a = _a + 10;
        _b = _b + 10;
    }
    
    public static void test1()
    {
        int a = 1;
        int b = 2;
        method1(a, ref b);
        Console.WriteLine("-- test1, a:{0}, b:{1}", a, b); //1, 12
    }
}
