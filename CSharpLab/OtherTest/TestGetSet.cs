using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestGetSet {


    class AAA {
        int mA = 0;
        public int a {
            get { return mA; }
            set { mA = value; }
        }
    }



    public static void test1() {
        AAA obj = new AAA();
        obj.a = 123;
        Console.WriteLine("--- a:{0}", obj.a);
    }
}
