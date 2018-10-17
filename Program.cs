using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        static bool Loop(List<Hashtable> data)
        {
            int keyNum = 0;
            string whatInfo;
            while(true)
            {
                try
                {
                    System.Console.WriteLine("Input a number(1-{0})",data[0].Count);
                    keyNum = int.Parse(Console.ReadLine());
                    if(keyNum >= 1 || keyNum <= data[0].Count)
                        break;
                    System.Console.WriteLine("That Data does not exist");

                }
                catch(FormatException)
                {
                    System.Console.WriteLine("That is not a number");
                }
            }
            while(true)
            {
                System.Console.WriteLine("{0} is {1}, what do you want to know? ({2} / {3})",keyNum,data[0][keyNum],"Hometown","Favorite Food");
                whatInfo = Console.ReadLine();
                if(Regex.IsMatch(whatInfo.ToLower(),"home"))
                {
                    System.Console.WriteLine("{0}'s hometown is {1}",data[0][keyNum],data[1][keyNum]);
                }
                else if(Regex.IsMatch(whatInfo.ToLower(),"food"))
                {
                    System.Console.WriteLine("{0}'s favorite food is {1}",data[0][keyNum],data[2][keyNum]);
                }
                else
                {
                    System.Console.WriteLine("Sorry, I do not have that data about {0}",data[0][keyNum]);
                }
                whatInfo = "";
                while(!Regex.IsMatch(whatInfo,"^[yYNn]"))
                {
                    System.Console.WriteLine("Do you want to know more about {0}?(Yes/No)",data[0][keyNum]);
                    whatInfo = Console.ReadLine();
                }
                if(Regex.IsMatch(whatInfo,"^[nN]"))
                    break;
            }
            whatInfo = "";
            while(!Regex.IsMatch(whatInfo,"^[yYNn]"))
            {
                System.Console.WriteLine("Do you want to know about another student?(Yes/No)");
                whatInfo = Console.ReadLine();
            }
            if(Regex.IsMatch(whatInfo,"^[nN]"))
                return false;
            return true;
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
                File = new StreamReader(Console.OpenStandardInput()); // This doesn't work right now
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
            //DisplayAllPeopleAndInfo(people);
            while (Loop(people));

        }
    }
}
