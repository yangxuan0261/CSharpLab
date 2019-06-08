> vscode 跑这个工程的姿势, 必须要有 *dotnet* 环境, 因为是通过 *dotnet* 命令执行编译的.
>
> - 添加一个自定义类. 
>
>     比如 *OtherTest* 目录下添加 *aaa.cs*, 然后必须在 *CSharpLab.csproj* 中添加一行编译包含项 `    <Compile Include="OtherTest\TestClass.cs" />`
>
>     最后按 *f5* 编译 exe 并启动 exe