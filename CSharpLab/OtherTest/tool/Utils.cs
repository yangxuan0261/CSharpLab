using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Utils {

    public static bool IsDirectoryExist(string path) {
        return System.IO.Directory.Exists(path);
    }

    public static bool CreateDirectory(string path) {
        bool result = true;
        try {
            System.IO.Directory.CreateDirectory(path);
        } catch (Exception e) {
            Console.WriteLine("CreateDirectory error, msg: {0}", e.Message);
            result = false;
        }
        return result;
    }

    public static string BytesToUTF8(byte[] bytes) {
        return Encoding.UTF8.GetString(bytes);
    }

    public static byte[] UTF8ToBytes(string str) {
        return Encoding.UTF8.GetBytes(str);
    }

    public static byte[] ReadAllBytesFromFile(string path) {
        return System.IO.File.ReadAllBytes(path);
    }

    public static Exception WriteFile(string path, byte[] content) {
        string dirPath = System.IO.Path.GetDirectoryName(path);
        if (!IsDirectoryExist(dirPath)) {
            CreateDirectory(dirPath);
        }

        FileStream fs = null;
        try {
            fs = new FileStream(path, FileMode.Create);
            fs.Write(content, 0, content.Length);
        } catch (System.Exception ex) {
            return ex;
        } finally {
            if (fs != null) {
                fs.Close();
            }
        }
        return null;
    }

    public static string BeautyJson(object obj, bool isPretty = true) {
        LitJson.JsonWriter writer = new LitJson.JsonWriter();
        writer.PrettyPrint = isPretty;
        LitJson.JsonMapper.ToJson(obj, writer);
        return writer.TextWriter.ToString();
    }

    public static void WriteObj2Json(string path, object obj, bool isPretty = true) {
        LitJson.JsonWriter writer = new LitJson.JsonWriter();
        writer.PrettyPrint = isPretty;
        LitJson.JsonMapper.ToJson(obj, writer);
        File.WriteAllText(path, writer.TextWriter.ToString());
    }

    public static T ReadJson<T>(string path) {
        byte[] content = Utils.ReadAllBytesFromFile(path);
        string str = System.Text.Encoding.UTF8.GetString(content);
        return LitJson.JsonMapper.ToObject<T>(str);
    }

    public static string GetDesktop(params string[] paths) {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        if (paths == null || paths.Length == 0) {
            return Join(desktop);
        } else {
            string relaPath = Path.Combine(paths);
            return Join(desktop, relaPath);
        }
    }

    // 返回绝对路径
    public static string Join(params string[] paths) {
        return Path.GetFullPath(Path.Combine(paths)).Replace("\\", "/");
    }
}