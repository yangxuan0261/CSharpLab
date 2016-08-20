using System;
using System.Collections.Generic;
using System.Linq;

class TestLambda
{
    public delegate string dlg1(int _a);

    public class People
    {
        public int age { get; set; }                //设置属性  
        public string name { get; set; }            //设置属性  
        public People(int age, string name)      //设置属性(构造函数构造)  
        {
            this.age = age;                 //初始化属性值age  
            this.name = name;               //初始化属性值name  
        }
    }

    /// <summary>
    /// test lambda and ienumeator
    /// </summary>
    public static void test1()
    {
        List<People> peopleList = new List<People>();
        People p1 = new People(21, "guojing");
        People p2 = new People(21, "wujunmin");
        People p3 = new People(20, "muqing");
        People p4 = new People(23, "lupan");
        peopleList.Add(p1);
        peopleList.Add(p2);
        peopleList.Add(p3);
        peopleList.Add(p4);

        //IEnumerable<People> results = peopleList.Where (delegate (People p) { return p.age > 20; });
        IEnumerable<People> results2 = peopleList.Where(_people => _people.age > 20);
        IEnumerator<People> itor = results2.GetEnumerator();
        while (itor.MoveNext())
        {
            People p = itor.Current;
            Console.WriteLine("--- name:{0}, age{1}", p.name, p.age);
        }
    }

    public static void test2()
    {
        string str1 = "hello ";
        dlg1 d = delegate (int _num) {
            Console.WriteLine("--- num:{0}", _num);
            return str1 + "world";
        };
        Console.WriteLine("--- ret:{0}", d(123));
    }
}
