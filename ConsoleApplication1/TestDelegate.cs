using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestDelegate {
    public delegate void dlg1(string _str);
    public static void method1(string _str) {
        Console.WriteLine("--- method1, str:{0}", _str);
    }

    /// <summary>
    /// test delegate
    /// </summary>
    public static void test1() {
        dlg1 d1 = TestDelegate.method1;
        d1("hello world");

        //dlg has return
        Func<int, string> f1 = (int _num) => {
            Console.WriteLine("--- f1, num:{0}", _num);
            return "aaa";
        };
        Console.WriteLine("--- f1, str:{0}", f1(123));

        //dlg no return
        Action<int> a1 = (int _num) => {
            var a = _num + 100;
            Console.WriteLine("--- a1, num:{0}", _num);
        };
        a1(456);
    }

    public static event dlg1 dlgHandler;
    /// <summary>
    /// test event
    /// just like Observer pattern
    /// </summary>
    public static void test2() {
        dlg1 d1 = (string _str) => { Console.WriteLine("--- d1, str:{0}", _str); };
        dlg1 d2 = (string _str) => { Console.WriteLine("--- d2, str:{0}", _str); };
        dlg1 d3 = (string _str) => { Console.WriteLine("--- d3, str:{0}", _str); };

        dlgHandler += d1;
        dlgHandler += d2;
        dlgHandler += d3;
        dlgHandler("hello");

        dlgHandler -= d1;
        dlgHandler("world");

    }
}
