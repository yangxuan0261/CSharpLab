using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestIO {

    static int counter = 0;

    public static string GetFileName(string filePath) {
        string fileName = GetDirName(filePath);
        return fileName == "" ? "" : fileName.Substring(0, fileName.LastIndexOf(".md"));
    }

    public static string GetDirName(string dirPath) {
        int index = dirPath.LastIndexOf("\\");
        if (index == -1)
            return "";
        else
            return dirPath.Substring(index + 1);
    }

    public static string GetTemplate(string fileName, string dirName) {
        string time = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        StringBuilder sb = new StringBuilder();
        sb.Append("---\r\n");
        sb.Append("title: {0}\r\n");
        sb.Append("categories: {1}\r\n");
        sb.Append("tags: [{2}, {3}]\r\n");
        sb.Append("date: {4}\r\n");
        sb.Append("comments: false\r\n");
        sb.Append("---\r\n");
        sb.Append("-> <!-- more -->\r\n\r\n");
        return string.Format(sb.ToString(), fileName, dirName, "defalutTag111", "defalutTag222", time);
    }

    public static void RecurDir(string dirPath) {
        DealDir(dirPath);

        string[] dires = System.IO.Directory.GetDirectories(dirPath);
        foreach (string dir in dires) {
            RecurDir(dir);
        }
    }

    public static void DealDir(string dirPath) {
        string[] files = System.IO.Directory.GetFiles(dirPath, "*", System.IO.SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".md")).ToArray();
        foreach (string filePath in files) {
            ModifyFile(filePath, dirPath);
        }
    }

    public static void ModifyFile(string filePath, string dirPath) {
        ++counter;
        string dirName = GetDirName(dirPath);
        string fileName = GetFileName(filePath);
        string value = GetTemplate(fileName, dirName);
        //Console.WriteLine("--- fileName:{0}", fileName);

        string src = Read(filePath);
        string dst = value + src;
        Write(filePath, dst);
    }

    public static string Read(string filePath) {
        return File.ReadAllText(filePath, Encoding.UTF8);
    }

    public static void Write(string filePath, string content) {
        FileStream fs = new FileStream(filePath, FileMode.Open);
        StreamWriter sw = new StreamWriter(fs);
        sw.Write(content);
        sw.Flush();
        sw.Close();
        fs.Close();
    }

    public static void test_GetFiles() {
        string[] files = Directory.GetFiles("E:/its_rummy/Document/pack_config", "a_packarg_outer_*.json", SearchOption.TopDirectoryOnly);
        Console.WriteLine("--- len: " + files.Length);
        foreach (string item in files) {
            Console.WriteLine("--- item: " + item);
        }
    }

    public static void test_copyFile() {
        string path1 = "C:/Users/wolegequ/Desktop/a_temp.lua";
        string path2 = "C:/Users/wolegequ/Desktop/a_temp222.lua";
        File.Copy(path1, path2, true);

        File.Move(path2, path2 + ".bytes");
    }

    public static void main() {
        // test_GetFiles();
        // test_copyFile();
        // string selDir = "D:\\z_mywiki\\a_csdn_blog";
        // RecurDir(selDir);
        // Console.WriteLine("--- modify files:{0}", counter);

        // string path = "E:/its_rummy/Assets/../z_package\\debug\\rmg_rummy_station_8-3-2_v0.16.6.5_1_20200605_115936.apk";
        // Console.WriteLine("filename: " + Path.GetFileName(path));

        string d2 = " E:/its_rummy/Channel/8-3-2/Android/res/drawable";
        Console.WriteLine("--- ok: {0}", System.IO.Directory.Exists(d2));

    }
}