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
        public static int globalWorryModifier = 1;
        List<long> items;
        int trueMonkey;
        int falseMonkey;
        int divisor;
        char operation;
        int operand;
        long inspections;
        long worryModifier;
        
        public Monkey(int trueMonkey, int falseMonkey, int divisor, char operation, int operand, int worryModifier, long[] items) {
            this.items = new List<long>();
            this.trueMonkey = trueMonkey;
            this.falseMonkey = falseMonkey;
            this.divisor = divisor;
            this.operation = operation;
            this.operand = operand;
            this.items.AddRange(items);
            this.worryModifier = worryModifier;
            inspections = 0;
        }

        public IEnumerable<long> InspectItems() {
            while(items.Count > 0) {
                long item = items[0];
                items.RemoveAt(0);
                yield return item;
            }
        }

        public void AddItem(long item) {
            items.Add(item);
        }

        public int MonkeyToPassItem(ref long item){
            long curOperand = operand == -1 ? item : operand;
            inspections += 1;
            if(operation == '*') item = item*curOperand;
            else item = item + curOperand;
            item = item / worryModifier;
            if(item % divisor == 0) return trueMonkey;
            return falseMonkey;
        }

        public int MonkeyToPassItemB(ref long item){
            long curOperand = operand == -1 ? item : operand;
            inspections += 1;
            if(operation == '*') item = item*curOperand;
            else item = item + curOperand;
            item = item % globalWorryModifier;
            if(item % divisor == 0) return trueMonkey;
            return falseMonkey;
        }

        public long getNumInspections() {
            return inspections;
        }
    }
    class Program
    {
        public static void Main(string[] args) {
            Part1("test.txt");
            Part1("input.txt");
            Part2("test.txt");
            Part2("input.txt");

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        static List<Monkey> ReadFromFile(StringReader reader, int worryModifier) {
            List<Monkey> monkeys = new List<Monkey>();
            Monkey.globalWorryModifier = 1;
            while(reader.Peek() != -1) {
                reader.ReadLine();
                string[] itemStrings = reader.ReadLine().Split(':')[1].Trim().Split(new []{", "}, StringSplitOptions.RemoveEmptyEntries);
                long[] items = new long[itemStrings.Length];
                for (int i = 0; i < itemStrings.Length; i++) {
                    items[i] = long.Parse(itemStrings[i]);
                }
                string[] operationWhole = reader.ReadLine().Split('=')[1].Split(new []{" "}, StringSplitOptions.RemoveEmptyEntries);
                char operation = operationWhole[1][0];
                int operand;
                if(!int.TryParse(operationWhole[2], out operand)){
                    operand = -1;
                }
                int divisor = int.Parse(reader.ReadLine().Split(new []{"by"}, StringSplitOptions.RemoveEmptyEntries)[1]);
                Monkey.globalWorryModifier *= divisor;
                int trueMonkey = int.Parse(reader.ReadLine().Split(new []{"monkey"}, StringSplitOptions.RemoveEmptyEntries)[1]);
                int falseMonkey = int.Parse(reader.ReadLine().Split(new []{"monkey"}, StringSplitOptions.RemoveEmptyEntries)[1]);
                Monkey m = new Monkey(trueMonkey, falseMonkey, divisor, operation, operand, worryModifier, items);
                reader.ReadLine();
                
                monkeys.Add(m);
            }
            return monkeys;
        }

        static void getMaxInspections(List<Monkey> monkeys, out long mostInspections, out long most2Inspections) {
            mostInspections = monkeys[0].getNumInspections();
            
            if (monkeys[1].getNumInspections() > mostInspections) {
                most2Inspections = mostInspections;
                mostInspections = monkeys[1].getNumInspections();
            } else {
                most2Inspections = monkeys[1].getNumInspections();
            }
            for (int i = 2; i < monkeys.Count; i += 1) {
                long inspectionCount = monkeys[i].getNumInspections();
                if (inspectionCount > mostInspections) {
                    most2Inspections = mostInspections;
                    mostInspections = inspectionCount;
                } else if (inspectionCount > most2Inspections) {
                    most2Inspections = inspectionCount;
                }
            }
        }

        static void Part1(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                List<Monkey> monkeys = ReadFromFile(reader, 3);
                int numTurns = 0;

                while(numTurns < 20) {
                    foreach(Monkey m in monkeys) {
                        for (var i = m.InspectItems().GetEnumerator(); i.MoveNext();) {
                            long item = i.Current;
                            int nextMonkey = m.MonkeyToPassItem(ref item);
                            monkeys[nextMonkey].AddItem(item);
                        }
                    }
                    numTurns += 1;
                }

                long mostInspections;
                long most2Inspections;
                
                getMaxInspections(monkeys, out mostInspections, out most2Inspections);


                Console.WriteLine("The solution to Part 1 with inputfile: {0} is: {1}", filename, mostInspections*most2Inspections);
            }
        }
        
        static void printInspectionTotals(List<Monkey> monkeys, int round){
            Console.WriteLine("== After round {0} ==", round);
            for (int i = 0; i < monkeys.Count; i++) {
                Console.WriteLine("Monkey {0} inspected items {1} times.", i, monkeys[i].getNumInspections());
            }
        }

        static void Part2(string filename) {
            using(StringReader reader = new StringReader(File.ReadAllText(filename))){
                List<Monkey> monkeys = ReadFromFile(reader, 1);
                int numTurns = 0;

                while(numTurns < 10000) {
                    foreach(Monkey m in monkeys) {
                        for (var i = m.InspectItems().GetEnumerator(); i.MoveNext();) {
                            long item = i.Current;
                            int nextMonkey = m.MonkeyToPassItemB(ref item);
                            monkeys[nextMonkey].AddItem(item);
                        }
                    }
                    
                    numTurns += 1;
                }
                printInspectionTotals(monkeys, numTurns);

                long mostInspections;
                long most2Inspections;
                
                getMaxInspections(monkeys, out mostInspections, out most2Inspections);


                Console.WriteLine("The solution to Part 2 with inputfile: {0} is: {1}", filename, mostInspections*most2Inspections);
            }
        }
    }
}