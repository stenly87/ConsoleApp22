using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    class Program
    {
        static void Main(string[] args)
        {
            // Разработка расширений классов (class extension)
            Human human = new Human { Fname = "Bob" };
            human.SayHello(); // человек представляется и говорит hello
            File.WriteAllBytes("test.human", human.GetBinary());
        }
    }

    [Serializable]
    public class Human
    { 
        public string Fname { get; set; }
    }

    // создаем класс-расширение (extension) для класса Human
    // в виде расширения можно добавить только методы
    // метод-расширение будет иметь доступ
    // только к публичным элементам класса
    public static class HumanExtension
    {
        // метод-расширение обязан быть public static
        // первый аргумент this РасширяемыйТип название
        public static void SayHello(this Human obj)
        {
            Console.WriteLine($"Hello, my name is {obj.Fname}");
        }

        public static byte[] GetBinary(this Human obj)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            byte[] array;
            using (MemoryStream ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                array = new byte[ms.Length];
                ms.Read(array, 0, array.Length);
            }
            return array;
        }

    }

}
