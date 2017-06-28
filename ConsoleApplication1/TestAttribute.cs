using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestAttribute {

    //CatAttribute 这样命名使用时是 Cat
    class CatAttribute : Attribute {

    }

    //建议取名：HobbyAttribute
    class Hobby : Attribute // 必须以System.Attribute 类为基类
    {
        // 参数值为null的string 危险，所以必需在构造函数中赋值
        public Hobby(string _type) // 定位参数
        {
            this.type = _type;
        }
        //兴趣类型
        private string type;
        public string Type {
            get { return type; }
            set { type = value; }
        }
        //兴趣指数
        private int level;
        public int Level {
            get { return level; }
            set { level = value; }
        }
    }

    //注意："Sports" 是给构造函数的赋值， Level = 5 是给属性的赋值。
    [Hobby("Sports", Level = 5)]
    class Student {
        [Hobby("Football"), Cat]
        public string profession;
        public string Profession {
            get { return profession; }
            set { profession = value; }
        }
    }

    public static void test1() {
        //使用反射读取Attribute
        System.Reflection.MemberInfo info = typeof(Student); //通过反射得到Student类的信息
        Hobby hobbyAttr = (Hobby)Attribute.GetCustomAttribute(info, typeof(Hobby));
        if (hobbyAttr != null) {
            Console.WriteLine("类名：{0}", info.Name);
            Console.WriteLine("兴趣类型：{0}", hobbyAttr.Type);
            Console.WriteLine("兴趣指数：{0}", hobbyAttr.Level);
        }
    }

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

    public static void test2() {
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




}
