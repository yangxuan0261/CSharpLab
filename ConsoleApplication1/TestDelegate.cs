using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestDelegate
{
    delegate void dlg1(string _str);
    public static void method1(string _str)
    {
        Console.WriteLine("--- method1, str:{0}", _str);
    }

    public static void test1()
    {
        dlg1 d1 = TestDelegate.method1;
        d1("hello world");

        //dlg has return
        Func<int, string> f1 = (int _num) => {
            Console.WriteLine("--- f1, num:{0}", _num);
            return "aaa";
        };
        Console.WriteLine("--- f1, str:{0}", f1(123));

        //dlg no return
        Action<int> a1 = (int _num) =>
        {
            var a = _num + 100;
            Console.WriteLine("--- a1, num:{0}", _num);
        };
        a1(456);
    }
}
