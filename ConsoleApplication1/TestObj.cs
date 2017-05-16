using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestObj {
    class Abs {
        private int i = 1;
        public Dictionary<string, string> _map = new Dictionary<string, string>();
        public string this[string key] {
            get {
                return _map.ContainsKey(key) ? _map[key] : null;
            }

            set {
                _map.Add(key, value);
            }
        }

        public void method1() {
            Console.WriteLine("--- method1");
        }
    }

    public static void test1() {
        Abs a = new Abs();
        a["aaa"] = "sdf";
        Console.WriteLine("---  key:{0}", a["aaa"]);
        Abs b = new Abs();
        Console.WriteLine("--- a addr:{0}", a.GetHashCode());
        Console.WriteLine("--- b addr:{0}", b.GetHashCode());

        bool eq = a.Equals(b);
        Console.WriteLine("--- equal:{0}", eq);

        Console.WriteLine("--- a.ToString = {0}", a.ToString());
        Console.WriteLine("--- a.GetType = {0}", a.GetType().ToString());

    }


    class Rect {
        public void print() {
            Console.WriteLine("Rect.print");
        }
    }

    class Circle {
        public void print() {
            Console.WriteLine("Circle.print");
        }

        public static implicit operator Rect(Circle c) { // 尼玛可以这样重写 隐式转换 或 强制转换
            return new Rect();
        }
    }

    public static void test2() {
        Circle c = new Circle();
        Rect a = (Rect)c; 
        a.print();
    }

    public static void testArgs(string name1, string name2) {
        Console.WriteLine("name1:{0}, name2:{1}", name1, name2);
    }

    public static void test3() {
        testArgs(name2: "hello", name1:"world");
    }

    class CA {
        public CA() {
            Console.WriteLine("CA construct");
        }
    }

    class CD {
        public static CA _a = new CA();

        static CD() {
            Console.WriteLine("CD static construct");
        }

        public CD() {
            Console.WriteLine("CD construct");
        }
    }

    public static void test4() {
        CD d = new CD();
        //CA construct
        //CD static construct
        //CD construct
    }

    public class MyClass {// collection of data
        private List<Circle> coll;
        private string name;
        public MyClass() :this(0, string.Empty) { }
        public MyClass(int initialCount = 0, string name = "") {
            coll = (initialCount > 0) ?  new List<Circle>(initialCount) : new List<Circle>();
            this.name = name;
        }
    }

}
