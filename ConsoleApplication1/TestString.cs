using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

class TestString
{
    /// <summary>
    /// Regex Split
    /// </summary>
    public static void test1()
    {
        string str = "aaajsbbbjsccc";
        string[] sArray = Regex.Split(str, "js", RegexOptions.IgnoreCase);
        foreach (string i in sArray)
            Console.WriteLine("{0}", i);
    }

    /// <summary>
    /// string Split
    /// </summary>
    public static void test2()
    {
        string str = "aaajbbbscccjdddseee";
        string[] sArray = str.Split(new char[2] { 'j', 's' });
        foreach (string i in sArray)
            Console.WriteLine("{0}", i);
    }

    /// <summary>
    /// string Split
    /// </summary>
    public static void test3()
    {
        string str = "aaajbbbjccc";
        string sp = "j"; 
        string[] sArray = str.Split(sp.ToArray<char>());
        foreach (string i in sArray)
            Console.WriteLine("{0}", i);
    }

    /// <summary>
    /// string int
    /// </summary>
    public static void test4() 
    {
        int varInt = 1;
        string varString = Convert.ToString(varInt);
        string varString2 = varInt.ToString();
        Console.WriteLine("--- varString2:{0}", varString2);

        string str = string.Empty;
        str = "123";
        int result = int.Parse(str);
        Console.WriteLine("--- result:{0}", result);

        string str2 = string.Empty;
        str2 = "xyz";
        int result2;
        int.TryParse(str2, out result2);
        Console.WriteLine("--- result2:{0}", result2);
    }

    /// <summary>
    /// string float
    /// </summary>
    public static void test5()
    {
        float varFloat = 11.22f;
        string varString2 = varFloat.ToString();
        Console.WriteLine("--- varString2:{0}", varString2);

        string str2 = "33.44";
        float result = float.Parse(str2);
        Console.WriteLine("--- result:{0}", result);
    }

    /// <summary>
    /// float int
    /// </summary>
    public static void test6()
    {
        float f1 = 11.22f;
        int i1 = Convert.ToInt32(f1);
        Console.WriteLine("--- result:{0}", i1);

        int i2 = 33;
        float f2 = Convert.ToSingle(i2);
        Console.WriteLine("--- result:{0}", f2);
    }

    public static void test7()
    {
        string str = String.Format("[{0}]-avgTime:{1:0.00}ms, avgSize:{2:0.00}kb, cnt:{3}\n"
    , "aaa"
    , 123.456f
    , 987.654f
    , 666);
        Console.WriteLine(str);
    }

}
