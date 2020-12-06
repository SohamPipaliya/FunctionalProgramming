using System;
using static System.Console;
using NDCConference;

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
            ReadLine();
        }
    }
}

namespace NDCConference
{
    class NDCCSharp
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

        public static implicit operator NDCCSharp(string Name) => new NDCCSharp(Name);

        public static implicit operator NDCCSharp(int Age) => new NDCCSharp(Age); // anything can be applied prerequiste is that you have to have that field or property

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
    }
}
