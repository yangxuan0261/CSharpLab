using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

// 参考
/*
- C# 正则表达式 - https://www.runoob.com/csharp/csharp-regular-expressions.html
*/
public class TestRegex {

    private static void test_catch() {
        string x = "Live for nothing,die for something";
        Regex r1 = new Regex(@"no([a-z]{5}),");
        if (r1.IsMatch(x)) {
            Console.WriteLine("group1 value 2:" + r1.Match(x).Groups[1].Value); //输出：thing  
        }

        string txt1 = "紫色版-7";
        Match mth = new Regex(@"-(\d+)").Match(txt1);
        Console.WriteLine("--- is match ok: " + mth.Success);
        // if (r2.IsMatch(txt1)) {
        //     Console.WriteLine("platId: " + r2.Match(txt1).Groups[1].Value); //输出：thing  
        // }
    }

    public static void test() {
        test_catch();
    }
}