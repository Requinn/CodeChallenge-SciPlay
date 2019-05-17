using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SciPlay_CodeChallenge {
    /// <summary>
    /// Program to consolidate all living people per year given a START and END year range and a list of people and their BIRTH and DEATH years.
    /// * A Person is included into a yearly count if they have been born or died on that year. (A person who is born/dies in a given year has lived during atleast a part of that year.)
    /// * This is only accepting inputs between the accepted date range.
    /// * The date range is inclusive of start and end years.
    /// </summary>
    class Program {
        static void Main(string[] args) {
            List<Person> _people = new List<Person>();

            const int START_YEAR = 1900;
            const int END_YEAR = 2000;

            List<int> _yearCensus = new List<int>(new int[(END_YEAR - START_YEAR) + 1]); //initilized list with ints

            Console.WriteLine("Enter your datapoints in the following format on every new line: [Birth Year] [Death Year]\nWhen finished enter 0 0");

            while (true) {
                //read input
                string[] input = Console.ReadLine().Split();

                //catch input errors
                if(input.Length != 2 || input[0] == " " || input[1] == " " ) {
                    Console.WriteLine("Incorrect parameter count!");
                    continue;
                }

                //try parse dates for integer values
                int birth, death;       
                if (!int.TryParse(input[0], out birth) || !int.TryParse(input[1], out death)) {
                    Console.WriteLine("Integer values only!");
                    continue;
                }

                //check for finish input
                if (death == 0 && birth == 0) {
                    Console.WriteLine();
                    break;
                }

                //check for range validity if we have integer values
                if (birth > death || birth < START_YEAR || death > END_YEAR || death < START_YEAR || birth > END_YEAR) {
                    Console.WriteLine("Invalid Date Range");
                    continue;
                }

                _people.Add(new Person(birth, death));
            }

            //loop through each person and calculate their living periods
            foreach(Person person in _people) {
                int liveRange = person.deathYear - person.birthYear;
                //edge case for same year birth and death
                if(liveRange == 0) {
                    _yearCensus[person.birthYear - START_YEAR]++;
                    continue;
                }
                //increment the years they were alive
                for (int i = 0; i <= liveRange; i++) {
                    _yearCensus[(person.birthYear - START_YEAR) + i]++;
                }
            }

            //Console.WriteLine("Birth: " + _people[0].birthYear + " Death: " + _people[0].deathYear);
            for(int yearOffset = 0; yearOffset <= END_YEAR - START_YEAR; yearOffset++) {
                Console.Write(START_YEAR + yearOffset + ": " + _yearCensus[yearOffset] + " ");
                if(yearOffset % 10 == 0) {
                    Console.Write("\n");
                }
            }

            //Hold window open
            Console.WriteLine("\nPress Enter to close...");
            Console.ReadLine();
        }

    }

    /// <summary>
    /// Defines a person with birth and death years.
    /// </summary>
    public struct Person {
        public int birthYear;
        public int deathYear;

        public Person(int birth, int death) {
            birthYear = birth;
            deathYear = death;
        }
    }
}
