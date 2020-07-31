using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class TestJson {

    public class CActor {
        public string name;
        public int age;
    }

    private static string str1;

    static TestJson() {
        CActor a1 = new CActor();
        a1.name = "hello";
        a1.age = 123;
        str1 = BeautyJson(a1, false);
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

    public static void test_beauty() {
        CActor a1 = new CActor();
        a1.name = "hello";
        a1.age = 123;
        Console.WriteLine("--- json 111:{0}", BeautyJson(a1, true));
    }

    public static void test_jsonToMap() {
        Dictionary<string, string> testDic = new Dictionary<string, string>();
        testDic.Add("aa", "123");
        testDic.Add("bb", "sss");
        string str2 = BeautyJson(testDic, false);
        Console.WriteLine("--- json 111:{0}", str2);

        var map2 = LitJson.JsonMapper.ToObject<Dictionary<string, string>>(str2);
        foreach (var item in map2) {
            Console.WriteLine("--- key: {0}, value: {1}", item.Key, item.Value);
        }
    }

    public static void main() {
        // test_beauty();
        test_jsonToMap();
    }
}