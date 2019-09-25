using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

class TestAsync {
    // 参考:
    // https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/async/
    // https://www.jianshu.com/p/4768c954a85f

    private static async Task<int> async11() {
        Console.WriteLine(string.Format("--- async11 111, CurrentThread:{0}", Thread.CurrentThread.ManagedThreadId));
        var res = await async22();
        Console.WriteLine("--- async11 222, res:{0}", res);
        return 1;
    }

    private static async Task<int> async22() {
        Console.WriteLine(string.Format("--- async22 111, CurrentThread:{0}", Thread.CurrentThread.ManagedThreadId));
        await Task.Delay(2000);
        Console.WriteLine(string.Format("--- async22 222, CurrentThread:{0}", Thread.CurrentThread.ManagedThreadId));
        return 123;
    }

    private static async Task<int> async33() {
        Console.WriteLine(string.Format("--- async33 111, CurrentThread:{0}", Thread.CurrentThread.ManagedThreadId));
        await Task.Delay(4000);
        Console.WriteLine(string.Format("--- async33 222, CurrentThread:{0}", Thread.CurrentThread.ManagedThreadId));
        return 123;
    }

    public static async Task<int> test1() {
        // Task<int> task11 = async11();
        Task<int> task22 = async22();
        Task<int> task33 = async33();
        var allTasks = new List<Task> { task22, task33 };
        // async11();
        Console.WriteLine("");
        Console.WriteLine(string.Format("--- test 111, CurrentThread:{0}", Thread.CurrentThread.ManagedThreadId));
        await Task.Delay(2000);

        // await Task.WhenAny(allTasks); // 只要有个 task 返回
        await Task.WhenAll(allTasks.ToArray()); // 等待所有的返回

        Console.WriteLine(string.Format("--- test 222, CurrentThread:{0}", Thread.CurrentThread.ManagedThreadId));

        return 1;
    }
}