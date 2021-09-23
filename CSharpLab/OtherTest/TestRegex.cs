using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// 参考
/*
- C# 正则表达式 - https://www.runoob.com/csharp/csharp-regular-expressions.html

原文链接：https://blog.csdn.net/qq_38507850/article/details/79179128
(1)"\d"这个符号代表从0-9的数字字符。
(2)"\w"代表所有单词字符，包括：大小写字母a-z、数字0-9、汉字(其实我认为是各国文字字符都可以但是身为中国人应该只用到了汉字)、下划线。
(3)"\s"代表任何空白字符，所谓空白字符就是打出来是空白的字符，包括：空格、制表符、换页符、换行符、回车符等等。
(4)"\D"代表任何非数字字符。
(5)"\W"代表任何非单词字符。
(6)"\S"代表任何非空白字符。
(7)"."代表除换行符(\n)之外的任何字符。
*/

/*
@字面量字符串中 如果有双引号怎么办？
就在双引号前面，加一个双引号就好了, 如:
Regex reg = new Regex(@"[.*?] = ""(.*?)"""); 可以匹配: [EnumUI.Broadcast] = "logic.ui.common", 并捕获 logic.ui.common
*/
public class TestRegex {

    private static void test_catch() {
        string txt1 = "紫色版-7-123";
        Match mth1 = new Regex(@"-(\d+)-(\d+)").Match(txt1);
        Console.WriteLine("--- is match ok: {0}, cnt: {1}", mth1.Success, mth1.Groups.Count); // is match ok: True, cnt: 3
        if (mth1.Success) {
            Console.WriteLine("platId0: {0}", mth1.Groups[0].Value); // -7-123
            Console.WriteLine("platId1: {0}", mth1.Groups[1].Value); // 7
            Console.WriteLine("platId2: {0}", mth1.Groups[2].Value); // 123
        }
        Match mth2 = new Regex(@"-(?:\d+)-(\d+)").Match(txt1); // ?: 表示只是匹配, 但不捕获
        Console.WriteLine("--- is match ok: {0}, cnt: {1}", mth2.Success, mth2.Groups.Count); // is match ok: True, cnt: 2
        if (mth2.Success) {
            Console.WriteLine("platId0: {0}", mth2.Groups[0].Value); // -7-123
            Console.WriteLine("platId1: {0}", mth2.Groups[1].Value); // 123
            Console.WriteLine("platId2: {0}", mth2.Groups[2].Value); // ""
        }

        string txt2 = "23794大富科世纪东方了djfkasdl@qq.com9548dhf28340385@163.comsdfjsd  2349@sina.com305983*&*&*2";
        MatchCollection mths1 = new Regex(@"[a-zA-Z0-9]+@[a-zA-z0-9]+\.com").Matches(txt2);
        foreach (Match match in mths1) {
            Console.WriteLine("--- mth: {0}", match);
        }

        string txt3 = "nihao123@qq.com   myemail@163.comasjdfjsdf";
        MatchCollection mths2 = new Regex(@"([a-zA-Z0-9_]+)@([a-zA-Z0-9]+)\.com").Matches(txt3);;
        Console.WriteLine("--- cnt: {0}", mths2.Count);
        for (int i = 0; i < mths2.Count; i++) {
            Console.WriteLine("匹配到的第 {0} 个邮箱结果是 {1}，对应用户名是 {2}, 邮箱域名: {3}", i + 1, mths2[i], mths2[i].Groups[1].Value, mths2[i].Groups[2].Value);
        }
    }

    private static void test_replace() {
        string txt1 = "紫色版-7-123";
        string res1 = new Regex(@"-(\d+)").Replace(txt1, "wolegequ", 1); // 1 表示只替换一次, -1 表示所有
        Console.WriteLine("--- res1: {0}", res1); // res1: 紫色版wolegequ-123

        string res2 = new Regex(@"-(\d+)").Replace(txt1, (mth) => {
            Console.WriteLine("--- mth: {0}", mth.ToString()); // mth: -7, mth: -123
            return "hello";
        });
        Console.WriteLine("--- res2: {0}", res2); // res2: 紫色版hellohello

        // 捕获替换 1:
        string res3 = new Regex(@"(-)\d+(-)").Replace(txt1, string.Format("$1wolegequ$2"), 1); // $1 $2 代表捕获值, $0 是 匹配串, 缺点是不能连着使用 格式化占位符, 比如: string.Format("$1{0}", "aaa) 就会有问题
        Console.WriteLine("--- res3: {0}", res3); // res2: 紫色版hellohello

        // 捕获替换 2: (建议)
        string res4 = Regex.Replace(txt1, @"(-)\d+(-)", m => m.Groups[1].Value + "hello" + m.Groups[2].Value);
        Console.WriteLine("--- res4: {0}", res4); // res2: 紫色版hellohello
    }

    private static void test_catch02() {
        string str1 = "-aAS12=123";
        // String[] token = Regex.Split(str1, ",");
        Match mth = new Regex(@"\-(\w+)=(.*)").Match(str1);
        Console.WriteLine("--- match: {0}", mth.Success);

        if (mth.Success) {
            Console.WriteLine("--- 1: {0}, 2: {1}", mth.Groups[1].Value, mth.Groups[2].Value);
        }
    }

    // 不包含字符串
    private static void test_exclude() {
        string path01 = "E:/its/its_rummy-7324/Assets/Plugins/iOS/plugin.plist";
        string path02 = "E:/its/its_rummy-7324/Assets/Plugins/iOS/plugin.plist.meta";

        Regex rgx = new Regex(@".*\.plist(?!\.meta)"); // 排除 .meta 后缀的文件, 使用 (?!xxx)
        Match mth01 = rgx.Match(path01); // true
        Match mth02 = rgx.Match(path02); // false

        Console.WriteLine("--- mth01: {0}", mth01.Success);
        Console.WriteLine("--- mth02: {0}", mth02.Success);
    }

    // 批量替换
    private static void test_multi_replace() {
        string txt01 = "hello_world_hello_nice_to_meet_you";
        IDictionary<string, string> map = new Dictionary<string, string>() { { "hello", "aaa" }, { "world", "bbb" }, { "you", "me" } };
        Regex regex = new Regex(String.Join("|", map.Keys));
        string txt02 = regex.Replace(txt01, m => map[m.Value]);
        Console.WriteLine("--- txt02: {0}", txt02);
    }

    public static void main() {
        // test_catch();
        // test_replace();
        // test_catch02();
        // test_exclude();
        test_multi_replace();
    }
}