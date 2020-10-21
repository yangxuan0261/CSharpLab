using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//delegate

public class TestBase {
    enum EM_aaa : byte {
        A = 0,
        B,
        C,
        D,
    }

    #region test fold the code

    static void test1() {
        bool b = 1 < 3 ? true : false;
        Console.WriteLine("hello{0} world{1}", 123, b); //hello123 worldTrue
        Console.WriteLine("{0}", 1 << 2); //4
        Console.WriteLine("{0}", (int) TestBase.EM_aaa.D); //3 

        int[] iArr = new int[5] { 1, 2, 3, 4, 5 };
        //for (int i = 0; i < iArr.Length; ++i)
        //Console.WriteLine(iArr[i]);

        string myStr = "asd,qwe,zxc";
        string[] strArr = myStr.Split(",".ToCharArray());
        Console.WriteLine(" strArr size:{0}", strArr.Length); //3
        foreach (string temp in strArr)
            Console.WriteLine("{0}", temp);

    }
    #endregion

    static void test2() {
        uint aaa = 123;
        string bbb = aaa.ToString();

        Console.WriteLine("--- uint to string:{0}", bbb);
    }

    static void test_bit() {
        int total = 11; // 1011
        int mail = 2;
        int mailDst = 1 << (mail - 1);
        int finalDst = mailDst & total;
        Console.WriteLine("--- mailDst:{0}", mailDst);
        Console.WriteLine("--- finalDst:{0}", finalDst);
    }

    public enum EAct : int {
        Walk = 1,
        Run = 2,
        Swim = 3,
    }

    public static class EnumUtil {
        public static IEnumerable<T> GetValues<T>() {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    static void test_enum() {
        // enum -> string, enum -> int
        EAct at1 = EAct.Run;
        Console.WriteLine(string.Format("--- at1: {0}, val: {1}", at1.ToString(), (int) at1));

        Enum at2 = at1;
        Console.WriteLine(string.Format("--- at2: {0}, val: {1}", at2.ToString(), Convert.ToInt32(at2))); // Enum 只能用 Convert 强转

        // string -> enum, 转不出来会报异常
        string name = "Swim";
        try {
            EAct at3 = (EAct) Enum.Parse(typeof(EAct), name);
            Console.WriteLine(string.Format("--- at3: {0}, val: {1}", at3.ToString(), (int) at3));
        } catch (System.Exception e) {
            Console.WriteLine(e);
        }

        // int -> enum
        int num = 1;
        EAct at4 = (EAct) num;
        Console.WriteLine(string.Format("--- at4: {0}, val: {1}", at4.ToString(), (int) at4));

        //  EnumUtil.GetValues<EAct>();

        foreach (EAct item in Enum.GetValues(typeof(EAct))) {
            Console.WriteLine(string.Format("--- val: {0}", (int) item));
        }

        foreach (string value in Enum.GetNames(typeof(EAct))) {
            Console.WriteLine(string.Format("--- name: {0}", value));
        }

        // enum to array or list
        List<int> intLst = Enum.GetValues(typeof(EAct)).Cast<int>().ToList();
        foreach (int value in intLst) {
            Console.WriteLine(string.Format("--- intLst: {0}", value));
        }
    }

    static void test_random() {
        Random rnd = new Random(System.Guid.NewGuid().ToString().GetHashCode());
        for (int i = 0; i < 10; i++) {
            int num = rnd.Next(1, 3); // 区间: [1, 3)
            Console.WriteLine(string.Format("--- num: {0}", num));
        }
    }

    public static void main() {
        // test1();
        // test2();
        // test_bit();
        // test_enum();
        test_random();
    }

}