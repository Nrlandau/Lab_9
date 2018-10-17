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
        static string[] GetPersonFromInput(StreamReader sr)
        {
            string [] personInfo = new string[NUMBEROFINFOPOINTS];
            for(int i =0; i < NUMBEROFINFOPOINTS; i++)
            {
                System.Console.WriteLine("INPUT A THING or -1 to quit");
                if((personInfo[i] = sr.ReadLine()) == "-1")
                    throw new EndOfStreamException();
            }
            return personInfo;
        }
        static void DisplayAllPeopleAndInfo(List<Hashtable> AllInfo)
        {
            for(int key = 1; key < AllInfo[0].Count + 1; key++)
            {
                for(int i =0; i < NUMBEROFINFOPOINTS; i++)
                {
                    System.Console.Write (AllInfo[i][key] + " ");
                }
                System.Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            List<Hashtable> people = CreatePeople();
            StreamReader File;// = new StreamReader(Console.OpenStandardInput());
            try
            {
                File = new StreamReader("people.txt");
            }
            catch (FileNotFoundException)
            {
                File = new StreamReader(Console.OpenStandardInput());
                System.Console.WriteLine("File Does not exist, Using Console input.");
            }
            try{
                while (true)
                AddPerson(people,GetPersonFromFile(File));
            }
            catch (EndOfStreamException)
            {
                if(people[0].Count == 0)
                {
                    System.Console.WriteLine("No Info Loaded");
                    throw new EndOfStreamException();
                }
                
            }
            finally
            {
               File.Close(); 
            }
            DisplayAllPeopleAndInfo(people);
        }
    }
}
