
#define DEBUG_TEST1 //必须在文件头行才能使用 #define 
//#define DEBUG_TEST2 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// https://msdn.microsoft.com/zh-cn/library/4xssyw96(v=vs.90).aspx
/// </summary>
class TestCondictional
{
    public static void func1() {
#if DEBUG_TEST1
        for (int i = 0; i < 10; i++) {
            Console.WriteLine("--- func1 - {0}", i);
        }
#endif
    }

        // 使用 conditional attribute 替代 #if/#endif，可以减少不必要的函数调用及代码
        [Conditional("DEBUG_TEST2")]
    public static void func2() {
        for (int i = 0; i < 10; i++) {
            Console.WriteLine("--- func2 - {0}", i);
        }
    }

    class CA {

        //[Conditional("DEBUG_TEST3")]
        public virtual void func3() {
            Console.WriteLine("--- CA.func3");
        }

        [Conditional("DEBUG_TEST4")]
        public void Calc(string str) {
            Console.WriteLine("--- CA.Calc, str:{0}", str);
        }

        //[Conditional("DEBUG_TEST5")] //编译错误，只能运用在 void 返回值的方法上
        //public string print() {
        //    Console.WriteLine("--- CA.Calc");
        //    return "";
        //}
    }

    class CB : CA{
        public override void func3() {
            base.func3();
            Console.WriteLine("--- CB.func3");
            Calc("hello");
        }
    }


    public static void test1() {
        //func1();
        //func2();

        CB b = new CB();
        b.func3();
    }
}
