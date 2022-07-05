using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

class TestFile {

    // 文件合并
    private static void TestMergeFile() {
        Action<List<string>, string> Combine = (List<string> files, string fullName) => {
            byte[] buffer = new byte[1024 * 100];
            using(FileStream outStream = new FileStream(fullName, FileMode.Create)) {
                int readedLen = 0;
                FileStream srcStream = null;
                for (int i = 0; i < files.Count; i++) {
                    srcStream = new FileStream(files[i], FileMode.Open);
                    while ((readedLen = srcStream.Read(buffer, 0, buffer.Length)) > 0) {
                        outStream.Write(buffer, 0, readedLen);
                    }
                    srcStream.Close();
                }
            }
        };

        // 测试代码
        List<string> fileArr = new List<string>() {
            Utils.GetDesktop("aaa_1.txt"),
            Utils.GetDesktop("aaa_2.txt"),
        };
        string output = Utils.GetDesktop("aaa_out.txt");
        Combine(fileArr, output);
        Console.WriteLine("--- success");
    }

    // 大文件拷贝
    private static void TestBigFileCopy() {
        // https://blog.csdn.net/kone0611/article/details/51801119
        Func<string, string, int, bool> CopyFile = (string fromPath, string toPath, int buffSize) => {
            FileStream fromFile = null;
            FileStream toFile = null;
            try {
                fromFile = new FileStream(fromPath, FileMode.Open, FileAccess.Read);
                toFile = new FileStream(toPath, FileMode.Append, FileAccess.Write);
                //如果每次读取的长度小于 源文件的长度 分段读取
                if (buffSize < fromFile.Length) {
                    long copied = 0;
                    byte[] buffer = new byte[buffSize];

                    // 剩余长度 >= buffSize, 就读取 buffSize 大小的流
                    while (copied + buffSize <= fromFile.Length) {
                        int readSize = fromFile.Read(buffer, 0, buffSize);
                        fromFile.Flush();
                        toFile.Write(buffer, 0, buffSize);
                        toFile.Flush();

                        //流的当前位置
                        toFile.Position = fromFile.Position;
                        copied += readSize;

                        // Console.WriteLine("--- copied: {0}, from len: {1}, num: {2} ", copied, fromFile.Length, fromFile.Length - buffSize);
                        // Thread.Sleep(1000 * 1); // TODO: aaa 测试, 方便看效果
                    }

                    // 剩余长度 < buffSize, 直接读剩下的长度
                    int left = (int) (fromFile.Length - copied);
                    fromFile.Read(buffer, 0, left);
                    fromFile.Flush();
                    toFile.Write(buffer, 0, left);
                    toFile.Flush();
                } else {
                    byte[] buffer = new byte[fromFile.Length];
                    fromFile.Read(buffer, 0, buffer.Length);
                    fromFile.Flush();
                    toFile.Write(buffer, 0, buffer.Length);
                    toFile.Flush();
                }
                return true;
            } catch (System.Exception) {
                return false;
            } finally {
                if (fromFile != null)
                    fromFile.Close();
                if (toFile != null)
                    toFile.Close();
                // Console.WriteLine("--- finally");
            }
        };

        // 测试代码
        string srcFile = Utils.GetDesktop("base-1.apk");
        string dstFile = Utils.GetDesktop("base-2.apk");

        bool isOk = CopyFile(srcFile, dstFile, 1024 * 1024 * 1);
        Console.WriteLine("--- isOk: {0}", isOk);
    }

    public static void main() {
        // TestMergeFile();
        TestBigFileCopy();
    }

}