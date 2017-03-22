using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestAttribute
{
    public class MyAttr : System.Attribute
    {
        private Type type;
        public MyAttr(Type t) { type = t; }
    }

    public class Human
    {
        [MyAttr(typeof(int))]
        public static void func1()
        {
            Console.WriteLine("--- func1");
        }
    }

    public static void test1()
    {
        //Human.func1();
        //int a = 1;
    }
}
