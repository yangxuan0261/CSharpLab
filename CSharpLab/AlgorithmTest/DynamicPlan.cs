using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 动态规划
class DynamicPlan {

    private const int MAX_STRING_LEN = 6;


    // 参考 <<算法的乐趣>> 随书源码 code03\dp\dp.cpp
    private int EditDistance3(char[] src, char[] dest) {
    // int i,j;


    int[,] matrix = new int[MAX_STRING_LEN,MAX_STRING_LEN]; 

    // for(i = 0; i <= src.Length; i++)
    //     d[i][0] = i;
    // for(j = 0; j <= dest.Length; j++)
    //     d[0][j] = j;


        return 0;
    }

    public static void test1() {
        DynamicPlan dp = new DynamicPlan();
        dp.EditDistance3(null, null);
    }
}
