using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using FuzzySharpTest;

namespace FuzzySharpTest
{
    internal class Program
    {
        private static readonly List<string> restRoomWords = new List<string>() {
                              "Restroom",
                              "Lavatory",
                              "Washroom",
                              "Bathroom",
                              "Facility",
                              "Toilet",
                              "John",
                              "Loo",
                              "Powder room",
                              "Latrine"
                            };

        private static readonly List<string> openOfficeWords = new List<string>() {
                                      "Open plan",
                                      "Open concept",
                                      "Collaborative",
                                      "Shared workspace",
                                      "Co-working",
                                      "Hot desk",
                                      "Flexible seating",
                                      "Activity-based working",
                                      "Agile working",
                                      "Team space"
                                    };

        private static readonly List<string> backOfHouseSpaces = new List<string>() {
                                      "Mechanical room",
                                      "Boiler room",
                                      "Elevator machine room",
                                      "Electrical room",
                                      "Telecommunications room",
                                      "Server room",
                                      "Storage room",
                                      "Utility room",
                                      "Janitorial closet",
                                      "Supply room",
                                      "Linen room",
                                      "Housekeeping closet",
                                      "Laundry room",
                                      "Loading dock",
                                      "Garage"
                                    };


        static void Main(string[] args)
        {

            Dictionary<string, List<string>> spaceTypes = new Dictionary<string, List<string>>
            {
                { "Restroom", restRoomWords },
                { "Open Office",  openOfficeWords},
                { "Mechanical Room",  backOfHouseSpaces}
            };

           HashSet<string> possibleTypes = new HashSet<string>();


            string input = Console.ReadLine();

            List<string> inputs = input.Split(' ').ToList();

            //inputs = inputs.Where(x => x.ToLower() != "room" || x.ToLower() != "space").ToList();

            inputs = inputs.RemoveSpecialWords(new string[2] { "room", "space" });

            foreach (var thing in inputs)
            {
                foreach (var spaceType in spaceTypes)
                {
                    bool isPossibleMatch = false;

                    var results = FuzzySharp.Process.ExtractAll(thing, spaceType.Value).Where(x => x.Score > 70).ToList();

                    if (results.Count() > 0)
                    {
                        isPossibleMatch = true;
                    }

                    if (isPossibleMatch)
                    {
                        possibleTypes.Add(spaceType.Key);
                    }
                }

            }

            
            Console.WriteLine("---------------------------");
            Console.WriteLine("Possible choices are:");
            foreach (var item in possibleTypes)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------------------------");

            Console.ReadLine();


        }
                
    }

    static class Compute
    {
        public static List<string> RemoveSpecialWords(this List<string> words, string[] wordsToRemove)
        {
            List<string> result = new List<string>();
            result = words.Where(x => wordsToRemove.Contains(x.ToLower()) == false).ToList();

            return result;
        }

        public static string RemoveSpecialCharacters(this string word, char[] charsToRemove)
        {
            
            string result = "";

            foreach (var ch in charsToRemove)
            {
                result = word.Replace(ch.ToString(), string.Empty);
            }

            return result;
        }

    }
}
