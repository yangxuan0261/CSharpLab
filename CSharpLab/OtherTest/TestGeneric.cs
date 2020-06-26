using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestGeneric {

    public class CDog {
        public string name = "Tom";
    }

    public static T newIns<T>() where T : CDog, new() { // new 是为了可以实例化, CDog 是为了 <> 指定时可以约束
        return new T();
    }

    public static void main() {
        CDog ins = newIns<CDog>();
        Console.WriteLine(string.Format("--- name: {0}", ins.name));

    }
}