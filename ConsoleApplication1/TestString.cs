using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

class TestString
{
    public static void test1()
    {
        string str = "aaajsbbbjsccc";
        string[] sArray = Regex.Split(str, "js", RegexOptions.IgnoreCase);
        foreach (string i in sArray)
            Console.WriteLine("{0}", i);
    }

    public static void test2()
    {
        string str = "aaajbbbscccjdddseee";
        string[] sArray = str.Split(new char[2] { 'j', 's' });
        foreach (string i in sArray)
            Console.WriteLine("{0}", i);
    }

    public static void test3()
    {
        string str = "aaajbbbjccc";
        string[] sArray = str.Split('j');
        foreach (string i in sArray)
            Console.WriteLine("{0}", i);
    }
}
