using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

class TestUsing {
    class MyBase : IDisposable {
        public MyBase(string name) {
            mName = name;
        }
        string mName = "";
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // Free other state (managed objects).
                Console.WriteLine("--- MyBase Dispose, name:{0}", mName);
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
        }

        ~MyBase() {
            // Simply call Dispose(false).
            Dispose(false);
            Console.WriteLine("--- ~MyBase:{0}", mName);
        }
    };

    // Design pattern for a derived class.
    class MyDerived : MyBase {
        public MyDerived(string name) : base(name) {
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                // Release managed resources.
            }
            // Release unmanaged resources.
            // Set large fields to null.
            // Call Dispose on your base class.
            base.Dispose(disposing);
        }
        // The derived class does not have a Finalize method
        // or a Dispose method without parameters because it inherits
        // them from the base class.
    }

    /// <summary>
    /// https://msdn.microsoft.com/zh-cn/library/yh598w02.aspx
    /// https://msdn.microsoft.com/zh-cn/library/b1yfkh5e(v=vs.85).aspx
    /// 实现类似c++中超过作用域后析构的效果，但c#中超过 using 只是掉了 Dispose 接口，对象还是没有被gc
    /// 实际应用如 FileStream 等，不需要去主动调用 Close()
    /// </summary>
    public static void test1() {

        Console.WriteLine("---test1 begin");

        using (MyDerived mt1 = new MyDerived("hello"),
                      mt2 = new MyDerived("world")) {
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

    class CA {
        public CA() {
            Console.WriteLine("CA construct");
        }

        public void Dispose() {
            Console.WriteLine("CA.Dispose");
        }
    }

    public static void test2() {
        CA a = new CA();
        //快速保护方法是使用 as 语句可以转换为安全可回收对象不管是否实现 IDisposable 接口：
        using (a as IDisposable) { //如果CA没有继承 IDisposable 并实现 Dispose 接口，则这行代码等价 using(null)
            Console.WriteLine("CA using");
        }
        Console.WriteLine("func over");
    }
}
