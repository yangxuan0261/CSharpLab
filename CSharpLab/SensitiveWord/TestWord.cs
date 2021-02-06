using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ToolGood.Words;

class TestWord {
   

    public static void test1() 
    {
        string s = "中国|国人|zg人|asd";
        string test = "我是中国人斯蒂zg人芬斯蒂芬速度发asd哦键盘机票";

        WordsSearchEx wordsSearch = new WordsSearchEx();
        wordsSearch.SetKeywords(s.Split('|'));

        var t = wordsSearch.Replace(test, '*');
        //Assert.AreEqual("我是***",t);
        Console.WriteLine("结果: {0}", t);
    }
}
