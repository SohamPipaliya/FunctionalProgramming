using System;
using static System.Console;
using NDCConference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionalProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            NDCCSharp myClass = new NDCCSharp(1, 22, "Soham", "Moviya");
            NDCCSharp myClass2 = new NDCCSharp(2, 20, "Yash", "Moviya");

            myClass.Swap();
            WriteLine(myClass.ID);
            WriteLine(myClass.Age);

            if (myClass) WriteLine("Yes Name Is - " + myClass.Name);

            myClass++;
            WriteLine(myClass.Age);
            myClass--;
            WriteLine(myClass.Age);

            NDCCSharp myClass3 = "Soham Patel";// or you can also give already created object
            WriteLine((string)myClass3);

            myClass3 = 22; // each time it return new new instance
            WriteLine((int)myClass3);

            WriteLine(myClass == myClass2);
            WriteLine(myClass != myClass2);

            WriteLine(myClass.Equals(1.0f)); // as object
            WriteLine(myClass.Equals(1)); // as class beacuse it auto converts int to NDCCSharp

            Span<int> span = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var newSpan = span.Slice(1, 2);

            foreach (var item in span[..]) // enables you apply index as range if no first or last arg not give it take first as element as first and last element as last. ex:- [0..10],[..10],[1..]. if you give first and last is not considered
            {
                WriteLine(item);
            }

            FP1();

            ReadLine();
        }

        static async void FP1() // can use Task instead of void
        {
            await foreach (var item in Csharp8.GetAsyncEnumerator())
            {
                WriteLine(item);
            }
        }
    }
}

namespace NDCConference
{
    class NDCCSharp : IEquatable<NDCCSharp>
    {
        public int ID { get; set; }
        public int Age { get; private set; }
        public string Name { get; private set; }
        public string Address { get; init; }

        private NDCCSharp(string Name) { this.Name = Name; }

        private NDCCSharp(int Age) { this.Age = Age; }

        public NDCCSharp(int pID, int pAge, string pName, string pAddress) => (ID, Age, Name, Address) = (pID, pAge, pName, pAddress);

        public static implicit operator string(NDCCSharp nDCCSharp) => nDCCSharp.Name;

        public static implicit operator int(NDCCSharp nDCCSharp) => nDCCSharp.Age;

        public static implicit operator bool(NDCCSharp nDCCSharp) => nDCCSharp.Name == "Soham";

        public static implicit operator NDCCSharp(string Name) => new NDCCSharp(Name);

        public static implicit operator NDCCSharp(int Age) => new NDCCSharp(Age); // anything can be applied prerequiste is that you have to be able to return that property.

        void foo(string str)
        {
            _ = str ?? throw new Exception(); // check wether string is null or not.
                                              // or 
            str = str ?? throw new Exception();
            WriteLine(str);
        }

        public void Swap() => (ID, Age) = (Age, ID);

        public static bool operator ==(NDCCSharp o1, NDCCSharp o2) => (o1.ID, o1.Age, o1.Address, o1.Name) == (o2.ID, o2.Age, o2.Address, o2.Name); // or any condition can be applied
        /*{
            return o1.ID == o2.ID && o1.Name == o2.Name && o1.Address == o2.Address && o1.Age == o2.Age;
        }*/

        public static bool operator !=(NDCCSharp o1, NDCCSharp o2) => (o1.ID, o1.Age, o1.Address, o1.Name) != (o2.ID, o2.Age, o2.Address, o2.Name);
        /*{
            return o1.ID != o2.ID || o1.Name != o2.Name || o1.Address != o2.Address || o1.Age != o2.Age;
        }*/

        public static NDCCSharp operator ++(NDCCSharp nDCCSharp)
        {
            nDCCSharp.Age++;
            return nDCCSharp;
        }

        public static NDCCSharp operator --(NDCCSharp nDCCSharp)
        {
            nDCCSharp.Age--;
            return nDCCSharp;
        }

        public override bool Equals(object? obj) => obj is NDCCSharp variable ? variable == this : false;

        public override int GetHashCode() => ID.GetHashCode() ^ Name.GetHashCode() ^ Address.GetHashCode();

        public virtual bool Equals(NDCCSharp other) => other == this;
    }

    class Csharp8 : IDisposable
    {
        public int MyProperty { get; set; }
        public void Dispose()
        {
            WriteLine("dispose called");
        }

        public static async IAsyncEnumerator<int> GetAsyncEnumerator()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return await Task.Delay(0).ContinueWith(_ => i);// to not to store in any variable use _
            }
        }
    }

    class CSharp9
    {
        // Improved Target Typing
        public List<List<int>> list = new() { new() { 1, 2, 3, 4, 5 }, new() { 6, 7, 8, 9, 10 } };

        //init property tat can only be set while instanciating class
        public int Value { get; init; }
        void foo()
        {
            CSharp9 obj = new() { Value = 10 };
            //not possible obj.Value = 10;
        }

        //Relational Pattern Matching 
        void foo2()
        {
            // use this instead of if else
            var msg = Value switch
            {
                int value when value <= 0 => "Less than or equal to 0",
                int value when value > 0 && value <= 10 => "More than 0 but less than or equal to 10",
                _ => "More than 10"
            };

            //also
            if (Value is > 0 and <= 10) Console.WriteLine("do something");
        }

        //Record type
        public static void foo3()
        {
            var obj = new Person("Soham", 5);
            var obj3 = obj with { Age = 10};
        }

        public record Person(string Name, int Age);// name anf age are properties with get and init accessor
    }

    static class Manager // these methods are automatically applied
    {
        public static IAsyncEnumerator<T> GetAsyncEnumerator<T>(this IAsyncEnumerator<T> enumerator) => enumerator;
        public static IEnumerator<T> GetEnumerator<T>(this IEnumerator<T> enumerator) => enumerator;
    }
}
