using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

class TestUsing
{
    class MyTest : IDisposable
    {
        public MyTest(string name)
        {
            mName = name;
        }
        string mName = "";
        public void Dispose()
        {
            Console.WriteLine("--- MyTest Dispose, name:{0}", mName);
        }
    };

    /// <summary>
    /// https://msdn.microsoft.com/zh-cn/library/yh598w02.aspx
    /// 实现类似c++中超过作用域后析构的效果，但c#中超过 using 只是掉了 Dispose 接口，对象还是没有被gc
    /// 实际应用如 FileStream 等，不需要去主动调用 Close()
    /// </summary>
    public static void test1()
    {
        Console.WriteLine("---test1 begin");

        using (MyTest mt1 = new MyTest("hello"), 
                      mt2 = new MyTest("world"))
        {
            Console.WriteLine("--- instantiate mt1 mt2");
        }

        Console.WriteLine("---test1 end");
        //结果：
        //---test1 begin
        //---instantiate mt1 mt2
        //---MyTest Dispose, name: world
        //---MyTest Dispose, name: hello
        //---test1 end

        //using (FileStream fs = File.Create(path))
        //{
        //    byte[] info = new UTF8Encoding(true).GetBytes(mFinalStr);
        //    fs.Write(info, 0, info.Length);
        //}
        //return path;
    }
}
