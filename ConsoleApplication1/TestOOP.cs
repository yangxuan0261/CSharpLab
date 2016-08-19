using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestOOP
{
    class Base
    {
        protected string mName;
        public virtual void method1()
        {
            Console.WriteLine("--- Base method1");
        }
    }

    class AAA : Base
    {
        public override void method1()
        {
            //base.method1();
            Console.WriteLine("--- AAA method1");
        }
    }

    class BBB : AAA
    {
        public override void method1()
        {
            //base.method1();
            Console.WriteLine("--- BBB method1");
        }
    }

    public static void test1()
    {
        Base obj1 = new BBB();
        obj1.method1();
    }
}
