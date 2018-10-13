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

    void test1() {
        bool b = 1 < 3 ? true : false;
        Console.WriteLine("hello{0} world{1}", 123, b); //hello123 worldTrue
        Console.WriteLine("{0}", 1 << 2); //4
        Console.WriteLine("{0}", (int)TestBase.EM_aaa.D);//3 

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

    public static void test2() {
        uint aaa = 123;
        string bbb = aaa.ToString();
        
        Console.WriteLine("--- uint to string:{0}", bbb);
    }
}
