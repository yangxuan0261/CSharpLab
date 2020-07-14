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
}