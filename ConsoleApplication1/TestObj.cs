using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestObj
{
    class Abs 
    {
        private int i = 1;
        public void method1()
        {
            Console.WriteLine("--- method1");
        }
    }

    public static void test1()
    {
        Abs a = new Abs();
        Abs b = new Abs();
        Console.WriteLine("--- a addr:{0}", a.GetHashCode());
        Console.WriteLine("--- b addr:{0}", b.GetHashCode());

        bool eq = a.Equals(b);
        Console.WriteLine("--- equal:{0}", eq);
    }
}
