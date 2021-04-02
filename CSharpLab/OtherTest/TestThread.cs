using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

class TestThread {

    public static void main() {
        object obj01 = "hello world";
        Thread t = new Thread((obj) => {
            Thread.Sleep(1000 * 3);
            Console.WriteLine("--- thread 001, obj: {0}", obj);
            Thread.Sleep(1000 * 3);
        });
        Console.WriteLine("--- main thread 111");
        t.Start(obj01);
        t.Join();
        Console.WriteLine("--- main thread 222");
    }
}