using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class TestString {
    /// <summary>
    /// Regex Split
    /// </summary>
    public static void test_split() {
        string str = "aaajsbbbjsccc";
        string[] sArray = Regex.Split(str, "js", RegexOptions.IgnoreCase);
        foreach (string i in sArray) {
            Console.WriteLine("{0}", i);
        }

        Console.WriteLine();
        string str2 = "aaajbbbscccjdddseee";
        string[] sArray2 = str2.Split(new char[2] { 'j', 's' });
        foreach (string i in sArray2) {
            Console.WriteLine("{0}", i);
        }

        Console.WriteLine();
        string str3 = "aaajbbbjccc";
        string sp = "j";
        string[] sArray3 = str3.Split(sp.ToArray<char>());
        foreach (string i in sArray3) {
            Console.WriteLine("{0}", i);
        }

        Console.WriteLine();
        string name = "aaa-bbb";
        string[] arr = name.Split('#'); // 如果找不到, 那么这个 arr 将会是 一个长度的数组, 元素就是字符串本身, aaa-bbb
        Console.WriteLine("--- arr not null, len:{0}", arr.Length);
        foreach (var item in arr) {
            Console.WriteLine("--- item: {0}", item);
        }
    }

    /// <summary>
    /// string int
    /// </summary>
    public static void test4() {
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
    public static void test5() {
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
    public static void test6() {
        float f1 = 11.22f;
        int i1 = Convert.ToInt32(f1);
        Console.WriteLine("--- result:{0}", i1);

        int i2 = 33;
        float f2 = Convert.ToSingle(i2);
        Console.WriteLine("--- result:{0}", f2);
    }

    public static void test_format() {
        // 参考: https://www.cnblogs.com/GreenLeaves/p/9171455.html
        string str = String.Format("[{0}]-avgTime:{1:0.00}ms, avgSize:{2:0.00}kb, cnt:{3}\n", "aaa", 123.456f, 987.654f, 666);
        Console.WriteLine("--- str: {0}", str);

        int number = 100;
        string outPut1 = $"{number:D5}"; // D一将整形转换成10进制, D5表示将数字转换成十进制,并以零填充保留5位
        Console.WriteLine("--- outPut1: {0}", outPut1); // 0000000100, 10位, 不足补零

        string outPut2 = $"{number:C6}"; // C一格式化货币, C6代表将数字转换成当前线程国家的货币符号形式的大小并保留6位小数
        Console.WriteLine("--- outPut2: {0}", outPut2); // 0000000100, 10位, 不足补零

        int number2 = 1000000000;
        string outPut4 = $"{number2:N3}"; // N一用分号分隔数字,默认三位加一个分号, N3表示将数字转换成以分号分隔的数字,并保留3位小数
        Console.WriteLine("--- outPut4: {0}", outPut4); // 1,000,000,000.000

        int number3 = 1;
        string outPut5 = $"{number3:P0}"; // P一将数字转成百分比,默认在百分比后面保留两位小数, P0表示将数字转换成百分比,并保留零位小数
        Console.WriteLine("--- outPut5: {0}", outPut5); // 100%

        int number4 = 100;
        string outPut6 = $"{number4:00000}"; // 0一零占位符, 
        string outPut7 = $"{number:00000.00}";
        // 00000表示先用0占5个位子,如果要格式化的值在0的位置有一个数字,则此数字被复制到该0的位置处,如果格式化值得长度大于00000的长度,不会舍弃,原样保存.如果小于则用0填充.
        // .00表示格式化的值的小数部分保留2位,如果第三位大于等于5,则4舍五入.如果小于两位第二位用0填充,以此类推.
        Console.WriteLine("--- outPut6: {0}", outPut6); // 00100
        Console.WriteLine("--- outPut7: {0}", outPut7); // 00100.00

        // 空格占位符
        var number5 = "666";
        string outPut8 = string.Format("{0,10}", number5);
        Console.WriteLine("--- $" + outPut8 + "$"); // $       666$
        string outPut9 = string.Format("{0,-10}", number5);
        Console.WriteLine("--- $" + outPut9 + "$"); // $666       $

        string outPut10 = number5.PadLeft(10); // 当然PadLeft支持填充自定义字符,空格占位符只能用空格
        Console.WriteLine("--- $" + outPut10 + "$"); // $       666$
        string outPut11 = number5.PadRight(10);
        Console.WriteLine("--- $" + outPut11 + "$"); // $666       $

        var now = DateTime.Now;
        var outPut12 = $"{now:yyyy-MM-dd}";
        Console.WriteLine("--- outPut12: {0}", outPut12); // 2020-08-16
    }

    public enum ERes : int {
        Zeus = 1,
        Poseidon = 2,
    }

    public static void test_enum2string() {
        // Enum -> String
        string s1 = Enum.GetName(typeof(ERes), 2);
        string s2 = Enum.GetName(typeof(ERes), ERes.Poseidon);
        Console.WriteLine(string.Format("--- s1: {0}", s1));
        Console.WriteLine(string.Format("--- s2: {0}", s2));
        /*
        --- s1: Poseidon
        --- s2: Poseidon
        */

        // String -> Enum
        ERes e1 = (ERes) Enum.Parse(typeof(ERes), "Poseidon");
        Console.WriteLine(string.Format("--- e1: {0}", e1));
        // --- e1: Poseidon

        // Int -> Enum
        bool b1 = Enum.IsDefined(typeof(ERes), 2);
        Console.WriteLine(string.Format("--- b1: {0}", b1));
        ERes e2 = (ERes) 2;
        Console.WriteLine(string.Format("--- e2: {0}", e2));
    }

    public static void main() {
        // test_split();
        test_format();
    }
}