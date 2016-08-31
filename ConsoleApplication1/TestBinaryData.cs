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

        FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
        BinaryWriter writer = new BinaryWriter(fileStream);

        //--- write data
        writer.Write(123);
        string str = @"hello world";
        writer.Write(str);
        writer.Write((short)456);
        writer.Write(true);
        writer.Write(444.55f);
        writer.Flush();

        writer.Close();
        fileStream.Close();
    }

    public static void read()
    {
        string filepath = getPath();
        if (!File.Exists(filepath))
            return;

        FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(fileStream);

        //--- read data
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
        fileStream.Close();
    }

    //-----------------------

    public static string getPath222()
    {
        string dir = System.IO.Directory.GetCurrentDirectory();
        string filepath = dir + @"\dataSeed.bin";

        Console.WriteLine("--- dir:{0}", filepath);
        return filepath;
    }

    public static void write222()
    {
        string filepath = getPath222();
        if (File.Exists(filepath))
            File.Delete(filepath);

        FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
        MemoryStream memStream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(memStream);

        //--- write data
        writer.Write(123);
        string str = @"hello world";
        writer.Write(str);
        writer.Write((short)456);
        writer.Write(true);
        writer.Write(444.55f);
        memStream.Flush();

        byte[] bytes = memStream.GetBuffer();
        for (int i = 0; i < bytes.Length; ++i)
            bytes[i] = (byte)~bytes[i];

        MemoryStream memStream2 = new MemoryStream(bytes);
        memStream2.Flush();
        memStream2.WriteTo(fileStream);

        memStream2.Close();
        memStream.Close();
        writer.Close();
        fileStream.Close();
    }

    public static void read222()
    {
        string filepath = getPath222();
        if (!File.Exists(filepath))
            return;

        FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        int nBytes = (int)fs.Length;
        byte[] byteArray = new byte[nBytes];
        int nBytesRead = fs.Read(byteArray, 0, nBytes);

        for (int i = 0; i < nBytesRead; ++i)
            byteArray[i] = (byte)~byteArray[i];

        MemoryStream br = new MemoryStream(byteArray);
        BinaryReader reader = new BinaryReader(br);

        //--- read data
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
        br.Close();
        fs.Close();
    }

    /// <summary>
    /// no encode seed
    /// </summary>
    public static void test1()
    {
        //write();
        read();
    }
    /// <summary>
    /// has encode seed
    /// </summary>
    public static void test2()
    {
        //write222();
        read222();
    }

}
