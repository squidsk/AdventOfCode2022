/*
 * User: skalmar
 * Date: 1/24/2023
 * Time: 3:06 PM
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace Day11
{
    class Monkey {
        List<int> items;
        int trueMonkey;
        int falseMonkey;
        int divisor;
        char operation;
        int operand;
        
        public Monkey(int trueMonkey, int falseMonkey, int divisor, char operation, int operand, int[] items) {
            this.items = new List<int>();
            this.trueMonkey = trueMonkey;
            this.falseMonkey = falseMonkey;
            this.divisor = divisor;
            this.operation = operation;
            this.operand = operand;
            this.items.AddRange(items);
        }
    }
    class Program
    {
        public static void Main(string[] args) {
            Part1("test.txt");
            //Part1("input.txt");
            //Part2("test.txt");
            //Part2("input.txt");

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static void Part1(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                while(reader.Peek() != -1) {
                    reader.ReadLine();
                    string[] itemStrings = reader.ReadLine().Split(':')[1].Trim().Split(new []{", "}, StringSplitOptions.RemoveEmptyEntries);
                    int[] items = new int[itemStrings.Length];
                    for (int i = 0; i < itemStrings.Length; i++) {
                        items[i] = int.Parse(itemStrings[i]);
                    }
                    string[] operationWhole = reader.ReadLine().Split('=')[1].Split(new []{" "}, StringSplitOptions.RemoveEmptyEntries);
                    char operation = operationWhole[1][0];
                    int operand = int.Parse(operationWhole[2]);
                    int divisor = int.Parse(reader.ReadLine().Split(new []{"by"}, StringSplitOptions.RemoveEmptyEntries)[1]);
                    int trueMonkey = int.Parse(reader.ReadLine().Split(new []{"monkey"}, StringSplitOptions.RemoveEmptyEntries)[1]);
                    int falseMonkey = int.Parse(reader.ReadLine().Split(new []{"monkey"}, StringSplitOptions.RemoveEmptyEntries)[1]);
                    Monkey m = new Monkey(trueMonkey, falseMonkey, divisor, operation, operand, items);
                    
                }

                Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, "Put solution here!");
            }
        }

        static void Part2(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                // TODO: Implement Functionality Here

                Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, "Put solution here!");
            }
        }
    }
}