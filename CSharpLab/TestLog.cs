using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TestLog {
    delegate int myDelegate(int _arg1, string _arg2);

    public static int test1(int _arg1, string _arg2) {
        Console.WriteLine("test3, _arg1:{0}, _arg2:{1}", _arg1, _arg2);
        return 234;

    }

    public static void test2() //test delegate
    {
        myDelegate md = new myDelegate(TestLog.test1);
        Console.WriteLine("exe delegate, ret:{0}", md(123, "hello"));
    }
}
