using System;
using System.Collections.Generic;
using System.Linq;

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

            List<string> possibleTypes = new List<string>();


            string input = Console.ReadLine();

            List<string> inputs = input.Split(' ').ToList();

            inputs = inputs.Where(x => x.ToLower() != "room" || x.ToLower() != "space").ToList();

            foreach(var thing in inputs)
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
            foreach (var item in new HashSet<string>(possibleTypes))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------------------------");

            Console.ReadLine();


        }
    }
}
