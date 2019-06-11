using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestContainer {
    public static void printDict<K, V>(Dictionary<K, V> _dir) {
        Console.WriteLine("");
        foreach (KeyValuePair<K, V> kvp in _dir) {
            Console.WriteLine("--- key={0},value={1}", kvp.Key, kvp.Value);
        }
    }

    public static void printSortDict<K, V>(SortedDictionary<K, V> _dir) {
        Console.WriteLine("");
        foreach (KeyValuePair<K, V> kvp in _dir) {
            Console.WriteLine("--- key={0},value={1}", kvp.Key, kvp.Value);
        }
    }

    public static void test1() {
        Dictionary<string, string> myDic = new Dictionary<string, string>();
        myDic.Add("aaa", "111");
        myDic.Add("bbb", "222");
        myDic.Add("ccc", "333");
        myDic.Add("ddd", "444");

        printDict(myDic);
        try {
            myDic.Add("ddd", "ddd");
        } catch (ArgumentException ex) {
            Console.WriteLine("此键已经存在：" + ex.Message);
        }
        //解决add()异常的方法是用ContainsKey()方法来判断键是否存在
        if (!myDic.ContainsKey("ddd"))
            myDic.Add("ddd", "ddd");
        else
            Console.WriteLine("此键已经存在：");

        //而使用索引器来负值时，如果建已经存在，就会修改已有的键的键值，而不会抛出异常
        myDic["ddd"] = "ddd";
        myDic["eee"] = "555";
        printDict(myDic);

        //使用索引器来取值时，如果键不存在就会引发异常
        try {
            Console.WriteLine("不存在的键fff的键值为：" + myDic["fff"]);
        } catch (KeyNotFoundException ex) {
            Console.WriteLine("没有找到键引发异常：" + ex.Message);
        }

        //解决上面的异常的方法是使用ContarnsKey() 来判断时候存在键，如果经常要取健值得化最好用 TryGetValue方法来获取集合中的对应键值
        myDic.Add("fff", "fffffffffffffffff");
        string value = "";
        if (myDic.TryGetValue("fff", out value))
            Console.WriteLine("键fff的值为：" + value);
        else
            Console.WriteLine("没有找到对应键的键值");

        //foreach values
        Console.WriteLine("\n--- foreach values");
        foreach (string s in myDic.Values) {
            Console.WriteLine("--- value={0}", s);
        }

        //foreach keys
        Console.WriteLine("\n--- foreach keys");
        var keys = myDic.Keys;
        foreach (string s in keys) {
            Console.WriteLine("--- key={0}", s);
        }

        //remove key
        myDic.Remove("aaa");
        printDict(myDic);

        //size
        Console.WriteLine("--- size={0}", myDic.Count());
        //myDic.Clear();
        //Console.WriteLine("--- size={0}", myDic.Count());

        /* cant remove during foreach
        foreach (KeyValuePair<string, string> kvp in myDic)
        {
            if (kvp.Key == "fff")
            myDic.Remove(kvp.Key);
        }
        Console.WriteLine("--- size={0}", myDic.Count());
        */

        //put key need to be remove to LinkedList(dont use List)
        //and foreach LinkedList, remove from 
        myDic.Add("tt", "ttttt");
        myDic.Add("yy", "yyyyyy");
        Console.WriteLine("--- size={0}", myDic.Count());
        LinkedList<string> tmpList = new LinkedList<string>();
        foreach (string k in myDic.Keys) {
            if (k.Count() == 2)
                tmpList.AddFirst(k);
        }

        foreach (string k in tmpList)
            myDic.Remove(k);
        Console.WriteLine("--- size={0}", myDic.Count());

        //lambda find
        myDic.Add("i", "iiii");
        myDic.Add("o", "oooo");
        IEnumerable<KeyValuePair<string, string>> result = myDic.Where(_pair => {
            if (_pair.Key.Count() == 1)
                return true;
            else
                return false;
        });

        IEnumerator<KeyValuePair<string, string>> ret2 = result.GetEnumerator();
        while (ret2.MoveNext())
            Console.WriteLine("--- ret2, key={0}, value={1}", ret2.Current.Key, ret2.Current.Value);
    }

    public static void test2() {
        List<int> mylist = new List<int>();
        mylist.Add(111);
        mylist.Add(222);
        Console.WriteLine("--- size={0}", mylist.Count());
    }

    /// <summary>
    /// test list sort
    /// </summary>
    public static void testListDort00() {
        Dictionary<int, string> mydic = new Dictionary<int, string>();
        mydic.Add(8, "888");
        mydic.Add(5, "555");
        mydic.Add(7, "777");
        mydic.Add(6, "666");
        printDict(mydic);

        List<int> keys = mydic.Keys.ToList();
        keys.Sort();
        foreach (int k in keys)
            Console.WriteLine("--- key:{0}, value:{1}", k, mydic[k]);
    }

    struct Info {
        public Info(int _a, string _b) {
            num = _a;
            name = _b;
            Console.WriteLine("--- construct info");
        }
        public int num;
        public string name;
    }

    public static void testDictDeleteByVaule() {
        Dictionary<int, Info> ht = new Dictionary<int, Info>();
        ht.Add(1, new Info(1, "aaa"));
        ht.Add(2, new Info(2, "asd"));
        ht.Add(3, new Info(3, "bbb"));

        //涉及复制拷贝，效率低
        //List<Info> tmp = new List<Info>(ht.Values);
        //foreach (var w in tmp)
        //{
        //    if (w.name == "asd")
        //    {
        //        ht.Remove(w.num);
        //    }
        //}

        //要删除的东西的key丢在临时容器tmp，然后遍历tmp，remove
        List<int> tmp = new List<int>();
        foreach (var w in ht) {
            if (w.Value.name == "asd") {
                tmp.Add(w.Key);
            }
        }

        foreach (var key in tmp) {
            ht.Remove(key);
        }

        foreach (var w in ht) {
            Console.WriteLine("--- key:{0}, value:{1}", w.Key, w.Value.name);
        }

    }

    /// <summary>
    /// List 删除
    /// </summary>
    public static void testListDelete() {
        List<int> ht = new List<int>();
        ht.Add(1);
        ht.Add(2);
        ht.Add(3);
        ht.Add(4);
        ht.Add(5);
        ht.Add(6);
        ht.Add(7);

        for (int i = 0; i < ht.Count; ++i) {
            /*
            if (ht[i] == 3) //正确的删除姿势
            {
                ht.Remove(ht[i]);
                i -= 1; //-1 是因为删除时 后面的 往 前面 挪了一位
            }

            if (ht[i] == 2) //正确的删除姿势
            {
                ht.Remove(ht[i]);
                i -= 1;
            }

            */

            if (ht[i] == 3) //正确的删除姿势
            {
                ht.RemoveAt(i);
                i -= 1;
            }

            if (ht[i] == 2) //正确的删除姿势
            {
                ht.RemoveAt(i);
                i -= 1;
            }
        }

        //ht.RemoveRange(0, 2);

        //错误的删除姿势
        //foreach (var num in ht)
        //{
        //    if (num == 1)
        //        ht.Remove(num);
        //}

        foreach (var num in ht)
            Console.WriteLine(num);
    }

    public class Dog : IComparable {
        public Dog(int a, string b) { age = a; name = b; }
        public int age;
        public string name;

        public int CompareTo(object obj) {
            Dog d = (Dog) obj;

            //return Convert.ToInt32(age > d.age);//错误
            return age.CompareTo(d.age); // 正确
        }
    }

    /// <summary>
    /// List 排序
    /// </summary>
    public static void testListSort() {
        List<Dog> ht = new List<Dog>();
        ht.Add(new Dog(12, "aaa"));
        ht.Add(new Dog(21, "ccc"));
        ht.Add(new Dog(21, "bbb"));
        ht.Add(new Dog(32, "ccc"));
        ht.Add(new Dog(27, "ddd"));
        ht.Add(new Dog(14, "eee"));

        //不需要继承IComparable，实现CompareTo接口
        ht.Sort(delegate(Dog d1, Dog d2) {
            //单条件排序
            //int ret = Convert.ToInt32(d1.age - d2.age);
            //int ret = d1.age - d2.age;

            //多条件排序
            int ret = d1.age.CompareTo(d2.age);
            if (ret == 0) {
                ret = d1.name.CompareTo(d2.name);
            }
            Console.WriteLine("--- ret:{0}", ret);
            return ret;
        });

        //需要继承IComparable，实现CompareTo接口
        //ht.Sort(); 
        //ht.Sort(delegate(Dog d1, Dog d2) { return d1.CompareTo(d2); });

        foreach (var item in ht) {
            Console.WriteLine("--- age:{0}, name:{1}", item.age, item.name);
        }
    }

    /// <summary>
    /// Dictionary List 删除
    /// </summary>
    public static void testDictDelete() {
        Dictionary<int, string> ht = new Dictionary<int, string>();
        ht.Add(1, "aa");
        ht.Add(2, "bbb");
        ht.Add(3, "cccc");
        ht.Add(4, "dd");

        int[] keys = ht.Where((p) => p.Value.Length == 2).Select((p) => { return p.Key; }).ToArray(); // 通过 where 筛选出需要的 kv, 通过 Select 取需要的 k 或 value
        // Select 也可以这样简写 Select(p => p.Key)
        for (int i = 0; i < keys.Length; i++)
            ht.Remove(keys[i]);

        foreach (var p in ht)
            Console.WriteLine("--- key:{0}, value:{1}", p.Key, p.Value);
    }

    /// <summary>
    /// 队列筛选
    /// </summary>
    public static void testListWhere() {
        List<string> ht = new List<string>();
        ht.Add("aa");
        ht.Add("bbb");
        ht.Add("cccc");
        ht.Add("dd");

        //string[] keys = ht.Where((p) => p.Length == 2).Select((p) => p).ToArray();
        string[] keys = ht.Where((p, index) => {
            if (p.Length == 2) {
                Console.WriteLine("--- index:{0}", index);
                return true;
            } else
                return false;
        }).ToArray();

        for (int i = 0; i < keys.Length; i++)
            ht.Remove(keys[i]);

        foreach (var p in ht)
            Console.WriteLine("--- value:{0}", p);
    }

    public static void test9() {
        List<string> ht = new List<string>();
        ht.Add("aa");
        ht.Add("bbb");
        ht.Add("cccc");
        ht.Add("dd");

        List<string> ht2 = ht.Select((item) => { return "new " + item; }).ToList();
        foreach (var p in ht2)
            Console.WriteLine("--- value:{0}", p);

        Console.WriteLine("");
        string[] ht3 = ht.Select((item) => { return "new " + item; }).ToArray();
        foreach (var p in ht3)
            Console.WriteLine("--- value:{0}", p);

        Console.WriteLine("");
        var result = from i in ht
        where i.Length > 2
        select i;
        var ht4 = result.ToList();
        foreach (var p in ht4)
            Console.WriteLine("--- value:{0}", p);
    }

    /// <summary>
    /// 两个队列去重
    /// </summary>
    public static void testQuchong() {
        List<string> destroyList = new List<string>();
        destroyList.Add("aa");
        destroyList.Add("bbb");
        destroyList.Add("cccc");
        destroyList.Add("dd");

        List<string> notAliveList = new List<string>();
        notAliveList.Add("bbb");
        notAliveList.Add("cccc");
        //ht2.Add("www");
        //ht2.Add("zzz");

        HashSet<string> ht3 = new HashSet<string>();
        HashSet<string> diffList = new HashSet<string>();

        foreach (var v in destroyList)
            ht3.Add(v);

        foreach (var v in notAliveList) {
            if (ht3.Contains(v))
                ht3.Remove(v);
        }

        foreach (var v in ht3)
            Console.WriteLine("--- value:{0}", v);

    }

    /// <summary>
    /// Dictionary 深拷贝
    /// </summary>
    public static void testDepthCopy() {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        dictionary.Add("cat", 1);
        dictionary.Add("dog", 3);
        dictionary.Add("iguana", 5);

        Dictionary<string, int> copy = new Dictionary<string, int>(dictionary);

        dictionary.Add("fish", 4);

        Console.WriteLine("--- Dictionary 1 ---");
        foreach (var pair in dictionary) {
            Console.WriteLine(pair);
        }

        Console.WriteLine("--- Dictionary 2222 ---");
        foreach (var pair in copy) {
            Console.WriteLine(pair);
        }
    }

    public static void testSortDict01() {
        Dictionary<int, string> dict01 = new Dictionary<int, string>();
        dict01.Add(4, "aaa");
        dict01.Add(1, "bbb");
        dict01.Add(3, "ccc");
        dict01.Add(2, "ddd");
        printDict(dict01);
        /*
--- key=4,value=aaa
--- key=1,value=bbb
--- key=3,value=ccc
--- key=2,value=ddd
         */

        SortedDictionary<int, string> sdict01 = new SortedDictionary<int, string>();
        sdict01.Add(4, "aaa");
        sdict01.Add(1, "bbb");
        sdict01.Add(3, "ccc");
        sdict01.Add(2, "ddd");
        printSortDict(sdict01);

        /*
--- key=1,value=bbb
--- key=2,value=ddd
--- key=3,value=ccc
--- key=4,value=aaa
         */
    }

    public static void testStack() {
        /*
        Count	获取 Stack 中包含的元素个数。

        1	public virtual void Clear(); 
        从 Stack 中移除所有的元素。
        2	public virtual bool Contains( object obj ); 
        判断某个元素是否在 Stack 中。
        3	public virtual object Peek();
        返回在 Stack 的顶部的对象，但不移除它。
        4	public virtual object Pop();
        移除并返回在 Stack 的顶部的对象。
        5	public virtual void Push( object obj );
        向 Stack 的顶部添加一个对象。
        6	public virtual object[] ToArray();
        复制 Stack 到一个新的数组中。
         */
        Stack<int> stk = new Stack<int>();
        stk.Push(11);
        stk.Push(22);
        stk.Push(33);
        stk.Push(44);
        stk.Push(55);
    }

    public static void testSortedList() {
        SortedList<int, string> sl = new SortedList<int, string>();
        sl.Add(4, "aaa");
        sl.Add(2, "bbb");
        sl.Add(3, "ccc");
        sl.Add(1, "ddd");

        foreach (var item in sl) {
            Console.WriteLine("key:{0}, value:{1}", item.Key, item.Value);
        }
        /*
key:1, value:ddd
key:2, value:bbb
key:3, value:ccc
key:4, value:aaa
         */
        // 输出的结果是有序的, 每一次 add 都会
    }

}