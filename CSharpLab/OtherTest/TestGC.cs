using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

class TestGC {

    class CObject {
        public int a, b, c, d, e;
    };

    public static void test1() {
        long c1 = GC.GetTotalMemory(false);
        Console.WriteLine("Memory used before collection:       {0:N0}",
                       c1);
        List<CObject> ht = new List<CObject>();
        for (int i = 0; i < 1000000; ++i) {
            CObject obj = new CObject();
            obj.a = i;
            ht.Add(obj); //用个容器引用住
        }
        long c2 = GC.GetTotalMemory(false);
        Console.WriteLine("make collection:       {0:N0}",
                       c2 - c1);
        ht.Clear();
        GC.Collect();

        long c3 = GC.GetTotalMemory(false);
        Console.WriteLine("Memory used after full collection:   {0:N0}, dec:{1:N0}",
                          c3, c2 - c3);
    }
}
