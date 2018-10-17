using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Lab9
{

    class Program
    {
        const int NUMBEROFINFOPOINTS = 3;
        static void AddPerson(List<Hashtable> People, string [] info)
        {
            for(int i = 0; i < NUMBEROFINFOPOINTS; i++)
                People[i].Add(People[i].Count+1,info[i]);
        }
        static List<Hashtable> CreatePeople()
        {
             List<Hashtable> allPeople = new List<Hashtable>();
             for(int i = 0; i < NUMBEROFINFOPOINTS; i++)
                allPeople.Add(new Hashtable());
             return allPeople;
        }
        static string[] GetPersonFromFile(StreamReader sr)
        {
            string [] personInfo = new string[NUMBEROFINFOPOINTS];
            for(int i =0; i < NUMBEROFINFOPOINTS; i++)
            {
                if((personInfo[i] = sr.ReadLine()) == null)
                    throw new EndOfStreamException();
            }
            return personInfo;

        }

        static void Main(string[] args)
        {
            StreamReader File = new StreamReader("people.txt");
            List<Hashtable> people = CreatePeople();
            try
            {
            while (true)
                AddPerson(people,GetPersonFromFile(File));
            }
            catch (EndOfStreamException)
            {
                if(people[0].Count == 0)
                {
                    System.Console.WriteLine("No Info Loaded");
                    //throw new EndOfStreamException();
                }
            }

        }
    }
}
