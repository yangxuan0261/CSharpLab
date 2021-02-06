using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

class TestReflection {

    //---------------- 官网代码
    //https://msdn.microsoft.com/en-us/library/z919e8tw(v=vs.100).aspx
    // Multiuse attribute.
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct,
                           AllowMultiple = true)  // Multiuse attribute.
    ]
    public class Author : System.Attribute {
        string name;
        public double version;

        public Author(string name) {
            this.name = name;

            // Default value.
            version = 1.0;
        }

        public string GetName() {
            return name;
        }
    }

    // Class with the Author attribute.
    [Author("P. Ackerman")]
    public class FirstClass {
        // ...
    }

    // Class without the Author attribute.
    public class SecondClass {
        // ...
    }

    // Class with multiple Author attributes.
    [Author("P. Ackerman"), Author("R. Koch", version = 2.0)]
    public class ThirdClass {
        // ...
    }

    private static void PrintAuthorInfo(System.Type t) {
        System.Console.WriteLine("Author information for {0}", t);

        // Using reflection.
        System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);  // Reflection.

        // Displaying output.
        foreach (System.Attribute attr in attrs) {
            if (attr is Author) {
                Author a = (Author)attr;
                System.Console.WriteLine("   {0}, version {1:f}", a.GetName(), a.version);
            }
        }
    }

    public static void test1() {
        PrintAuthorInfo(typeof(FirstClass));
        PrintAuthorInfo(typeof(SecondClass));
        PrintAuthorInfo(typeof(ThirdClass));
    }
    /*
        Author information for TestAttribute+FirstClass
            P. Ackerman, version 1.00
        Author information for TestAttribute+SecondClass
        Author information for TestAttribute+ThirdClass
            P. Ackerman, version 1.00
            R. Koch, version 2.00
    */



    //-------------------- 实现类似 unity 的 build 后处理
    [AttributeUsage(AttributeTargets.Method)]
    public class CallOrder : System.Attribute {
        private int _order = 0;

        public CallOrder() { }

        public CallOrder(int order) {
            _order = order;
        }

        public int Order {
            get { return _order; }
        }
    }

    class Indent {
        public Indent() { }
        public Indent(string word) {
            Console.WriteLine("--- constructor, word:{0}", word);
        }

        public string Run(object[] args) { // 反射调用这个方法需要 实例对象
            Console.WriteLine("--- Indent.Run with instance");
            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine("--- arg{0}:{1}", i, args[i]);
            }
            return "Ok!";
        }

        [CallOrder(22)]
        public static void Walk(string word) {
            Console.WriteLine("--- Indent.Walk without instance, say:{0}", word);
        }
    }

    class Cat {
        [CallOrder(44)]
        public static void Walk(string word) {
            Console.WriteLine("--- Cat.Walk without instance, say:{0}", word);
        }
    }

    class Dog {
        [CallOrder(33)]
        public static void Walk(string word) {
            Console.WriteLine("--- Dog.Walk without instance, say:{0}", word);
        }
    }

    class Fish {
        public static void Walk(string word) {
            Console.WriteLine("--- Fish.Walk without instance, say:{0}", word);
        }
    }

    public static void test2() {
        Indent ins = new Indent();

        var allTypes = Assembly.GetExecutingAssembly().GetTypes();
        foreach (var item in allTypes) {
            if (item.Name == "Indent") {
                Console.WriteLine("--- class:{0}", item.Name);

                //参考：http://www.cnblogs.com/binarytree/archive/2010/04/21/1717491.html
                //invoke会把参数数组里面的第一个参数作为参数传递给你要调用的方法
                object[] args = new object[] { 123, "hello", 444.1f };
                args = new object[] { args }; //必须
                item.GetMethod("Run").Invoke(ins, args); // 实例函数，第一个参数为 实例对象，且 args 里的参数的第一个参数会传进这个方法中

                object[] args2 = new object[] { "wolegequ" };
                item.GetMethod("Walk").Invoke(null, args2); //静态函数，不需要 实例对象，args2 直接传进这个方法中
                item.GetMethod("Walk", BindingFlags.Public | BindingFlags.Static).Invoke(null, args2);

                MethodInfo mi = item.GetMethod("Run2");
                if (mi == null) {
                    Console.WriteLine("--- no method call Run2");
                }
            }
        }

        //var allTypes2 = Assembly.GetCallingAssembly().GetTypes();
        //foreach (var item in allTypes2) {
        //    Console.WriteLine("--- class:{0}", item.Name);
        //}

        //var allTypes3 = Assembly.GetEntryAssembly().GetTypes();
        //foreach (var item in allTypes3) {
        //    Console.WriteLine("--- class:{0}", item.Name);
        //}

        //var macroClasses = allTypes.Where(x => x.Namespace.ToUpper().Contains("Indent"));
        //foreach (var tempClass in macroClasses) {
        //    // using reflection I will be able to run the method as:
        //    tempClass.GetMethod("Run").Invoke(null, null);
        //}

    }

    public static void test3() {
        List<MethodInfo> dstList = new List<MethodInfo>();
        var allTypes = Assembly.GetExecutingAssembly().GetTypes();
        MethodInfo mi = null;
        foreach (var item in allTypes) {
            mi = item.GetMethod("Walk", BindingFlags.Public | BindingFlags.Static); // 不加 static 默认找的是实例方法
            if (mi != null) {
                dstList.Add(mi);
            }
        }

        dstList.Sort((MethodInfo mi1, MethodInfo mi2) => {
            CallOrder c1 = mi1.GetCustomAttribute<CallOrder>();
            CallOrder c2 = mi2.GetCustomAttribute<CallOrder>();
            if (c1 == null || c2 == null) 
                return -1; //没有定义顺序属性的放在头部
            else
                return c1.Order.CompareTo(c2.Order); // 有定义的按 升序 排序
        });

        object[] args = new object[] { "TMD" };
        foreach (var item in dstList) {
            CallOrder co = item.GetCustomAttribute<CallOrder>();
            if (co != null) {
                Console.WriteLine("------ order:{0}", co.Order);
                item.Invoke(null, args);
            } else {
                item.Invoke(null, args);
            }
        }
    }

    public static void test4() {
        Indent ins = new Indent();
        Type t = ins.GetType();
        Console.WriteLine("--- Name:{0}", t.Name);
        Indent ins2 = (Indent)Activator.CreateInstance(t, "wolegequ"); //实例化对象
    }
}
