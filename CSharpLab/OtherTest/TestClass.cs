using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class TestClass {

    static int aaa = 1;
    int bbb = 1;

    public TestClass() {
        Console.WriteLine("--- instance construct");
        aaa += 1;
    }

    static TestClass() { // 可以静态构造函数
        Console.WriteLine("--- static construct");
    }


    public void InsOutput() {
        Console.WriteLine("--- InsOutput, aaa:{0}, bbb:{1}", aaa, bbb);
    }

    public static void StaticOutput() {
        Console.WriteLine("--- StaticOutput, aaa:{0}", aaa);
        // Console.WriteLine("--- aaa:{0}, bbb:{1}", aaa, bbb); // 编译报错, bbb 是实例变量
    }

    public static void test2() {
        TestClass tc1 = new TestClass();
        TestClass tc2 = new TestClass();
        TestClass tc3 = new TestClass();

        tc3.InsOutput();
        TestClass.StaticOutput();

        /*
--- static construct // 可以看出 静态构造函数最先执行
--- instance construct
--- instance construct
--- instance construct
--- InsOutput, aaa:4, bbb:1
--- StaticOutput, aaa:4
         */
    }
}