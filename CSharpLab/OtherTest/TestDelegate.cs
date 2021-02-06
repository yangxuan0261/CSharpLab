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

    public static void test1() {
        dlg1 d1 = TestDelegate.method1; // 静态 委托
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


    private void method2(string _str) {
        Console.WriteLine("--- method2, str:{0}", _str);
    }

    static void test2() {
        TestDelegate td = new TestDelegate();
        dlg1 ptrDlg = new dlg1(td.method2); // 实例 委托
        ptrDlg("wolegequ");
    }

    public static event dlg1 dlgHandler; // 事件 委托
    public static void test3() {
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

    static void test4() {
        if (dlgHandler == null) {
            Console.WriteLine("--- dlgHandler == null 111"); // 没有添加 委托时, dlgHandler == null
        }

        dlg1 d1 = (string _str) => { Console.WriteLine("--- d1, str:{0}", _str); };
        dlgHandler += d1;
        dlgHandler("hello");

        if (dlgHandler == null) {
            Console.WriteLine("--- dlgHandler == null 222"); 
        } else {
            Console.WriteLine("--- dlgHandler != null 222"); // 只要有添加 委托, dlgHandler != null 222
        }

        dlgHandler -= d1;
        if (dlgHandler == null) {
            Console.WriteLine("--- dlgHandler == null 333"); // 清空了 委托, dlgHandler == null 333
        } else {
            Console.WriteLine("--- dlgHandler != null 333");
        }
    }

    public static void main() {
        // test1();
        // test2();
        // test3();
        test4();
    }
}
