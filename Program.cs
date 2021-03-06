﻿/*
Lab 9 by Nicholas Landau
This program does a thing.
 */

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lab9
{

    class Program
    {
        const int NUMBEROFINFOPOINTS = 3; //Name,Home,Food
        static void AddPerson(List<Hashtable> People, string [] info) // adds one person and their info into the list.
        {
            for(int i = 0; i < NUMBEROFINFOPOINTS; i++)
                People[i].Add(People[i].Count+1,info[i]);
        }
        static List<Hashtable> CreatePeople() // initlises the List
        {
             List<Hashtable> allPeople = new List<Hashtable>();
             for(int i = 0; i < NUMBEROFINFOPOINTS; i++)
                allPeople.Add(new Hashtable());
             return allPeople;
        }
        static string[] GetPersonFromFile(StreamReader sr) // gets name/hometwon/favorite food from file.
        {
            string [] personInfo = new string[NUMBEROFINFOPOINTS];
            for(int i =0; i < NUMBEROFINFOPOINTS; i++)
            {
                if((personInfo[i] = sr.ReadLine()) == null)
                    throw new EndOfStreamException();
            }
            return personInfo;
        }
        static void ConsoleInput(List<Hashtable> data) // main loop for user input into the list.
        {
            string cont = "";
            do
            {
                AddPerson(data,GetPersonFromInput());
                cont =  "";
                while(!Regex.IsMatch(cont,"^[nNYy]"))
                {
                System.Console.WriteLine("Enter another name?");
                cont = Console.ReadLine();
                }
                
            }while(!Regex.IsMatch(cont,"^[nN]"));
        }
        static string[] GetPersonFromInput() // for user input into the list
        {
            string [] personInfo = {"name", "hometown", "favorite food"};
            for(int i =0; i < NUMBEROFINFOPOINTS; i++)
            {
                System.Console.WriteLine("Input a {0}" , personInfo[i]);
                personInfo[i] = Console.ReadLine();
            }
            return personInfo;
        }
        
        static void DisplayAllPeopleAndInfo(List<Hashtable> AllInfo) //unused  displays everying in the list.
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
            while(true) // get user key
            {
                try
                {
                    System.Console.WriteLine("Input a number(1-{0})",data[0].Count);
                    keyNum = int.Parse(Console.ReadLine());
                    if(keyNum >= 1 && keyNum <= data[0].Count)
                        break;
                    System.Console.WriteLine("That Data does not exist");

                }
                catch(FormatException)
                {
                    System.Console.WriteLine("That is not a number");
                }
            }
            while(true) //get info from one user
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
                while(!Regex.IsMatch(whatInfo,"^[yYNn]")) //exit single user
                {
                    System.Console.WriteLine("Do you want to know more about {0}?(Yes/No)",data[0][keyNum]);
                    whatInfo = Console.ReadLine();
                }
                if(Regex.IsMatch(whatInfo,"^[nN]"))
                    break;
            }
            whatInfo = "";
            while(!Regex.IsMatch(whatInfo,"^[yYNn]")) // exit program
            {
                System.Console.WriteLine("Do you want to know about another user?(Yes/No)");
                whatInfo = Console.ReadLine();
            }
            if(Regex.IsMatch(whatInfo,"^[nN]"))
                return false;
            return true;
        }

        static void Main(string[] args)
        {
            List<Hashtable> people = CreatePeople();
            StreamReader File;
            try
            {
                System.Console.WriteLine("Input a file"); // getting file input

                File = new StreamReader(Console.ReadLine());
                
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
            }
            // If The file input fails use Console input.
            catch (FileNotFoundException)
            {
                
                System.Console.WriteLine("File Does not exist, Using Console input.");
                ConsoleInput(people);
            }
            catch(UnauthorizedAccessException)
            {
                
                System.Console.WriteLine("You do no have perission to use that file, Using Console input.");
                ConsoleInput(people);
            }
            catch(ArgumentException)
            {
                
                System.Console.WriteLine("No File inputed, Using Console input.");
                ConsoleInput(people);
            }
            while (Loop(people)); // main loop.

        }
    }
}
