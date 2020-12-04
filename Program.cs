using System;
using static System.Console;

namespace FunctionalProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            NDCCSharp myClass = new NDCCSharp(10, 20, 30);
            NDCCSharp myClass2 = new NDCCSharp(20, 10, 30);

            Console.WriteLine(myClass.MyProperty);
            Console.WriteLine(myClass.MyProperty2);
            Console.WriteLine(myClass.MyProperty3);

            Console.WriteLine("-->");
            myClass.swap();

            Console.WriteLine(myClass.MyProperty);
            Console.WriteLine(myClass.MyProperty2);
            Console.WriteLine(myClass.MyProperty3);

            Console.WriteLine("-->");
            myClass.MyProperty = 50;
            myClass2.MyProperty = 50;

            Console.WriteLine(myClass.GetHashCode());
            Console.WriteLine(myClass2.GetHashCode());

            string str = myClass;
            NDCCSharp nDCCSharp = str;
            ReadLine();
        }
    }

    class NDCCSharp
    {
        public int MyProperty { get; set; }
        public int MyProperty2 { get; set; }
        public int MyProperty3 { get; set; }

        public NDCCSharp(string s) { }

        public NDCCSharp(int p, int p2, int p3) => (MyProperty, MyProperty2, MyProperty3) = (p, p2, p3);

        public static bool operator ==(NDCCSharp o1, NDCCSharp o2) => (o1.MyProperty, o1.MyProperty, o1.MyProperty3) == (o2.MyProperty, o2.MyProperty, o2.MyProperty3);

        public static bool operator !=(NDCCSharp o1, NDCCSharp o2) => (o1.MyProperty, o1.MyProperty, o1.MyProperty3) != (o2.MyProperty, o2.MyProperty, o2.MyProperty3);

        public static implicit operator string(NDCCSharp nDCCSharp) => nDCCSharp.MyProperty.ToString();

        public static implicit operator NDCCSharp(string sr) => new NDCCSharp(sr);

        void foo(string str)
        {
            _ = str ?? throw new Exception();
            Console.WriteLine(str);
        }

        public void swap() => (MyProperty, MyProperty2) = (MyProperty2, MyProperty);

        public override bool Equals(object obj) => obj is NDCCSharp variable ? variable == this : false;

        public override int GetHashCode() => MyProperty.GetHashCode() ^ MyProperty2.GetHashCode() ^ MyProperty3.GetHashCode();
    }
}
