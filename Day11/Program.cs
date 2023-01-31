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
        int inspections;
        
        public Monkey(int trueMonkey, int falseMonkey, int divisor, char operation, int operand, int[] items) {
            this.items = new List<int>();
            this.trueMonkey = trueMonkey;
            this.falseMonkey = falseMonkey;
            this.divisor = divisor;
            this.operation = operation;
            this.operand = operand;
            this.items.AddRange(items);
            inspections = 0;
        }

        public IEnumerable<int> InspectItems() {
            while(items.Count > 0) {
                yield return items.RemoveAt(0);
            }
        }

        public void AddItem(int item) {
            items.Add(item);
        }

        public int MonkeyToPassItem(out int item){
            inspections += 1;
            if(operation == '*') item = item*operand;
            else item = item + operand;
            item = item / 3;
            if(item % divisor == 0) return trueMonkey;
            return falseMonkey;
        }

        public int getNumInspections() {
            return inspections;
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

        static List<Mokey> ReadFromFile(StringReader reader) {
            List<Monkey> monkeys = new List<Monkey>();
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
                
                monkeys.Add(monkey);
            }
            return monkeys;
        }

        static void Part1(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                List<Monkey> monkeys = ReadFromFile(reader);
                int numTurns = 1;

                while(numTurns <= 20) {
                    foreach(Monkey m in monkeys) {
                        foreach(int item in monkey.InspectItems()) {
                            int nextMonkey = monkey.MonkeyToPassItem(out item);
                            monkeys[nextMonkey].AddItem(item);
                        }
                    }
                }

                int mostInspections = monkeys[0].getNumInspections();
                int most2Inspections;
                if(monkeys[1].getNumInspections() > mostInspections) {
                    most2Inspections = mostInspections;
                    mostInspections = monkeys[1].getNumInspections();
                } else {
                    most2Inspections = monkeys[1].getNumInspections();
                }

                for(int i = 2; i < monkeys.Count; i += 1) {
                    int inspectionCount = monkeys[i].getNumInspections();
                    if(inspectionCount > mostInspections) {
                        most2Inspections = mostInspections;
                        mostInspections = inspectionCount;
                    } else if (inspectionCount > most2Inspections) {
                        most2Inspections = inspectionCount;
                    }
                }

                Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, mostInspections*most2Inspections);
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