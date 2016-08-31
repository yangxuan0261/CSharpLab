using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class TestBinaryData
{
    public static string getPath() 
    {
        string dir = System.IO.Directory.GetCurrentDirectory();
        string filepath = dir + @"\data.bin";

        Console.WriteLine("--- dir:{0}", filepath);
        return filepath;
    }

    public static void write()
    {
        string filepath = getPath();
        if (File.Exists(filepath))
            File.Delete(filepath);

        FileStream fileStream = new FileStream(filepath, FileMode.Create);
        MemoryStream memStream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(memStream);
        writer.Write(123);
        string str = @"hello world";
        writer.Write(str);
        writer.Write((short)456);
        writer.Write(true);
        writer.Write(444.55f);
        memStream.Flush();
        byte[] bytes = memStream.GetBuffer();
        for (int i = 0; i < bytes.Length; ++i)
        {
            bytes[i] ^= bytes[i]; 
        }


        MemoryStream memStream2 = new MemoryStream(bytes);
        memStream2.WriteTo(fileStream);
        memStream2.Close();

        memStream.Close();
        writer.Close();
        fileStream.Close();
    }

    public static void read()
    {
        string filepath = getPath();
        if (!File.Exists(filepath))
            return;

        FileStream fs = new FileStream(filepath, FileMode.Open);
        BinaryReader reader = new BinaryReader(fs);
        int num = reader.ReadInt32();
        string str = reader.ReadString();
        int num2 = reader.ReadInt16();
        bool b = reader.ReadBoolean();
        float time = reader.ReadSingle();

        Console.WriteLine("--- num:{0}", num);
        Console.WriteLine("--- str:{0}", str);
        Console.WriteLine("--- num2:{0}", num2);
        Console.WriteLine("--- b:{0}", b);
        Console.WriteLine("--- time:{0}", time);

        reader.Close();
        fs.Close();
    }

    public static void test1()
    {
        write();
        //read();
    }
}
