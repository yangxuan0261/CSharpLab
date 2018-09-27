using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace TestClsExt {

    //partial，将Human类拆分出来，c# 2.0的语法糖
    public partial class Human {
        public string mName;
        public int mAge;

        public void Walk() {
            Console.WriteLine("--- {0} Walk", mName);
        }
    }

    public partial class Human {
        public void Idle() {
            Console.WriteLine("--- {0} Idle", mName);
        }
    }

    //扩展类的方法
    public static class MyExt {
        public static void Run(this Human target) {
            Console.WriteLine("--- {0} Run", target.mName);
        }
    }

    public class TestClsExt2 {
        public static void test1() {
            Human a = new Human();
            a.mName = "aaa";
            a.Walk();
            a.Run();
            a.Idle();
        }
    }
}
