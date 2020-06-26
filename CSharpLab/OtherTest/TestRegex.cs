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
public class TestRegex {

    private static void test_catch() {
        string txt1 = "紫色版-7";
        Match mth = new Regex(@"-(\d+)$").Match(txt1);
        Console.WriteLine("--- is match ok: " + mth.Success);
        if (mth.Success) {

            Console.WriteLine("coutn: {0}", mth.Groups.Count); //输出：thing  
            Console.WriteLine("platId: {0}", mth.Groups[1].Value); //输出：thing  
            Console.WriteLine("platId: {0}", mth.Groups[2].Value); //输出：thing  
        }

        string txt2 = "23794大富科世纪东方了djfkasdl@qq.com9548dhf28340385@163.comsdfjsd  2349@sina.com305983*&*&*2";
        MatchCollection mths1 = new Regex(@"[a-zA-Z0-9]+@[a-zA-z0-9]+\.com").Matches(txt2);
        foreach (Match match in mths1) {
            Console.WriteLine("--- mth: {0}", match);
        }

        string txt3 = "nihao123@qq.com   myemail@163.comasjdfjsdf";
        MatchCollection mths2 = new Regex(@"([a-zA-Z0-9_]+)@([a-zA-Z0-9]+)\.com").Matches(txt3);;
        for (int i = 0; i < mths2.Count; i++) {
            Console.WriteLine("匹配到的第 {0} 个邮箱结果是 {1}，对应用户名是 {2}, 邮箱域名: {3}", i + 1, mths2[i], mths2[i].Groups[1].Value, mths2[i].Groups[2].Value);
        }
    }

    public static void main() {
        test_catch();
    }
}