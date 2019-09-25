### 配置 .net 环境

vscode 跑这个工程的姿势, 必须要有 *dotnet* 环境, 因为是通过 *dotnet* 命令执行编译的.

1. 如果已安装 .net, 查看本机 .net 版本. 参考: 获取机器安装.NET版本的几种方式 - https://www.cnblogs.com/gaochundong/p/how_to_determine_which_net_framework_versions_are_installed.html

    比如通过控制面板查询

    ![](http://yxbl.itengshe.com/20190925170907-1.png)

2. 设置工程的 .net 版本. 编辑 *CSharpLab.csproj* 文件

    ```xml
    <TargetFrameworkVersion>v4.7.1</TargetFrameworkVersion>
    ```

3. done.



---

### 测试

1. 在 *OtherTest* 目录下添加 添加一个自定义文件 *TestAsync.cs*. 

    ```csharp
    class TestAsync {
        public static void test1() {
            Console.WriteLine("--- TestAsync");
        }
    }
    ```

2. 在项目配置文件 *CSharpLab.csproj* 中增加此文件, 

    ```xml
        <Compile Include="OtherTest\TestAsync.cs" />
    ```

3. 在 *a_main.cs* 修改执行入口

    ```csharp
    static void Main(string[] args) {
        ...
        TestAsync.test1();
        ...
    }
    ```

4. 执行.

    1. 直接运行 exe ( 建议 ). 按 *ctrl + shift + b*

        ![1569403595198](C:\Users\wolegequ\AppData\Roaming\Typora\typora-user-images\1569403595198.png)

    2. 调试执行 ( 不太建议 ). 按 *f5*. 多线程时可能会执行不出来

        ![](http://yxbl.itengshe.com/20190925172721-1.png)

        异步代码没有执行出来, 不知道为啥.