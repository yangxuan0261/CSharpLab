using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestException
{
    public void except1()
    {
        try
        {
            int d = 100;
            int c = 0;
            int a = d / c;
        }
        catch
        {
            Console.WriteLine("exception");
        }
        finally
        {
            Console.WriteLine("exe finally");
        }
    }

    public static void test1()
    {
        TestException te = new TestException();
        te.except1();
    }
}
