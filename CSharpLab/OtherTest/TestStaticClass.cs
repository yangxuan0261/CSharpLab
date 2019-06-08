using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class TestStaticClass {

    // TestStaticClass() { } // 编译报错, 静态类 不允许实例化, 所以没有构造函数

    static TestStaticClass() { // 可以静态构造函数
        Console.WriteLine("--- static construct");
    }

    static int aaa = 1;

    // int bbb = 2; // 编译报错, 静态类 只能使用 静态 的变量和方法

    public static void test2() {
        Console.WriteLine(aaa);
    }
}